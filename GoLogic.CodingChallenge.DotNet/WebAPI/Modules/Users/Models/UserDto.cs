namespace WebAPI.Modules.Users.Models
{
    [AutoMap(typeof(User))]
    internal class UserDto
    {
        #region Properties

        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal BalanceAvailable { get; set; }

        #endregion
    }
}