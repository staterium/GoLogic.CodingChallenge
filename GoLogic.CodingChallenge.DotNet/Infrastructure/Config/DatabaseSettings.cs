namespace Infrastructure.Config
{
    public class DatabaseSettings
    {
        #region Properties

        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        #endregion
    }
}