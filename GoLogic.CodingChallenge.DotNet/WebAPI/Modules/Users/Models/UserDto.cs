namespace WebAPI.Modules.Users.Models
{
    /// <summary>
    /// </summary>
    [AutoMap(typeof(User))]
    public class UserDto
    {
        #region Properties

        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal BalanceAvailable { get; set; }

        #endregion
    }
}