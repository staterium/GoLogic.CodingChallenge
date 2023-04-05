namespace WebAPI.Interfaces
{
    /// <summary>
    ///     An interface that describes all the functionality a module must implement.
    ///     Modules are organised vertically (i.e. ProductModule, PurchaseModule etc).
    ///     Credit: https://timdeschryver.dev/blog/maybe-its-time-to-rethink-our-project-structure-with-dot-net-6
    /// </summary>
    public interface IModule
    {
        #region Public Members

        IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints);

        IServiceCollection RegisterModule(IServiceCollection builder);

        #endregion
    }
}