# 🏨 HOTEL MANAGER SYSTEM

Hệ thống quản lý khách sạn với Backend .NET 9.0 và Frontend React 19.

---

## 📚 TÀI LIỆU HƯỚNG DẪN

| File | Mô tả | Khi nào dùng |
|------|-------|--------------|
| `SETUP_DATABASE.md` | Setup database & seed data | ⚠️ ĐỌC ĐẦU TIÊN! |
| `HUONG_DAN_TEST_API.md` | Test API với Postman | Test backend |
| `API_ENDPOINTS.md` | Danh sách 60+ endpoints | Tra cứu API |
| `LUONG_HOAT_DONG.md` | Giải thích luồng hoạt động | Hiểu kiến trúc |
| `CHUC_NANG_HE_THONG.md` | Chức năng đã có & cần thêm | Roadmap |
| `Hotel-Manager-API.postman_collection.json` | Postman collection | Import vào Postman |

---

## ⚡ CHẠY HỆ THỐNG (3 PHÚT)

### 1. Setup Database (LẦN ĐẦU)
```bash
cd Manager/Manager.API
dotnet ef database update
```

### 2. Chạy Backend
```bash
cd Manager/Manager.API
dotnet run
```
✅ Chờ thấy: `Now listening on: http://localhost:5162`

### 3. Chạy Frontend (Terminal mới)
```bash
cd "React/Holtel manager"
npm run dev
```
✅ Chờ thấy: `Local: http://localhost:5173/`

### 4. Mở Browser
- Vào: `http://localhost:5173`
- Nhấn `Ctrl + Shift + R` (Hard Refresh)

---

## 🎯 CHỨC NĂNG CHÍNH

### ✅ Đã hoàn thành (85%)
- Authentication & Authorization (JWT)
- Quản lý Phòng & Loại Phòng (CRUD đầy đủ)
- Đặt phòng & Hủy đặt phòng
- Quản lý Dịch vụ (CRUD đầy đủ)
- Quản lý Giảm giá (CRUD đầy đủ)
- Đánh giá (CRUD đầy đủ)
- Báo cáo Sự cố (CRUD đầy đủ)
- Check In/Out, Chuyển phòng, Gia hạn
- Quản lý Hóa đơn & Thanh toán
- Profile & Đổi mật khẩu
- Báo cáo Doanh thu

### 📊 Dữ liệu mẫu (Seed Data)
- 4 loại phòng (Standard, Deluxe, Suite, Family)
- 15 phòng (Tầng 1-3)
- 5 dịch vụ (Giặt ủi, Ăn sáng, Massage, v.v.)
- 3 mã giảm giá (WELCOME10, SUMMER20, LONGSTAY15)

---

## 🧪 TEST API

### Cách 1: Import Postman Collection
1. Mở Postman
2. Import file `Hotel-Manager-API.postman_collection.json`
3. Tạo Environment `Hotel Manager` với variable `token`
4. Test!

### Cách 2: Test thủ công
Xem file `HUONG_DAN_TEST_API.md` để biết chi tiết.

### Workflow test cơ bản:
```
1. Register → Lấy token
2. Get Room Types → Xem loại phòng
3. Get Rooms → Xem danh sách phòng
4. Create Booking → Đặt phòng
5. Get My Bookings → Xem đặt phòng của tôi
```

---

## 🛠️ CÔNG NGHỆ

### Backend
- .NET 9.0
- Entity Framework Core
- SQL Server LocalDB
- JWT Authentication
- Repository Pattern

### Frontend
- React 19
- TypeScript
- Vite
- React Router DOM
- Axios
- Context API

---

## 📂 CẤU TRÚC DỰ ÁN

```
├── Manager/Manager.API/          # Backend .NET
│   ├── Controllers/              # API endpoints
│   ├── Models/                   # Database models
│   ├── Repository/               # Data access
│   ├── Dtos/                     # Data transfer objects
│   ├── Mappers/                  # Object mapping
│   └── Data/                     # DbContext & Seed
│
├── React/Holtel manager/         # Frontend React
│   └── src/
│       ├── pages/                # Page components
│       ├── services/             # API services
│       ├── contexts/             # React contexts
│       ├── shared/               # Shared components
│       └── assets/               # CSS & static files
│
└── *.md                          # Documentation
```

---

## 🔐 AUTHENTICATION

### Roles
- **Guest**: Khách hàng (mặc định khi đăng ký)
- **Manager**: Quản lý khách sạn
- **Admin**: Quản trị viên

### Password Requirements
- Tối thiểu 8 ký tự
- Có chữ hoa, chữ thường
- Có số và ký tự đặc biệt

---

## 🐛 GẶP VẤN ĐỀ?

### Không có dữ liệu phòng?
→ Xem `SETUP_DATABASE.md`

### Trang web trắng?
→ Nhấn `Ctrl + Shift + R` để hard refresh

### Lỗi 401 Unauthorized?
→ Token hết hạn, đăng nhập lại

### Backend không chạy?
→ Kiểm tra database đã setup chưa

---

## 📞 HỖ TRỢ

Nếu gặp vấn đề, cung cấp:
1. Screenshot lỗi
2. Output terminal Backend
3. Output terminal Frontend
4. Các bước đã thử

---

**Hệ thống sẵn sàng sử dụng! 🚀**

*Phát triển bởi sinh viên với ❤️*
