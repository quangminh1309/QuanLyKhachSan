# 🧪 HƯỚNG DẪN TEST API VỚI POSTMAN

## 🚀 SETUP BAN ĐẦU

### Bước 1: Chạy Backend
```bash
cd Manager/Manager.API
dotnet run
```
✅ Đợi thấy: `Now listening on: http://localhost:5162`

### Bước 2: Tạo Environment trong Postman
1. Click **Environments** (biểu tượng mắt)
2. Click **Create Environment**
3. Tên: `Hotel Manager`
4. Thêm variable `token` (để trống)
5. **Save** và chọn environment này

---

## 📋 TEST WORKFLOW CHUẨN

### 1️⃣ ĐĂNG KÝ TÀI KHOẢN

**Request:**
```http
POST http://localhost:5162/api/Account/register
Content-Type: application/json

{
  "username": "testuser",
  "email": "test@example.com",
  "password": "Test@123456"
}
```

**Tab Tests (Auto-save token):**
```javascript
if (pm.response.code === 200) {
    var jsonData = pm.response.json();
    pm.environment.set("token", jsonData.token);
    console.log("✅ Token saved:", jsonData.token);
}
```

**Response:**
```json
{
  "userName": "testuser",
  "email": "test@example.com",
  "token": "eyJhbGc..."
}
```

---

### 2️⃣ XEM DANH SÁCH LOẠI PHÒNG

**Request:**
```http
GET http://localhost:5162/api/RoomType
Authorization: Bearer {{token}}
```

**Response:**
```json
[
  {
    "id": 1,
    "name": "Standard",
    "description": "Phòng tiêu chuẩn với đầy đủ tiện nghi cơ bản",
    "capacity": 2,
    "basePrice": 500000
  },
  {
    "id": 2,
    "name": "Deluxe",
    "description": "Phòng cao cấp với view đẹp",
    "capacity": 2,
    "basePrice": 800000
  }
]
```

---

### 3️⃣ XEM DANH SÁCH PHÒNG

**Request:**
```http
GET http://localhost:5162/api/room
Authorization: Bearer {{token}}
```

**Response:**
```json
[
  {
    "id": 1,
    "roomNumber": "101",
    "roomTypeId": 1,
    "currentStatus": "Available",
    "createAt": "2025-01-01T00:00:00"
  },
  {
    "id": 2,
    "roomNumber": "102",
    "roomTypeId": 1,
    "currentStatus": "Available",
    "createAt": "2025-01-01T00:00:00"
  }
]
```

---

### 4️⃣ ĐẶT PHÒNG

**Request:**
```http
POST http://localhost:5162/api/Booking?IdRoom=1
Authorization: Bearer {{token}}
Content-Type: application/json

{
  "checkInDate": "2025-04-01",
  "checkOutDate": "2025-04-05",
  "numberOfGuests": 2,
  "specialRequests": "Phòng tầng cao, view đẹp"
}
```

**Response:**
```json
{
  "id": 1,
  "roomId": 1,
  "checkInDate": "2025-04-01",
  "checkOutDate": "2025-04-05",
  "numberOfGuests": 2,
  "totalPrice": 2000000,
  "status": "Pending",
  "specialRequests": "Phòng tầng cao, view đẹp"
}
```

---

### 5️⃣ XEM ĐẶT PHÒNG CỦA TÔI

**Request:**
```http
GET http://localhost:5162/api/Booking/my-bookings
Authorization: Bearer {{token}}
```

**Response:**
```json
[
  {
    "id": 1,
    "roomId": 1,
    "checkInDate": "2025-04-01",
    "checkOutDate": "2025-04-05",
    "numberOfGuests": 2,
    "totalPrice": 2000000,
    "status": "Pending"
  }
]
```

---

### 6️⃣ XEM DANH SÁCH DỊCH VỤ

**Request:**
```http
GET http://localhost:5162/api/Services
Authorization: Bearer {{token}}
```

**Response:**
```json
[
  {
    "id": 1,
    "name": "Giặt ủi",
    "description": "Dịch vụ giặt ủi quần áo",
    "price": 50000
  },
  {
    "id": 2,
    "name": "Ăn sáng",
    "description": "Buffet sáng tại nhà hàng",
    "price": 150000
  }
]
```

---

### 7️⃣ TẠO ĐÁNH GIÁ

**Request:**
```http
POST http://localhost:5162/api/Review
Authorization: Bearer {{token}}
Content-Type: application/json

{
  "bookingId": 1,
  "rating": 5,
  "comment": "Phòng sạch sẽ, nhân viên thân thiện!"
}
```

