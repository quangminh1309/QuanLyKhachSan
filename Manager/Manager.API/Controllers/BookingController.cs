using Manager.API.Dtos.Booking;
using Manager.API.Interfaces;
using Manager.API.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace Manager.API.Controllers
{
    [Route("api/Booking")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;
        public BookingController(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bookings = await _bookingRepository.GetAllAsync();
            if (bookings == null || bookings.Count == 0)
                return NotFound("No bookings found.");
            var dtos = bookings.Select(b => b.ToBookingDto());
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var booking = await _bookingRepository.GetByIdAsync(id);
            if (booking == null)
                return NotFound($"No booking found with id {id}.");
            return Ok(booking.ToBookingDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBookingDto dto)
        {
            var booking = dto.ToBooking();
            var created = await _bookingRepository.CreateAsync(booking);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created.ToBookingDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateBookingDto dto)
        {
            var updated = await _bookingRepository.UpdateAsync(id, dto);
            if (updated == null)
                return NotFound($"No booking found with id {id}.");
            return Ok(updated.ToBookingDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _bookingRepository.DeleteAsync(id);
            if (deleted == null)
                return NotFound($"No booking found with id {id}.");
            return Ok(deleted.ToBookingDto());
        }
    }
}