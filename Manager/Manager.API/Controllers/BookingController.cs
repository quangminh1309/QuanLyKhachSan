using Manager.API.Dtos.Booking;
using Manager.API.Interfaces;
using Manager.API.Mappers;
using Manager.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Manager.API.Controllers
{
    [Route("api/Booking")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepo;
        private readonly IRoomRepository _roomRepo;
        private readonly IRoomRateRepository _roomRateRepo;
        private readonly UserManager<AppUser> _userManager;

        public BookingController(
            IBookingRepository bookingRepo,
            IRoomRepository roomRepo,
            IRoomRateRepository roomRateRepo,
            UserManager<AppUser> userManager)
        {
            _bookingRepo = bookingRepo;
            _roomRepo = roomRepo;
            _roomRateRepo = roomRateRepo;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> GetAll()
        {
            var bookings = await _bookingRepo.GetAllAsync();
            if (bookings == null || bookings.Count == 0)
                return NotFound("No bookings found.");
            var dtos = bookings.Select(b => b.ToBookingDto());
            return Ok(dtos);
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var booking = await _bookingRepo.GetByIdAsync(id);
            if (booking == null)
                return NotFound($"No booking found with id {id}.");

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            if (booking.UserId != user.Id && !User.IsInRole("Admin") && !User.IsInRole("Manager"))
                return Forbid();

            return Ok(booking.ToBookingDto());
        }

        [HttpGet("my-bookings")]
        [Authorize]
        public async Task<IActionResult> GetMyBookings()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            var bookings = await _bookingRepo.GetByUserIdAsync(user.Id);
            var dtos = bookings.Select(b => b.ToBookingDto());
            return Ok(dtos);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateBookingRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            var room = await _roomRepo.GetByIdAsync(dto.RoomId);
            if (room == null)
                return NotFound("Room not found.");

            if (room.CurrentStatus != "Available")
                return BadRequest("Room is not available.");

            if (dto.CheckInDate >= dto.CheckOutDate)
                return BadRequest("Check-out date must be after check-in date.");

            if (dto.CheckInDate < DateTime.Now.Date)
                return BadRequest("Check-in date cannot be in the past.");

            var booking = dto.ToBookingFromCreate(user.Id);

            var days = (dto.CheckOutDate - dto.CheckInDate).Days;
            var roomRate = await _roomRateRepo.GetByRoomTypeIdAsync(room.RoomTypeId);
            if (roomRate != null)
            {
                booking.TotalPrice = roomRate.Price * days;
            }

            var created = await _bookingRepo.CreateAsync(booking);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created.ToBookingDto());
        }

        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateBookingRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var booking = await _bookingRepo.GetByIdAsync(id);
            if (booking == null)
                return NotFound($"No booking found with id {id}.");

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            if (booking.UserId != user.Id && !User.IsInRole("Admin") && !User.IsInRole("Manager"))
                return Forbid();

            if (booking.Status != "Pending" && booking.Status != "Confirmed")
                return BadRequest("Cannot update booking in current status.");

            if (dto.CheckInDate.HasValue)
                booking.CheckInDate = dto.CheckInDate.Value;

            if (dto.CheckOutDate.HasValue)
                booking.CheckOutDate = dto.CheckOutDate.Value;

            if (dto.NumberOfGuests.HasValue)
                booking.NumberOfGuests = dto.NumberOfGuests.Value;

            if (dto.SpecialRequests != null)
                booking.SpecialRequests = dto.SpecialRequests;

            if (dto.CheckInDate.HasValue || dto.CheckOutDate.HasValue)
            {
                var days = (booking.CheckOutDate - booking.CheckInDate).Days;
                var room = await _roomRepo.GetByIdAsync(booking.RoomId);
                if (room != null)
                {
                    var roomRate = await _roomRateRepo.GetByRoomTypeIdAsync(room.RoomTypeId);
                    if (roomRate != null)
                    {
                        booking.TotalPrice = roomRate.Price * days;
                    }
                }
            }

            var updated = await _bookingRepo.UpdateAsync(id, booking);
            return Ok(updated?.ToBookingDto());
        }

        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Cancel([FromRoute] int id)
        {
            var booking = await _bookingRepo.GetByIdAsync(id);
            if (booking == null)
                return NotFound($"No booking found with id {id}.");

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            if (booking.UserId != user.Id && !User.IsInRole("Admin") && !User.IsInRole("Manager"))
                return Forbid();

            if (booking.Status != "Pending" && booking.Status != "Confirmed")
                return BadRequest("Cannot cancel booking in current status.");

            booking.Status = "Cancelled";
            await _bookingRepo.UpdateAsync(id, booking);

            var room = await _roomRepo.GetByIdAsync(booking.RoomId);
            var updateRoom = room.ToUpdateRoomRequestDto();
            if (room != null && room.CurrentStatus == "Reserved")
            {
                room.CurrentStatus = "Available";
                await _roomRepo.UpdateAsync(booking.RoomId, updateRoom);
            }

            return Ok(new { message = "Booking cancelled successfully." });
        }

        [HttpGet("history")]
        [Authorize]
        public async Task<IActionResult> GetBookingHistory()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            var bookings = await _bookingRepo.GetByUserIdAsync(user.Id);
            var dtos = bookings.Select(b => b.ToBookingDto()).OrderByDescending(b => b.CreatedAt);
            return Ok(dtos);
        }

        [HttpPost("{id:int}/request-refund")]
        [Authorize]
        public async Task<IActionResult> RequestRefund([FromRoute] int id, [FromBody] RefundRequestDto dto)
        {
            var booking = await _bookingRepo.GetByIdAsync(id);
            if (booking == null)
                return NotFound($"No booking found with id {id}.");

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            if (booking.UserId != user.Id)
                return Forbid();

            if (booking.Status != "Cancelled")
                return BadRequest("Can only request refund for cancelled bookings.");

            booking.RefundRequested = true;
            booking.RefundReason = dto.Reason;
            booking.RefundRequestedAt = DateTime.Now;
            await _bookingRepo.UpdateAsync(id, booking);

            return Ok(new { message = "Refund request submitted successfully." });
        }

        [HttpPost("{id:int}/cancel-refund-request")]
        [Authorize]
        public async Task<IActionResult> CancelRefundRequest([FromRoute] int id)
        {
            var booking = await _bookingRepo.GetByIdAsync(id);
            if (booking == null)
                return NotFound($"No booking found with id {id}.");

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            if (booking.UserId != user.Id)
                return Forbid();

            if (!booking.RefundRequested)
                return BadRequest("No refund request found.");

            booking.RefundRequested = false;
            booking.RefundReason = null;
            booking.RefundRequestedAt = null;
            await _bookingRepo.UpdateAsync(id, booking);

            return Ok(new { message = "Refund request cancelled successfully." });
        }
    }
}

public class RefundRequestDto
{
    public string Reason { get; set; } = string.Empty;
}