namespace FunctionalTests
{
    /// <summary>
    ///     A collection definition that is used to group tests together, so that functional tests share context are run in
    ///     sequence
    /// </summary>
    [CollectionDefinition("Http Client Collection")]
    public class HttpClientCollection : ICollectionFixture<CustomWebApplicationFactory<Program>>
    {
    }
}