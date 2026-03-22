using Manager.API.Dtos.Review;
using Manager.API.Models;

namespace Manager.API.Interfaces
{
    public interface IReviewRepository
    {
        Task<List<Review>> GetAllAsync();
        Task<Review?> GetByIdAsync(int id);
        Task<Review> CreateAsync(Review review);
        Task<Review?> UpdateAsync(int id, UpdateReviewDto dto);
        Task<Review?> DeleteAsync(int id);
    }
}