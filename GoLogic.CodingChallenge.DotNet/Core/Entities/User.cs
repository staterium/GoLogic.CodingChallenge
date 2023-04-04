namespace Core.Entities
{
    public class User
    {
        #region Properties

        public string Name { get; set; }

        #endregion

        #region Constructors

        public User(string name)
        {
            Name = name;
        }

        #endregion
    }
}