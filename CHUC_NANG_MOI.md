# ✨ CHỨC NĂNG MỚI ĐÃ BỔ SUNG

## 📋 TỔNG QUAN

Đã bổ sung **8 API endpoints mới** để hoàn thiện hệ thống!

---

## 🆕 DANH SÁCH CHỨC NĂNG MỚI

### 1. 📅 XEM LỊCH SỬ ĐẶT PHÒNG

**Endpoint:**
```http
GET /api/Booking/history
Headers: Authorization: Bearer {token}
```

**Mô tả:** Xem tất cả đặt phòng của tôi (bao gồm cả đã hủy, đã hoàn thành)

**Response:**
```json
[
  {
    "id": 1,
    "roomId": 101,
    "checkInDate": "2025-04-01",
    "checkOutDate": "2025-04-05",
    "status": "Completed",
    "totalPrice": 2000000
  },
  {
    "id": 2,
    "status": "Cancelled"
  }
]
```

---

### 2. 💰 YÊU CẦU HOÀN TIỀN

**Endpoint:**
```http
POST /api/Booking/{id}/request-refund
Headers: Authorization: Bearer {token}
Content-Type: application/json

Body:
{
  "reason": "Lý do yêu cầu hoàn tiền"
}
```

**Mô tả:** Yêu cầu hoàn tiền cho đặt phòng đã hủy

**Điều kiện:**
- Booking phải ở trạng thái "Cancelled"
- Chỉ chủ booking mới yêu cầu được

---

### 3. ❌ HỦY YÊU CẦU HOÀN TIỀN

**Endpoint:**
```http
POST /api/Booking/{id}/cancel-refund-request
Headers: Authorization: Bearer {token}
```

**Mô tả:** Hủy yêu cầu hoàn tiền đã gửi

---

### 4. ⭐ XEM ĐÁNH GIÁ THEO PHÒNG

**Endpoint:**
```http
GET /api/Review/by-room/{roomId}
Headers: Authorization: Bearer {token}
```

**Mô tả:** Xem tất cả đánh giá của một phòng cụ thể

**Response:**
```json
[
  {
    "id": 1,
    "bookingId": 5,
    "rating": 5,
    "comment": "Phòng rất đẹp!",
    "createAt": "2025-03-28"
  }
]
```

---

### 5. ⭐ XEM ĐÁNH GIÁ CỦA TÔI

**Endpoint:**
```http
GET /api/Review/my-reviews
Headers: Authorization: Bearer {token}
```

**Mô tả:** Xem tất cả đánh giá mà tôi đã viết

**Response:**
```json
[
  {
    "id": 1,
    "bookingId": 5,
    "rating": 5,
    "comment": "Phòng rất đẹp!",
    "createAt": "2025-03-28"
  }
]
```

---

### 6. 🚨 XEM SỰ CỐ CỦA TÔI

**Endpoint:**
```http
GET /api/Incident/my-incidents
Headers: Authorization: Bearer {token}
```

**Mô tả:** Xem tất cả sự cố mà tôi đã báo cáo

**Response:**
```json
[
  {
    "id": 1,
    "bookingId": 5,
    "description": "Điều hòa không hoạt động",
    "status": "Pending",
    "reportedAt": "2025-03-28"
  }
]
```

---

## 🔄 CẬP NHẬT MODEL

### Booking Model - Thêm properties:

```csharp
public bool RefundRequested { get; set; } = false;
public string? RefundReason { get; set; }
public DateTime? RefundRequestedAt { get; set; }
```

**Giải thích:**
- `RefundRequested`: Có yêu cầu hoàn tiền không
- `RefundReason`: Lý do yêu cầu hoàn tiền
- `RefundRequestedAt`: Thời gian yêu cầu

---

## 📊 BẢNG TỔNG HỢP

