namespace Manager.API.Dtos.Invoice
{
    public class UpdateInvoiceDto
    {
        public decimal RoomCharge { get; set; }
        public decimal ServiceCharge { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
    }
}