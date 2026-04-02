# 🗄️ HƯỚNG DẪN SETUP DATABASE

## ⚠️ VẤN ĐỀ: Không có dữ liệu phòng, loại phòng

### Nguyên nhân:
- Database chưa được tạo
- Migration chưa chạy
- Seed data chưa được load

---

## ✅ GIẢI PHÁP: Chạy lệnh sau

### Bước 1: Mở Terminal/PowerShell

```bash
cd Manager/Manager.API
```

### Bước 2: Xóa database cũ (nếu có)

```bash
dotnet ef database drop -f
```

### Bước 3: Tạo database mới và chạy migrations

```bash
dotnet ef database update
```

### Bước 4: Chạy backend

```bash
dotnet run
```

**Kết quả:** Seed data sẽ tự động chạy khi backend khởi động!

---

## 📊 DỮ LIỆU ĐƯỢC TẠO TỰ ĐỘNG

### 1. Room Types (4 loại phòng):
- **Standard** - 500,000 VNĐ/đêm (2 người)
- **Deluxe** - 800,000 VNĐ/đêm (2 người)
- **Suite** - 1,500,000 VNĐ/đêm (4 người)
- **Family** - 1,200,000 VNĐ/đêm (6 người)

### 2. Rooms (15 phòng):
- **Tầng 1:** 101-105 (Standard)
- **Tầng 2:** 201-205 (Deluxe)
- **Tầng 3:** 301-303 (Suite), 304-305 (Family)

### 3. Services (5 dịch vụ):
- Giặt ủi - 50,000 VNĐ
- Ăn sáng - 150,000 VNĐ
- Massage - 300,000 VNĐ
- Đưa đón sân bay - 500,000 VNĐ
- Minibar - 100,000 VNĐ

### 4. Discounts (3 mã giảm giá):
- **WELCOME10** - Giảm 10%
- **SUMMER20** - Giảm 20%
- **LONGSTAY15** - Giảm 15%

---

## 🧪 KIỂM TRA DỮ LIỆU

### Test 1: Lấy danh sách Room Types

```http
GET http://localhost:5162/api/RoomType
Authorization: Bearer YOUR_TOKEN
```

**Kết quả mong đợi:** 4 loại phòng

### Test 2: Lấy danh sách Rooms

```http
GET http://localhost:5162/api/room
Authorization: Bearer YOUR_TOKEN
```

**Kết quả mong đợi:** 15 phòng

### Test 3: Lấy danh sách Services

```http
GET http://localhost:5162/api/Services
Authorization: Bearer YOUR_TOKEN
```

**Kết quả mong đợi:** 5 dịch vụ

### Test 4: Lấy danh sách Discounts

```http
GET http://localhost:5162/api/Discount
Authorization: Bearer YOUR_TOKEN
```

**Kết quả mong đợi:** 3 mã giảm giá

---

## 🔍 TROUBLESHOOTING

### Lỗi: "Cannot open database"
```bash
# Xóa và tạo lại
dotnet ef database drop -f
dotnet ef database update
```

### Lỗi: "No such table"
```bash
# Chạy lại migrations
dotnet ef database update
```

### Vẫn không có dữ liệu?
1. Kiểm tra backend có chạy không
2. Kiểm tra Console log có lỗi không
3. Restart backend: `Ctrl+C` rồi `dotnet run`

---

## 📝 GHI CHÚ

- Seed data chỉ chạy 1 lần khi database trống
- Nếu đã có data, seed data sẽ không chạy lại
- Muốn reset data: Xóa database và chạy lại migration

---

**Sau khi setup xong, test API để xem dữ liệu!** 🚀
