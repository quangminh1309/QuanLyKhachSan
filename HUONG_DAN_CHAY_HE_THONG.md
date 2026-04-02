# 🚀 HƯỚNG DẪN CHẠY HỆ THỐNG

## ✅ TRẠNG THÁI: HỆ THỐNG ĐÃ SẴN SÀNG!

Hệ thống đã được hoàn thiện và sẵn sàng để chạy!

---

## 📋 CHECKLIST ĐÃ HOÀN THÀNH

- ✅ Database đã được tạo và cập nhật
- ✅ Migration đã chạy thành công
- ✅ Seed data đã được load (4 loại phòng, 15 phòng, 5 dịch vụ, 3 mã giảm giá)
- ✅ Backend đang chạy tại `http://localhost:5162`
- ✅ Các chức năng mới đã được bổ sung:
  - Xem lịch sử đặt phòng
  - Yêu cầu hoàn tiền
  - Hủy yêu cầu hoàn tiền
  - Xem đánh giá theo phòng
  - Xem đánh giá của tôi
  - Xem sự cố của tôi

---

## 🎯 CÁCH CHẠY HỆ THỐNG

### 1. Backend (Đang chạy)

Backend đã được khởi động tại: `http://localhost:5162`

Nếu cần restart:
```bash
# Dừng backend (Ctrl+C)
# Sau đó chạy lại:
cd Manager/Manager.API
dotnet run
```

### 2. Frontend

Mở terminal mới và chạy:
```bash
cd "React/Holtel manager"
npm run dev
```

Frontend sẽ chạy tại: `http://localhost:5173`

---

## 🧪 TEST API VỚI POSTMAN

### Bước 1: Import Collection

File: `Hotel-Manager-API.postman_collection.json`

### Bước 2: Đăng ký tài khoản

```http
POST http://localhost:5162/api/Account/register
Content-Type: application/json

{
  "username": "testuser",
  "email": "test@example.com",
  "password": "Test@123"
}
```

### Bước 3: Đăng nhập và lưu token

```http
POST http://localhost:5162/api/Account/login
Content-Type: application/json

{
  "username": "testuser",
  "password": "Test@123"
}
```

**Trong tab Tests của Postman, thêm script:**
```javascript
pm.environment.set("token", pm.response.json().token);
```

### Bước 4: Test các endpoint

Sử dụng token trong header:
```
Authorization: Bearer {{token}}
```

---

## 📊 DỮ LIỆU MẪU ĐÃ CÓ

### Room Types (4 loại)
- Standard - Capacity: 2
- Deluxe - Capacity: 2
- Suite - Capacity: 4
- Family - Capacity: 6

### Rooms (15 phòng)
- Tầng 1: 101-105 (Standard)
- Tầng 2: 201-205 (Deluxe)
- Tầng 3: 301-303 (Suite), 304-305 (Family)

### Services (5 dịch vụ)
- Giặt ủi - 50,000 VNĐ/kg
- Ăn sáng - 150,000 VNĐ/suất
- Massage - 300,000 VNĐ/giờ
- Đưa đón sân bay - 500,000 VNĐ/chuyến
- Minibar - 100,000 VNĐ/lần

### Discounts (3 mã)
- WELCOME10 - Giảm 10%
- SUMMER20 - Giảm 20%
- LONGSTAY15 - Giảm 15%

---

## 🔍 KIỂM TRA DỮ LIỆU

### Test 1: Lấy danh sách Room Types
```http
GET http://localhost:5162/api/RoomType
Authorization: Bearer {{token}}
```

### Test 2: Lấy danh sách Rooms
```http
GET http://localhost:5162/api/room
Authorization: Bearer {{token}}
```

### Test 3: Lấy danh sách Services
```http
GET http://localhost:5162/api/Services
Authorization: Bearer {{token}}
```

### Test 4: Lấy danh sách Discounts
```http
GET http://localhost:5162/api/Discount
Authorization: Bearer {{token}}
```

---

## 🆕 TEST CHỨC NĂNG MỚI

### 1. Xem lịch sử đặt phòng
```http
GET http://localhost:5162/api/Booking/history
Authorization: Bearer {{token}}
```

### 2. Yêu cầu hoàn tiền
```http
POST http://localhost:5162/api/Booking/{id}/request-refund
Authorization: Bearer {{token}}
Content-Type: application/json

{
  "reason": "Lý do yêu cầu hoàn tiền"
}
```

### 3. Hủy yêu cầu hoàn tiền
```http
POST http://localhost:5162/api/Booking/{id}/cancel-refund-request
Authorization: Bearer {{token}}
```

### 4. Xem đánh giá theo phòng
```http
GET http://localhost:5162/api/Review/by-room/{roomId}
Authorization: Bearer {{token}}
```

### 5. Xem đánh giá của tôi
```http
GET http://localhost:5162/api/Review/my-reviews
Authorization: Bearer {{token}}
```

### 6. Xem sự cố của tôi
```http
GET http://localhost:5162/api/Incident/my-incidents
Authorization: Bearer {{token}}
```

---

## 📁 TÀI LIỆU THAM KHẢO

- `LUONG_HOAT_DONG.md` - Giải thích luồng hoạt động hệ thống
- `CHUC_NANG_HE_THONG.md` - Danh sách đầy đủ chức năng
- `CHUC_NANG_MOI.md` - Chi tiết các chức năng mới
- `API_ENDPOINTS.md` - Danh sách tất cả API endpoints
- `HUONG_DAN_TEST_API.md` - Hướng dẫn test API với Postman
- `GIAI_THICH_DATABASE.md` - Giải thích về database
- `Hotel-Manager-API.postman_collection.json` - Postman collection

---

## ⚠️ LƯU Ý

### Database
- Sử dụng SQL Server LocalDB
- Connection string: `Server=(localdb)\\MSSQLLocalDB;Database=ManagerDB`
- Truy cập qua Visual Studio SQL Server Object Explorer

### Ports
- Backend: `http://localhost:5162`
- Frontend: `http://localhost:5173`

### Roles
- Admin: Quản lý toàn bộ hệ thống
- Manager: Quản lý khách sạn
- Guest: Khách hàng

---

## 🐛 TROUBLESHOOTING

### Backend không chạy?
```bash
cd Manager/Manager.API
dotnet build
dotnet run
```

### Database lỗi?
```bash
cd Manager/Manager.API
dotnet ef database drop -f
dotnet ef database update
dotnet run
```

### Frontend lỗi?
```bash
cd "React/Holtel manager"
npm install
npm run dev
```

---

## 🎉 HOÀN THÀNH!

Hệ thống đã sẵn sàng để sử dụng. Chúc bạn test thành công!

**Backend:** ✅ Đang chạy  
**Database:** ✅ Đã có dữ liệu  
**Seed Data:** ✅ Đã load  
**Chức năng mới:** ✅ Đã bổ sung  

Bắt đầu test ngay! 🚀
