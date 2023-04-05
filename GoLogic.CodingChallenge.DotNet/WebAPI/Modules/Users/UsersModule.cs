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
            endpoints.MapGet("/user/{name}", GetUserByNameAsync());
            endpoints.MapPost("/user", CreateUserAsync());

            return endpoints;
        }

        public IServiceCollection RegisterModule(IServiceCollection builder)
        {
            builder.AddTransient<IUserRepository, UserRepository>();
            return builder;
        }

        #endregion

        #region Private Members

        private static Func<CreateUserDto, IUserRepository, Task<IResult>> CreateUserAsync()
        {
            return async (userDto, userRepository) =>
            {
                var user = new User(userDto.Name);
                await userRepository.SaveNewUserAsync(user);

                return Results.Ok();
            };
        }

        private static Func<string, IUserRepository, IMapper, Task<IResult>> GetUserByNameAsync()
        {
            return async (name, userRepository, mapper) =>
            {
                var user = await userRepository.GetByNameAsync(name);
                var result = mapper.Map<UserDto>(user);

                return user == null ? Results.NotFound() : Results.Ok(result);
            };
        }

        #endregion
    }
}