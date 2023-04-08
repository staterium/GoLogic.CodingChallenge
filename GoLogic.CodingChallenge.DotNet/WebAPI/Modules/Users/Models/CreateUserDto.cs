namespace WebAPI.Modules.Users.Models
{
    /// <summary>
    ///     Represents the data transfer object for a user creation operation.
    /// </summary>
    public class CreateUserDto
    {
        #region Properties

        public string UserName { get; set; }

        #endregion
    }
}