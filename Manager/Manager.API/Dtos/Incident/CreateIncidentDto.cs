namespace Manager.API.Dtos.Incident
{
    public class CreateIncidentDto
    {
        public int BookingId { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}