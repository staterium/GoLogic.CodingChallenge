using Core.Entities;
using Core.Interfaces;

namespace FunctionalTests.TestHarnesses
{
    /// <summary>
    ///     A test harness that implements the <see cref="IUserRepository" /> interface and facilitates in-memory testing.
    /// </summary>
    internal class UserRepositoryTestHarness : IUserRepository
    {
        #region Fields

        private readonly List<User> _users = new();

        #endregion

        #region Interface Implementations

        public Task DeleteAllUsersAsync()
        {
            return Task.Run(() => _users.Clear());
        }

        public Task<User?> GetByNameAsync(string name)
        {
            return Task.FromResult(_users.FirstOrDefault(u => u.Name == name));
        }

        public Task SaveNewUserAsync(User user)
        {
            return Task.Run(() => _users.Add(user));
        }

        public Task UpdateUserAsync(User user)
        {
            return Task.Run(
                () =>
                {
                    var index = _users.FindIndex(u => u.Name == user.Name);
                    _users[index] = user;
                });
        }

        #endregion
    }
}