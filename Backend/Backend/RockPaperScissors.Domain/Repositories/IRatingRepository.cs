using RockPaperScissors.Domain.Entities;

namespace RockPaperScissors.Domain.Repositories;

public interface IRatingRepository
{
    Task<List<UserRating>> GetAllAsync();
    Task<UserRating?> GetByUserIdAsync(Guid userId);
    Task AddOrUpdateAsync(UserRating rating);
}