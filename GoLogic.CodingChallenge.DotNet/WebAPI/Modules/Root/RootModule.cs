namespace WebAPI.Modules.Root
{
    /// <summary>
    ///     Describes all the endpoints and services that are available in the root module.
    /// </summary>
    public class RootModule : IModule
    {
        #region Interface Implementations

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/", () => Results.Ok("API available")); // health probe endpoint
            endpoints.MapGet("/reset", ResetDatabaseAsync());

            return endpoints;
        }

        public IServiceCollection RegisterModule(IServiceCollection builder)
        {
            return builder;
        }

        #endregion

        #region Private Members

        /// <summary>
        ///     Resets the database to its initial state.
        /// </summary>
        private static Func<IServiceProvider, Task<IResult>> ResetDatabaseAsync()
        {
            return async services =>
            {
                await ClearAndSeedData.InitializeAsync(services);
                return Results.Ok();
            };
        }

        #endregion
    }
}