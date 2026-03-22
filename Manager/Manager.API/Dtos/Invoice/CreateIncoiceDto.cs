namespace Manager.API.Dtos.Invoice
{
    public class CreateInvoiceDto
    {
        public int BookingId { get; set; }
        public decimal RoomCharge { get; set; }
        public decimal ServiceCharge { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
    }
}