namespace Manager.API.Dtos.Incident
{
    public class IncidentDto
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime CreateAt { get; set; }
    }
}