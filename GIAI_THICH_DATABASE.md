# 🗄️ GIẢI THÍCH VỀ DATABASE

## ❓ CÂU HỎI: Database có dùng SQL Server không?

**Trả lời: CÓ! Hệ thống ĐANG DÙNG SQL Server LocalDB**

---

## 🔍 PHÂN TÍCH CHI TIẾT

### 1. Connection String trong `appsettings.json`

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=ManagerDB;Trusted_Connection=True;"
  }
}
```

**Giải thích từng phần:**

| Phần | Ý nghĩa |
|------|---------|
| `Server=(localdb)\\MSSQLLocalDB` | Dùng SQL Server LocalDB (cài sẵn với Visual Studio) |
| `Database=ManagerDB` | Tên database là `ManagerDB` |
| `Trusted_Connection=True` | Dùng Windows Authentication (không cần username/password) |

---

## 🏗️ SQL Server LocalDB LÀ GÌ?

### Định nghĩa:
**SQL Server LocalDB** là phiên bản nhẹ của SQL Server, được cài đặt tự động khi bạn cài:
- Visual Studio
- .NET SDK
- SQL Server Express

### Đặc điểm:
- ✅ Là SQL Server thật 100%
- ✅ Chạy tự động khi cần
- ✅ Không cần cài đặt riêng
- ✅ Không cần SQL Server Management Studio (SSMS)
- ✅ Lưu file `.mdf` trong máy tính

### Vị trí file database:
```
C:\Users\{YourUsername}\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\
```

Hoặc:
```
C:\Users\{YourUsername}\
```

---

## 🔄 QUY TRÌNH TẠO DATABASE

### Bước 1: Entity Framework Core Migrations

Khi bạn chạy lệnh:
```bash
dotnet ef database update
```

**Điều gì xảy ra:**

1. **EF Core đọc Models** (Room, RoomType, Booking, v.v.)
2. **Tạo file Migration** (nếu chưa có)
3. **Kết nối đến SQL Server LocalDB**
4. **Tạo database `ManagerDB`** (nếu chưa có)
5. **Chạy các câu SQL** để tạo bảng

### Bước 2: SQL được tạo tự động

EF Core tự động generate SQL như:

```sql
-- Tạo database
CREATE DATABASE ManagerDB;

-- Tạo bảng RoomTypes
CREATE TABLE RoomTypes (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(500),
    Capacity INT NOT NULL,
    BasePrice DECIMAL(18,2) NOT NULL,
    CreateAt DATETIME2 NOT NULL,
    UpdateAt DATETIME2 NOT NULL
);

-- Tạo bảng Rooms
CREATE TABLE Rooms (
    Id INT PRIMARY KEY IDENTITY(1,1),
    RoomNumber NVARCHAR(50) NOT NULL,
    Floor INT NOT NULL,
    RoomTypeId INT NOT NULL,
    CurrentStatus NVARCHAR(50) NOT NULL,
    CreateAt DATETIME2 NOT NULL,
    UpdateAt DATETIME2 NOT NULL,
    FOREIGN KEY (RoomTypeId) REFERENCES RoomTypes(Id)
);

-- ... và 14 bảng khác
```

### Bước 3: Seed Data tự động

Khi backend chạy lần đầu, `SeedData.cs` sẽ:

```csharp
// Kiểm tra database có trống không
if (await context.RoomTypes.AnyAsync())
{
    return; // Đã có data, không seed nữa
}

// Insert data vào SQL Server
INSERT INTO RoomTypes (Name, Description, Capacity, BasePrice, CreateAt)
VALUES ('Standard', 'Phòng tiêu chuẩn', 2, 500000, GETDATE());

INSERT INTO Rooms (RoomNumber, Floor, RoomTypeId, CurrentStatus, CreateAt)
VALUES ('101', 1, 1, 'Available', GETDATE());
```

---

## 📊 CẤU TRÚC DATABASE THỰC TẾ

### Database: `ManagerDB`

```
ManagerDB
├── Tables (16 bảng)
│   ├── AspNetUsers              # Bảng user (Identity)
│   ├── AspNetRoles              # Bảng roles
│   ├── AspNetUserRoles          # Liên kết user-role
│   ├── RoomTypes                # Loại phòng
│   ├── Rooms                    # Phòng
│   ├── Bookings                 # Đặt phòng
│   ├── Services                 # Dịch vụ
│   ├── Discounts                # Giảm giá
│   ├── Invoices                 # Hóa đơn
│   ├── Payments                 # Thanh toán
│   ├── Reviews                  # Đánh giá
│   ├── Incidents                # Sự cố
│   ├── LostItems                # Đồ thất lạc
│   ├── RoomRates                # Giá phòng theo thời gian
│   ├── SupportChats             # Chat hỗ trợ
│   └── __EFMigrationsHistory    # Lịch sử migrations
│
└── Stored Procedures (0)
```

---

## 🔍 CÁCH XEM DATABASE

### Cách 1: SQL Server Object Explorer (Visual Studio)

1. Mở Visual Studio
2. View → SQL Server Object Explorer
3. Mở `(localdb)\MSSQLLocalDB`
4. Mở `Databases` → `ManagerDB`
5. Xem Tables, Data

### Cách 2: SQL Server Management Studio (SSMS)

1. Mở SSMS
2. Server name: `(localdb)\MSSQLLocalDB`
3. Authentication: Windows Authentication
4. Connect
5. Xem database `ManagerDB`

### Cách 3: Command Line

```bash
# Kết nối đến LocalDB
sqlcmd -S "(localdb)\MSSQLLocalDB"