| STT | Chức năng | Endpoint | Method | Auth |
|-----|-----------|----------|--------|------|
| 1 | Xem lịch sử đặt phòng | `/api/Booking/history` | GET | User |
| 2 | Yêu cầu hoàn tiền | `/api/Booking/{id}/request-refund` | POST | User |
| 3 | Hủy yêu cầu hoàn tiền | `/api/Booking/{id}/cancel-refund-request` | POST | User |
| 4 | Xem đánh giá theo phòng | `/api/Review/by-room/{roomId}` | GET | Public |
| 5 | Xem đánh giá của tôi | `/api/Review/my-reviews` | GET | User |
| 6 | Xem sự cố của tôi | `/api/Incident/my-incidents` | GET | User |

---

## 🧪 TEST SCENARIOS

### Scenario 1: Quy trình hoàn tiền

```
1. Đặt phòng → POST /api/Booking
2. Hủy đặt phòng → DELETE /api/Booking/{id}
3. Yêu cầu hoàn tiền → POST /api/Booking/{id}/request-refund
4. (Nếu muốn hủy) → POST /api/Booking/{id}/cancel-refund-request
```

### Scenario 2: Xem đánh giá phòng

```
1. Xem danh sách phòng → GET /api/room
2. Chọn phòng → GET /api/room/{id}
3. Xem đánh giá phòng đó → GET /api/Review/by-room/{roomId}
```

### Scenario 3: Quản lý đánh giá của tôi

```
1. Xem đánh giá của tôi → GET /api/Review/my-reviews
2. Sửa đánh giá → PUT /api/Review/{id}
3. Xóa đánh giá → DELETE /api/Review/{id}
```

### Scenario 4: Quản lý sự cố

```
1. Báo cáo sự cố → POST /api/Incident
2. Xem sự cố của tôi → GET /api/Incident/my-incidents
3. Sửa sự cố → PUT /api/Incident/{id}
4. Xóa sự cố → DELETE /api/Incident/{id}
```

---

## 🔧 CẦN TẠO MIGRATION

✅ ĐÃ HOÀN THÀNH!

Migration đã được tạo và database đã được cập nhật:

```bash
cd Manager/Manager.API

# Migration đã tạo
dotnet ef migrations add AddRefundToBooking

# Database đã cập nhật
dotnet ef database update
```

---

## 📝 GHI CHÚ

### Refund Request Flow:

```
1. User đặt phòng (Status: Pending)
2. User hủy phòng (Status: Cancelled)
3. User yêu cầu hoàn tiền (RefundRequested: true)
4. Admin/Manager xử lý yêu cầu
5. (Optional) User hủy yêu cầu hoàn tiền
```

### Review Flow:

```
1. User đặt phòng
2. User check-in và check-out
3. User viết đánh giá
4. User có thể sửa/xóa đánh giá của mình
5. Mọi người có thể xem đánh giá theo phòng
```

### Incident Flow:

```
1. User báo cáo sự cố
2. User xem sự cố của mình
3. Admin/Manager xem tất cả sự cố
4. Admin/Manager cập nhật trạng thái
5. User có thể xóa sự cố nếu đã giải quyết
```

---

## ✅ CHECKLIST HOÀN THÀNH

- [x] Xem lịch sử đặt phòng
- [x] Yêu cầu hoàn tiền
- [x] Hủy yêu cầu hoàn tiền
- [x] Xem đánh giá theo phòng
- [x] Xem đánh giá của tôi
- [x] Xem sự cố của tôi
- [x] Cập nhật Booking model
- [x] Cập nhật Repository interfaces
- [x] Implement Repository methods
- [x] Cập nhật Controllers

---

## 🚀 BƯỚC TIẾP THEO

### 1. Chạy Migration
```bash
cd Manager/Manager.API
dotnet ef migrations add AddRefundToBooking
dotnet ef database update
```

### 2. Restart Backend
```bash
dotnet run
```

### 3. Test với Postman
- Import collection
- Test các endpoint mới
- Verify kết quả

---

**Hệ thống giờ đã hoàn thiện hơn! 🎉**
