using Ardalis.GuardClauses;

namespace Core.Entities
{
    public class User
    {
        #region Properties

        public string Name { get; set; }

        public decimal BalanceAvailable { get; set; }

        #endregion

        #region Constructors

        public User(string name, decimal balanceAvailable)
        {
            Guard.Against.Negative(balanceAvailable);

            Name = name;
            BalanceAvailable = balanceAvailable;
        }

        #endregion
    }
}