using WebAPI.Modules.Purchases.Models;
using WebAPI.Modules.Users.Models;

namespace WebAPI.Modules.Users
{
    /// <summary>
    ///     A module that contains all the functionality related to users (endpoints, services etc).
    /// </summary>
    internal class UsersModule : IModule
    {
        #region Interface Implementations

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/user/{username}", GetUserByNameAsync());
            endpoints.MapPost("/user", CreateUserAsync());
            endpoints.MapPost("/deposit", DepositFundsAsync());
            endpoints.MapGet("/cashout/{username}", CashOutAsync());

            return endpoints;
        }

        public IServiceCollection RegisterModule(IServiceCollection builder)
        {
            builder.AddTransient<IUserRepository, UserRepository>();
            return builder;
        }

        #endregion

        #region Private Members

        private static Func<string, IUserRepository, IProductRepository, IPurchaseRepository, Task<IResult>> CashOutAsync()
        {
            return async (userName, userRepository, productRepository, purchaseRepository) =>
            {
                var user = await userRepository.GetByNameAsync(userName);

                if (user == null)
                    return Results.NotFound("No such user found");

                var products = await productRepository.GetAllProductsAsync();
                var purchases = await purchaseRepository.GetAllUserPurchasesAsync(user);

                var purchasesDto = purchases.Select(
                    s => new ListPurchasesDto
                    {
                        Price = products.First(f => f.Name == s.ProductName).Price,
                        ProductName = s.ProductName,
                        Quantity = 1,
                        Total = products.First(f => f.Name == s.ProductName).Price
                    });

                var purchasesGrouped = purchasesDto.GroupBy(g => new { g.ProductName, g.Price })
                    .ToList()
                    .Select(s => new { s.Key.ProductName, s.Key.Price, Quantity = s.Sum(g => g.Quantity), Total = s.Sum(g => g.Total) })
                    .ToList();

                var result = new
                {
                    UserName = user.Name,
                    TotalSpent = purchasesGrouped.Sum(s => s.Total),
                    ChangeRecevied = user.BalanceAvailable,
                    Purchases = purchasesGrouped
                };

                return Results.Ok(result);
            };
        }

        private static Func<CreateUserDto, IUserRepository, Task<IResult>> CreateUserAsync()
        {
            return async (userDto, userRepository) =>
            {
                var user = new User(userDto.UserName);
                await userRepository.SaveNewUserAsync(user);

                return Results.Ok();
            };
        }

        private static Func<DepositFundsDto, IUserRepository, Task<IResult>> DepositFundsAsync()
        {
            return async (depositFundsDto, userRepository) =>
            {
                var user = await userRepository.GetByNameAsync(depositFundsDto.UserName);

                if (user == null)
                    return Results.NotFound();

                user.BalanceAvailable += depositFundsDto.DepositAmount;
                await userRepository.UpdateUserAsync(user);

                return Results.Ok();
            };
        }

        private static Func<string, IUserRepository, IMapper, Task<IResult>> GetUserByNameAsync()
        {
            return async (username, userRepository, mapper) =>
            {
                var user = await userRepository.GetByNameAsync(username);
                var result = mapper.Map<UserDto>(user);

                return user == null ? Results.NotFound() : Results.Ok(result);
            };
        }

        #endregion
    }
}