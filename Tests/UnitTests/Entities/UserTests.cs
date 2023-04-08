using Core.Entities;

namespace UnitTests.Entities
{
    public class UserTests
    {
        #region Public Members

        [Fact]
        public void BalanceAvailable_Cannot_BeLessThanZero()
        {
            //assert
            Assert.Throws<ArgumentException>(() => new User("TestUser", -1m));
        }

        #endregion
    }
}