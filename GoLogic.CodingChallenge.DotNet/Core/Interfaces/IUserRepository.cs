using Core.Entities;

namespace Core.Interfaces
{
    public interface IUserRepository
    {
        #region Public Members

        public User GetByName(string name);

        public void SaveNewUser(User user);

        #endregion
    }
}