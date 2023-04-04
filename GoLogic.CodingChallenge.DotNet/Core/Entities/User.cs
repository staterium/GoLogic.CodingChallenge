using Ardalis.GuardClauses;

namespace Core.Entities
{
    /// <summary>
    ///     An entity class describing a user of the vending machine.
    ///     This class is used to store the balance available to the user, and to track purchase history.
    /// </summary>
    public class User
    {
        #region Properties

        /// <summary>
        ///     The name of the user. This is used to identify the user in lieu of a user id or any type of authentication.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     The balance available to the user.
        /// </summary>
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