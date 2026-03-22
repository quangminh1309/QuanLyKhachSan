using Manager.API.Dtos.LostItem;
using Manager.API.Models;

namespace Manager.API.Mappers
{
    public static class LostItemMapper
    {
        public static LostItemDto ToLostItemDto(this LostItem lostItem)
        {
            return new LostItemDto
            {
                Id = lostItem.Id,
                BookingId = lostItem.BookingId,
                ItemName = lostItem.ItemName,
                Description = lostItem.Description,
                Status = lostItem.Status,
                FoundDate = lostItem.FoundDate,
                CreateAt = lostItem.CreateAt
            };
        }

        public static LostItem ToLostItem(this CreateLostItemDto dto)
        {
            return new LostItem
            {
                BookingId = dto.BookingId,
                ItemName = dto.ItemName,
                Description = dto.Description,
                FoundDate = dto.FoundDate
            };
        }
    }
}