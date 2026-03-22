namespace Manager.API.Dtos.Invoice
{
    public class InvoiceDto
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public decimal RoomCharge { get; set; }
        public decimal ServiceCharge { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public DateTime CreateAt { get; set; }
    }
}