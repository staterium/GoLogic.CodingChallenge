using WebAPI.Modules.Purchases.Models;
using WebAPI.Modules.Users.Models;

namespace FunctionalTests.Tests
{
    public class TestBase
    {
        #region Fields

        protected readonly HttpClient Client;

        #endregion

        #region Constructors

        public TestBase(CustomWebApplicationFactory<Program> factory)
        {
            Client = factory.CreateClient();
        }

        #endregion

        #region Protected Members

        protected Task<HttpResponseMessage> MakePurchaseAsync(string testProduct, string testUser)
        {
            return Client.PostAsJsonAsync("/purchase", new MakePurchaseDto { ProductName = testProduct, UserName = testUser });
        }

        protected async Task ResetAndSetUpUserAsync(string userName, decimal depositAmount = 0m)
        {
            await ResetAsync();
            await Client.PostAsJsonAsync("/user", new CreateUserDto { UserName = userName });
            await Client.PostAsJsonAsync("/deposit", new DepositFundsDto { UserName = userName, DepositAmount = depositAmount });
        }

        protected Task ResetAsync()
        {
            return Client.GetAsync("/reset");
        }

        #endregion
    }
}