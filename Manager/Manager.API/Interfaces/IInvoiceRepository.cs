using Manager.API.Dtos.Invoice;
using Manager.API.Models;

namespace Manager.API.Interfaces
{
    public interface IInvoiceRepository
    {
        Task<List<Invoice>> GetAllAsync();
        Task<Invoice?> GetByIdAsync(int id);
        Task<Invoice> CreateAsync(Invoice invoice);
        Task<Invoice?> UpdateAsync(int id, UpdateInvoiceDto dto);
        Task<Invoice?> DeleteAsync(int id);
    }
}