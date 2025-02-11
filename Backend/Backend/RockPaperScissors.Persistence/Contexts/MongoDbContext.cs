using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using RockPaperScissors.Application.Services;
using RockPaperScissors.Domain.Entities;

namespace RockPaperScissors.Persistence;

public class MongoDbContext 
{
    public IMongoDatabase _database;

    public MongoDbContext(string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
    }
    
    public IMongoCollection<UserRating> Ratings => _database.GetCollection<UserRating>("Ratings");
}