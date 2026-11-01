using Mongo2Go;
using MongoDB.Driver;

namespace ShopOn.IntegrationTests.Helpers;

public class MongoDbFixture
{
    public MongoDbRunner Runner { get; private set; }
    public MongoClient Client { get; private set; }

    public MongoDbFixture()
    {
        Runner = MongoDbRunner.StartForDebugging(port:27018);
        Client = new MongoClient(Runner.ConnectionString);
    }

    public void Dispose()
    {
        Runner.Dispose();
    }
}
