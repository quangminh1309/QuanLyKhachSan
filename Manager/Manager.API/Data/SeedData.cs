using Manager.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Manager.API.Data
{
    public static class SeedData
    {
        public static async Task Initialize(ApplicationDBContext context)
        {
            // Kiểm tra đã có data chưa
            if (await context.RoomTypes.AnyAsync())
            {
                return; // Database đã có data
            }

            // Seed Room Types
            var roomTypes = new List<RoomType>
            {
                new RoomType
                {
                    Name = "Standard",
                    Description = "Phòng tiêu chuẩn với đầy đủ tiện nghi cơ bản",
                    Capacity = "2",
                    CreateAt = DateTime.Now
                },
                new RoomType
                {
                    Name = "Deluxe",
                    Description = "Phòng cao cấp với view đẹp và tiện nghi hiện đại",
                    Capacity = "2",
                    CreateAt = DateTime.Now
                },
                new RoomType
                {
                    Name = "Suite",
                    Description = "Phòng suite rộng rãi với phòng khách riêng",
                    Capacity = "4",
                    CreateAt = DateTime.Now
                },
                new RoomType
                {
                    Name = "Family",
                    Description = "Phòng gia đình lớn, phù hợp cho 4-6 người",
                    Capacity = "6",
                    CreateAt = DateTime.Now
                }
            };
            await context.RoomTypes.AddRangeAsync(roomTypes);
            await context.SaveChangesAsync();

            // Seed Rooms
            var rooms = new List<Rooms>();
            var roomTypeIds = await context.RoomTypes.Select(rt => rt.Id).ToListAsync();
            
            // Tầng 1: Standard rooms
            for (int i = 1; i <= 5; i++)
            {
                rooms.Add(new Rooms
                {
                    RoomNumber = $"10{i}",
                    RoomTypeId = roomTypeIds[0], // Standard
                    CurrentStatus = "Available",
                    CreateAt = DateTime.Now
                });
            }

            // Tầng 2: Deluxe rooms
            for (int i = 1; i <= 5; i++)
            {
                rooms.Add(new Rooms
                {
                    RoomNumber = $"20{i}",
                    RoomTypeId = roomTypeIds[1], // Deluxe
                    CurrentStatus = "Available",
                    CreateAt = DateTime.Now
                });
            }

            // Tầng 3: Suite và Family
            for (int i = 1; i <= 3; i++)
            {
                rooms.Add(new Rooms
                {
                    RoomNumber = $"30{i}",
                    RoomTypeId = roomTypeIds[2], // Suite
                    CurrentStatus = "Available",
                    CreateAt = DateTime.Now
                });
            }

            for (int i = 4; i <= 5; i++)
            {
                rooms.Add(new Rooms
                {
                    RoomNumber = $"30{i}",
                    RoomTypeId = roomTypeIds[3], // Family
                    CurrentStatus = "Available",
                    CreateAt = DateTime.Now
                });
            }

            await context.Rooms.AddRangeAsync(rooms);
            await context.SaveChangesAsync();

            // Seed Services
            var services = new List<Services>
            {
                new Services
                {
                    ServiceType = "Laundry",
                    Name = "Giặt ủi",
                    Price = 50000,
                    unit = "kg",
                    CreateAt = DateTime.Now
                },
                new Services
                {
                    ServiceType = "Food",
                    Name = "Ăn sáng",
                    Price = 150000,
                    unit = "suất",
                    CreateAt = DateTime.Now
                },
                new Services
                {
                    ServiceType = "Spa",
                    Name = "Massage",
                    Price = 300000,
                    unit = "giờ",
                    CreateAt = DateTime.Now
                },
                new Services
                {
                    ServiceType = "Transport",
                    Name = "Đưa đón sân bay",
                    Price = 500000,
                    unit = "chuyến",
                    CreateAt = DateTime.Now
                },
                new Services
                {
                    ServiceType = "Food",
                    Name = "Minibar",
                    Price = 100000,
                    unit = "lần",
                    CreateAt = DateTime.Now
                }
            };
            await context.Services.AddRangeAsync(services);
            await context.SaveChangesAsync();

            // Seed Discounts
            var discounts = new List<Discount>
            {
                new Discount
                {
                    Name = "WELCOME10",
                    DiscountType = "Percentage",
                    DiscountValue = 10,
                    FromDate = DateTime.Now,
                    ToDate = DateTime.Now.AddMonths(3),
                    IsActive = true,
                    CreateAt = DateTime.Now
                },
                new Discount
                {
                    Name = "SUMMER20",
                    DiscountType = "Percentage",
                    DiscountValue = 20,
                    FromDate = DateTime.Now,
                    ToDate = DateTime.Now.AddMonths(2),
                    IsActive = true,
                    CreateAt = DateTime.Now
                },
                new Discount
                {
                    Name = "LONGSTAY15",
                    DiscountType = "Percentage",
                    DiscountValue = 15,
                    FromDate = DateTime.Now,
                    ToDate = DateTime.Now.AddMonths(6),
                    IsActive = true,
                    CreateAt = DateTime.Now
                }
            };
            await context.Discounts.AddRangeAsync(discounts);
            await context.SaveChangesAsync();
        }
    }
}
