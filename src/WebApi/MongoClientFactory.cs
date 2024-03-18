using MongoDB.Driver;

namespace WebApi;

public static class MongoClientFactory {
    public static IMongoDatabase ConfigureMongo(IConfiguration configuration) {
        var settings = MongoClientSettings.FromConnectionString(configuration["DocumentStore:ConnectionString"]);
        return new MongoClient(settings).GetDatabase(configuration["DocumentStore:DatabaseName"]);
    }
}