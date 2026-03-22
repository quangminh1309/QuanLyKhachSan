using Manager.API.Dtos.Review;
using Manager.API.Models;

namespace Manager.API.Mappers
{
    public static class ReviewMapper
    {
        public static ReviewDto ToReviewDto(this Review review)
        {
            return new ReviewDto
            {
                Id = review.Id,
                BookingId = review.BookingId,
                UserId = review.UserId,
                Rating = review.Rating,
                Comment = review.Comment,
                CreateAt = review.CreateAt
            };
        }

        public static Review ToReview(this CreateReviewDto dto)
        {
            return new Review
            {
                BookingId = dto.BookingId,
                UserId = dto.UserId,
                Rating = dto.Rating,
                Comment = dto.Comment
            };
        }
    }
}