**Response:**
```json
{
  "id": 1,
  "bookingId": 1,
  "rating": 5,
  "comment": "Phòng sạch sẽ, nhân viên thân thiện!",
  "createAt": "2025-03-28T10:00:00"
}
```

---

### 8️⃣ SỬA ĐÁNH GIÁ

**Request:**
```http
PUT http://localhost:5162/api/Review/1
Authorization: Bearer {{token}}
Content-Type: application/json

{
  "rating": 4,
  "comment": "Phòng tốt nhưng hơi ồn"
}
```

---

### 9️⃣ XÓA ĐÁNH GIÁ

**Request:**
```http
DELETE http://localhost:5162/api/Review/1
Authorization: Bearer {{token}}
```

---

### 🔟 BÁO CÁO SỰ CỐ

**Request:**
```http
POST http://localhost:5162/api/Incident
Authorization: Bearer {{token}}
Content-Type: application/json

{
  "bookingId": 1,
  "description": "Điều hòa không hoạt động"
}
```

**Response:**
```json
{
  "id": 1,
  "bookingId": 1,
  "description": "Điều hòa không hoạt động",
  "reportedAt": "2025-03-28T10:00:00",
  "status": "Pending"
}
```

---

### 1️⃣1️⃣ HỦY ĐẶT PHÒNG

**Request:**
```http
DELETE http://localhost:5162/api/Booking/1
Authorization: Bearer {{token}}
```

**Response:**
```json
{
  "id": 1,
  "status": "Cancelled"
}
```

---

## 🎯 TEST SCENARIOS NÂNG CAO

### Scenario 1: Quy trình đặt phòng hoàn chỉnh
```
1. Register → Lấy token
2. Get Room Types → Chọn loại phòng
3. Get Rooms → Chọn phòng trống
4. Create Booking → Đặt phòng
5. Get My Bookings → Xem đặt phòng
6. Create Review → Đánh giá sau khi ở
```

### Scenario 2: Quản lý sự cố
```
1. Create Incident → Báo cáo sự cố
2. Get Incidents → Xem danh sách (Admin/Manager)
3. Update Incident → Cập nhật trạng thái (Admin/Manager)
4. Delete Incident → Xóa nếu không cần
```

### Scenario 3: Quản lý dịch vụ (Admin/Manager)
```
1. Get Services → Xem danh sách
2. Create Service → Thêm dịch vụ mới
3. Update Service → Sửa giá/mô tả
4. Delete Service → Xóa dịch vụ
```

---

## 💡 TIPS & TRICKS

### 1. Auto-save Token
Thêm vào tab **Tests** của Register/Login:
```javascript
if (pm.response.code === 200) {
    pm.environment.set("token", pm.response.json().token);
}
```

### 2. Sử dụng Token
Ở tất cả request khác, thêm header:
```
Authorization: Bearer {{token}}
```

### 3. Kiểm tra Response
Thêm vào tab **Tests**:
```javascript
pm.test("Status code is 200", function () {
    pm.response.to.have.status(200);
});

pm.test("Response has data", function () {
    var jsonData = pm.response.json();
    pm.expect(jsonData).to.not.be.empty;
});
```

### 4. Log Response
```javascript
console.log(pm.response.json());
```

---

## 🐛 TROUBLESHOOTING

### Lỗi 401 Unauthorized
- Token hết hạn → Đăng nhập lại
- Token sai → Kiểm tra `{{token}}`
- Chưa đăng nhập → Register/Login trước

### Lỗi 404 Not Found
- URL sai → Kiểm tra endpoint
- Backend chưa chạy → `dotnet run`
- ID không tồn tại → Kiểm tra ID

### Lỗi 403 Forbidden
- Không đủ quyền → Cần role Admin/Manager
- Assign role: `POST /Account/assign-role`

### Không có dữ liệu
- Database chưa seed → Xem `SETUP_DATABASE.md`
- Chạy: `dotnet ef database update`

---

## 📊 CHECKLIST TEST

- [ ] Register thành công
- [ ] Login thành công
- [ ] Token được lưu tự động
- [ ] Get Room Types có data (4 loại)
- [ ] Get Rooms có data (15 phòng)
- [ ] Get Services có data (5 dịch vụ)
- [ ] Create Booking thành công
- [ ] Get My Bookings hiển thị đúng
- [ ] Create Review thành công
- [ ] Update Review thành công
- [ ] Delete Review thành công
- [ ] Create Incident thành công
- [ ] Cancel Booking thành công

---

**Chúc bạn test thành công! 🎉**
