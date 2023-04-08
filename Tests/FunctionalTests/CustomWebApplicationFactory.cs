using Core.Interfaces;
using FunctionalTests.TestHarnesses;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FunctionalTests
{
    /// <summary>
    ///     A web application factory that allows for the injection of test services.
    /// </summary>
    /// <typeparam name="TProgram"></typeparam>
    public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        #region Protected Members

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(
                services =>
                {
                    //remove the production services
                    services.Remove(services.Single(d => d.ServiceType == typeof(IUserRepository)));
                    services.Remove(services.Single(d => d.ServiceType == typeof(IPurchaseRepository)));
                    services.Remove(services.Single(d => d.ServiceType == typeof(IProductRepository)));

                    //add test services
                    services.AddSingleton<IProductRepository, ProductRepositoryTestHarness>();
                    services.AddSingleton<IUserRepository, UserRepositoryTestHarness>();
                    services.AddSingleton<IPurchaseRepository, PurchaseRepositoryTestHarness>();
                });
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            var host = builder.Build();
            host.Start();
            return host;
        }

        #endregion
    }
}