using WebAPI.Modules.Purchases;
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

        /// <summary>
        ///     A function that cashes out the user.
        /// </summary>
        private static Func<string, IUserRepository, IProductRepository, IPurchaseRepository, Task<IResult>> CashOutAsync()
        {
            return async (userName, userRepository, productRepository, purchaseRepository) =>
            {
                var user = await userRepository.GetByNameAsync(userName);

                if (user == null)
                    return Results.NotFound("No such user found");

                var products = await productRepository.GetAllProductsAsync();
                var purchases = await purchaseRepository.GetAllUserPurchasesAsync(user);
                var purchasesGrouped = PurchasesModule.GetPurchasesGrouped(purchases, products);

                var result = new CashOutDto
                {
                    UserName = user.Name,
                    TotalSpent = purchasesGrouped.Sum(s => s.Total),
                    ChangeReceived = user.BalanceAvailable,
                    Purchases = purchasesGrouped
                };

                return Results.Ok(result);
            };
        }

        /// <summary>
        ///     A function that creates a new user.
        /// </summary>
        private static Func<CreateUserDto, IUserRepository, Task<IResult>> CreateUserAsync()
        {
            return async (userDto, userRepository) =>
            {
                var user = new User(userDto.UserName);
                await userRepository.SaveNewUserAsync(user);

                return Results.Ok();
            };
        }

        /// <summary>
        ///     A function that deposits funds to the user.
        /// </summary>
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

        /// <summary>
        ///     A function that gets a user by name.
        /// </summary>
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