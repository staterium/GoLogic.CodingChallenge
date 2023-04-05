namespace WebAPI.Extensions
{
    /// <summary>
    ///     Helper functions used for the DI and endpoint registration of modules.
    ///     Credit: https://timdeschryver.dev/blog/maybe-its-time-to-rethink-our-project-structure-with-dot-net-6
    /// </summary>
    public static class ModuleExtensions
    {
        #region Fields

        private static readonly List<IModule> RegisteredModules = new();

        #endregion

        #region Public Members

        public static WebApplication MapEndpoints(this WebApplication app)
        {
            foreach (var module in RegisteredModules)
                module.MapEndpoints(app);

            return app;
        }

        public static IServiceCollection RegisterModules(this IServiceCollection services)
        {
            var modules = DiscoverModules();
            foreach (var module in modules)
            {
                module.RegisterModule(services);
                RegisteredModules.Add(module);
            }

            return services;
        }

        #endregion

        #region Private Members

        private static IEnumerable<IModule> DiscoverModules()
        {
            return typeof(IModule).Assembly.GetTypes()
                .Where(p => p.IsClass && p.IsAssignableTo(typeof(IModule)))
                .Select(Activator.CreateInstance)
                .Cast<IModule>();
        }

        #endregion
    }
}