namespace Manager.API.Dtos.LostItem
{
    public class LostItemDto
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime FoundDate { get; set; }
        public DateTime CreateAt { get; set; }
    }
}