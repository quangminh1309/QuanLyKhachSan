using Manager.API.Data;
using Manager.API.Dtos.Review;
using Manager.API.Interfaces;
using Manager.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Manager.API.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDBContext _context;
        public ReviewRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Review>> GetAllAsync()
        {
            return await _context.Reviews.ToListAsync();
        }

        public async Task<Review?> GetByIdAsync(int id)
        {
            return await _context.Reviews.FindAsync(id);
        }

        public async Task<Review> CreateAsync(Review review)
        {
            review.CreateAt = DateTime.Now;
            review.UpdateAt = DateTime.Now;
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
            return review;
        }

        public async Task<Review?> UpdateAsync(int id, UpdateReviewDto dto)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null) return null;

            review.Rating = dto.Rating;
            review.Comment = dto.Comment;
            review.UpdateAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return review;
        }

        public async Task<Review?> DeleteAsync(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null) return null;

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
            return review;
        }
    }
}