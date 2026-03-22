using Manager.API.Dtos.Invoice;
using Manager.API.Models;

namespace Manager.API.Mappers
{
    public static class InvoiceMapper
    {
        public static InvoiceDto ToInvoiceDto(this Invoice invoice)
        {
            return new InvoiceDto
            {
                Id = invoice.Id,
                BookingId = invoice.BookingId,
                RoomCharge = invoice.RoomCharge,
                ServiceCharge = invoice.ServiceCharge,
                Discount = invoice.Discount,
                TotalAmount = invoice.TotalAmount,
                Status = invoice.Status,
                CreateAt = invoice.CreateAt
            };
        }

        public static Invoice ToInvoice(this CreateInvoiceDto dto)
        {
            return new Invoice
            {
                BookingId = dto.BookingId,
                RoomCharge = dto.RoomCharge,
                ServiceCharge = dto.ServiceCharge,
                Discount = dto.Discount,
                TotalAmount = dto.TotalAmount
            };
        }
    }
}