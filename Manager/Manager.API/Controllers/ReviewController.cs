using Manager.API.Dtos.Review;
using Manager.API.Interfaces;
using Manager.API.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace Manager.API.Controllers
{
    [Route("api/Review")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;
        public ReviewController(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var reviews = await _reviewRepository.GetAllAsync();
            if (reviews == null || reviews.Count == 0)
                return NotFound("No reviews found.");
            var dtos = reviews.Select(r => r.ToReviewDto());
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var review = await _reviewRepository.GetByIdAsync(id);
            if (review == null)
                return NotFound($"No review found with id {id}.");
            return Ok(review.ToReviewDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateReviewDto dto)
        {
            var review = dto.ToReview();
            var created = await _reviewRepository.CreateAsync(review);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created.ToReviewDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateReviewDto dto)
        {
            var updated = await _reviewRepository.UpdateAsync(id, dto);
            if (updated == null)
                return NotFound($"No review found with id {id}.");
            return Ok(updated.ToReviewDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _reviewRepository.DeleteAsync(id);
            if (deleted == null)
                return NotFound($"No review found with id {id}.");
            return Ok(deleted.ToReviewDto());
        }
    }
}