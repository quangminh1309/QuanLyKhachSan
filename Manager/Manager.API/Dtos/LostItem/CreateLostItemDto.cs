namespace Manager.API.Dtos.LostItem
{
    public class CreateLostItemDto
    {
        public int BookingId { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public DateTime FoundDate { get; set; }
    }
}