namespace Manager.API.Dtos.Review
{
    public class CreateReviewDto
    {
        public int BookingId { get; set; }
        public string UserId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}