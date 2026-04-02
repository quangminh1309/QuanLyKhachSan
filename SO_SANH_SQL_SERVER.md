# 🔍 SO SÁNH CÁC SQL SERVER INSTANCES

## 📊 BẢNG SO SÁNH

| Tính năng | `MINH05` | `(localdb)\MSSQLLocalDB` |
|-----------|----------|--------------------------|
| **Loại** | SQL Server Express/Full | SQL Server LocalDB |
| **Tên máy** | MINH05 (tên máy tính của bạn) | LocalDB instance |
| **Cài đặt** | Cài riêng SQL Server | Tự động với Visual Studio |
| **Chạy** | Luôn chạy (Windows Service) | Chỉ chạy khi cần |
| **Quản lý** | SQL Server Management Studio | Visual Studio hoặc SSMS |
| **Kích thước** | ~2-5 GB | ~50 MB |
| **Hiệu năng** | Cao | Trung bình |
| **Dùng cho** | Production, nhiều user | Development, 1 user |
| **Port** | 1433 (mặc định) | Không có port cố định |
| **Remote access** | Có thể | Không |

---

## 🖥️ `MINH05` - SQL SERVER EXPRESS/FULL

### Đây là gì?
- **MINH05** là tên máy tính của bạn
- SQL Server được cài đặt trên máy này
- Instance name có thể là: `MINH05`, `MINH05\SQLEXPRESS`, hoặc `MINH05\MSSQLSERVER`

### Đặc điểm:
```
✅ SQL Server đầy đủ
✅ Chạy như Windows Service (luôn bật)
✅ Có thể truy cập từ máy khác (nếu config)
✅ Quản lý bằng SSMS
✅ Hiệu năng cao
✅ Phù hợp cho production
```

### Khi nào dùng:
- Khi deploy lên server thật
- Khi nhiều người cùng dùng
- Khi cần hiệu năng cao
- Khi cần remote access

### Connection String:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=MINH05;Database=ManagerDB;Trusted_Connection=True;"
  }
}
```

Hoặc với instance name:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=MINH05\\SQLEXPRESS;Database=ManagerDB;Trusted_Connection=True;"
  }
}
```

---

## 💻 `(localdb)\MSSQLLocalDB` - SQL SERVER LOCALDB

### Đây là gì?
- **LocalDB** là phiên bản nhẹ của SQL Server
- Được cài tự động với Visual Studio hoặc .NET SDK
- Chỉ dùng cho development

### Đặc điểm:
```
✅ Nhẹ (~50MB)
✅ Tự động chạy khi cần
✅ Tự động tắt khi không dùng
✅ Không cần cài đặt riêng
✅ Dễ dùng cho dev
❌ Không dùng cho production
❌ Không remote access
❌ Hiệu năng thấp hơn
```

### Khi nào dùng:
- Khi đang phát triển (development)
- Khi làm việc một mình
- Khi không muốn cài SQL Server đầy đủ
- Khi cần database nhẹ, đơn giản

### Connection String:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=ManagerDB;Trusted_Connection=True;"
  }
}
```

---

## 🎯 TẠI SAO DỰ ÁN DÙNG LocalDB?

### Lý do:

1. **Đơn giản cho sinh viên**
   - Không cần cài SQL Server riêng
   - Tự động có sẵn với Visual Studio
   - Không cần config phức tạp

2. **Nhẹ**
   - Chỉ ~50MB
   - Không chiếm tài nguyên khi không dùng
   - Phù hợp cho laptop sinh viên

3. **Đủ dùng cho học tập**
   - Giống SQL Server 99%
   - Đủ để học và làm đồ án
   - Dễ demo cho giáo viên

4. **Tự động**
   - Không cần start/stop service
   - Tự chạy khi backend chạy
   - Tự tắt khi không dùng

---

## 🔄 CHUYỂN ĐỔI GIỮA CÁC SERVER

### Từ LocalDB → SQL Server Express (MINH05)

**Bước 1: Kiểm tra SQL Server có chạy không**

```bash
# Mở Services (Win + R → services.msc)
# Tìm "SQL Server (SQLEXPRESS)" hoặc "SQL Server (MSSQLSERVER)"
# Đảm bảo nó đang Running
```

**Bước 2: Sửa Connection String**

File: `Manager/Manager.API/appsettings.json`

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=MINH05\\SQLEXPRESS;Database=ManagerDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

**Bước 3: Chạy lại Migration**

```bash
cd Manager/Manager.API
dotnet ef database update
```

Database sẽ được tạo trên `MINH05\SQLEXPRESS`!

---

### Từ SQL Server Express → LocalDB

**Bước 1: Sửa Connection String**

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=ManagerDB;Trusted_Connection=True;"
  }
}
```

**Bước 2: Chạy lại Migration**

```bash
dotnet ef database update
```

---

## 🔍 CÁCH KIỂM TRA CÁC SQL SERVER INSTANCES

### Cách 1: PowerShell

```powershell
# Xem tất cả SQL Server instances
Get-Service | Where-Object {$_.Name -like "*SQL*"}
```

**Output mẫu:**
```
Status   Name               DisplayName
------   ----               -----------
Running  MSSQL$SQLEXPRESS   SQL Server (SQLEXPRESS)
Running  MSSQLLocalDB       SQL Server LocalDB
```

### Cách 2: Command Line

```bash
# Xem LocalDB instances
sqllocaldb info

# Xem chi tiết LocalDB
sqllocaldb info MSSQLLocalDB
```

**Output:**
```
Name:               MSSQLLocalDB
Version:            15.0.2000.5
Shared name:
Owner:              MINH05\luudi
Auto-create:        Yes
State:              Running
```

### Cách 3: SQL Server Configuration Manager

1. Mở **SQL Server Configuration Manager**
2. Xem **SQL Server Services**
3. Thấy tất cả instances đang chạy

---

## 💡 KHUYẾN NGHỊ

### Cho Sinh Viên / Học Tập:
✅ **Dùng LocalDB** - Đơn giản, nhẹ, đủ dùng

### Cho Đồ Án / Demo:
✅ **Dùng LocalDB** - Dễ setup, không cần config

### Cho Production / Deploy:
✅ **Dùng SQL Server Express/Full** - Hiệu năng cao, ổn định

### Cho Làm Việc Nhóm:
✅ **Dùng SQL Server Express** - Nhiều người cùng truy cập

---

## 🎓 TÓM TẮT

### `MINH05` (SQL Server Express/Full):
```
✅ SQL Server đầy đủ
✅ Hiệu năng cao
✅ Dùng cho production
❌ Cần cài đặt riêng
❌ Nặng (~2-5GB)
```

### `(localdb)\MSSQLLocalDB` (LocalDB):
```
✅ Nhẹ (~50MB)
✅ Tự động có sẵn
✅ Đủ dùng cho dev
✅ Đơn giản
❌ Không dùng production
❌ Hiệu năng thấp hơn
```

---

## 🚀 KẾT LUẬN

**Dự án của bạn dùng LocalDB vì:**

1. ✅ Đơn giản cho sinh viên
2. ✅ Không cần cài SQL Server riêng
3. ✅ Nhẹ, không tốn tài nguyên
4. ✅ Đủ dùng cho học tập và đồ án
5. ✅ Dễ demo cho giáo viên

**Nếu muốn chuyển sang SQL Server đầy đủ:**
- Chỉ cần sửa Connection String
- Chạy lại `dotnet ef database update`
- Xong!

---

**Cả hai đều là SQL Server thật, chỉ khác phiên bản!** 🎯
