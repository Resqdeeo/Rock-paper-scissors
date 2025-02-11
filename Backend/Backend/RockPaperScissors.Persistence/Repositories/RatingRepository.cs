using MongoDB.Driver;
using RockPaperScissors.Domain.Entities;
using RockPaperScissors.Domain.Repositories;

namespace RockPaperScissors.Persistence.Repositories;

public class RatingRepository : IRatingRepository
{
    private readonly IMongoCollection<UserRating> _ratings;

    public RatingRepository(MongoDbContext context)
    {
        _ratings = context.Ratings;
    }

    public async Task<List<UserRating>> GetAllAsync()
    {
        return await _ratings.Find(_ => true).SortByDescending(r => r.Rating).ToListAsync();
    }

    public async Task<UserRating?> GetByUserIdAsync(Guid userId)
    {
        return await _ratings.Find(r => r.UserId == userId).FirstOrDefaultAsync();
    }

    public async Task AddOrUpdateAsync(UserRating rating)
    {
        var filter = Builders<UserRating>.Filter.Eq(r => r.UserId, rating.UserId);
        await _ratings.ReplaceOneAsync(filter, rating, new ReplaceOptions { IsUpsert = true });
    }
}