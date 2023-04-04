using Core.Entities;

namespace Core.Interfaces
{
    /// <summary>
    ///     An interface describing the user repository.
    /// </summary>
    public interface IUserRepository
    {
        #region Public Members

        /// <summary>
        ///     Gets a user by name.
        /// </summary>
        /// <param name="name">
        ///     The name of the user.
        /// </param>
        /// <returns>
        ///     The user with the specified name.
        /// </returns>
        public User GetByName(string name);

        /// <summary>
        ///     Saves a new user.
        /// </summary>
        /// <param name="user">
        ///     The user to save.
        /// </param>
        public void SaveNewUser(User user);

        #endregion
    }
}