# Xem danh sách database
SELECT name FROM sys.databases;
GO

# Dùng database ManagerDB
USE ManagerDB;
GO

# Xem danh sách bảng
SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES;
GO

# Xem dữ liệu trong bảng Rooms
SELECT * FROM Rooms;
GO
```

### Cách 4: Dùng EF Core CLI

```bash
cd Manager/Manager.API

# Xem connection string
dotnet ef dbcontext info

# Xem danh sách migrations
dotnet ef migrations list

# Tạo script SQL từ migrations
dotnet ef migrations script > database.sql
```

---

## 🎯 SO SÁNH: LocalDB vs SQL Server đầy đủ

| Tính năng | LocalDB | SQL Server Full |
|-----------|---------|-----------------|
| Cài đặt | Tự động với VS/.NET | Cần cài riêng |
| Kích thước | ~50 MB | ~2-5 GB |
| Chạy | Tự động khi cần | Chạy liên tục |
| Quản lý | Không cần SSMS | Dùng SSMS |
| Hiệu năng | Đủ cho dev | Tối ưu cho production |
| Phù hợp | Development | Production |
| Giống SQL Server | 99% | 100% |

---

## 💡 TẠI SAO DÙNG LocalDB?

### Ưu điểm:
1. ✅ **Không cần cài đặt** - Có sẵn với Visual Studio
2. ✅ **Nhẹ** - Chỉ ~50MB
3. ✅ **Tự động** - Chạy khi cần, tắt khi không dùng
4. ✅ **Đủ dùng** - Giống SQL Server 99%
5. ✅ **Dễ dàng** - Không cần config phức tạp

### Nhược điểm:
1. ❌ Không dùng cho production
2. ❌ Không có GUI quản lý (cần SSMS)
3. ❌ Hiệu năng thấp hơn SQL Server đầy đủ

---

## 🔄 CHUYỂN SANG SQL SERVER ĐẦY ĐỦ

Nếu muốn dùng SQL Server thật (không phải LocalDB):

### Bước 1: Cài SQL Server Express

Download: https://www.microsoft.com/en-us/sql-server/sql-server-downloads

### Bước 2: Sửa Connection String

File: `Manager/Manager.API/appsettings.json`

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=ManagerDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

Hoặc với username/password:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=ManagerDB;User Id=sa;Password=YourPassword;TrustServerCertificate=True;"
  }
}
```

### Bước 3: Chạy lại Migration

```bash
cd Manager/Manager.API
dotnet ef database update
```

Database sẽ được tạo trên SQL Server thật!

---

## 📝 TÓM TẮT

### Câu trả lời ngắn gọn:

**Có, hệ thống ĐANG DÙNG SQL Server!**

- ✅ Dùng SQL Server LocalDB (phiên bản nhẹ)
- ✅ Database tên `ManagerDB`
- ✅ Có 16 bảng thật
- ✅ Dữ liệu lưu trong file `.mdf`
- ✅ Entity Framework Core tự động tạo bảng
- ✅ Seed data tự động insert dữ liệu

### Quy trình:

```
1. Bạn chạy: dotnet ef database update
   ↓
2. EF Core kết nối SQL Server LocalDB
   ↓
3. Tạo database ManagerDB
   ↓
4. Tạo 16 bảng bằng SQL
   ↓
5. Bạn chạy: dotnet run
   ↓
6. SeedData.cs insert dữ liệu mẫu
   ↓
7. Database sẵn sàng sử dụng!
```

---

## 🔍 KIỂM TRA DATABASE CÓ TỒN TẠI

### Cách 1: Dùng lệnh

```bash
cd Manager/Manager.API
dotnet ef dbcontext info
```

**Output:**
```
Provider name: Microsoft.EntityFrameworkCore.SqlServer
Database name: ManagerDB
Data source: (localdb)\MSSQLLocalDB
Options: None
```

### Cách 2: Xem file

Tìm file `.mdf` trong:
```
C:\Users\{YourUsername}\
```

Tên file: `ManagerDB.mdf`

---

**Hy vọng giải thích này giúp bạn hiểu rõ về database! 🎓**
