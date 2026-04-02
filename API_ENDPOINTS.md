# 📡 DANH SÁCH API ENDPOINTS

Base URL: `http://localhost:5162/api`

---

## 🔐 AUTHENTICATION

### Register
```http
POST /Account/register
Body: {
  "username": "string",
  "email": "string",
  "password": "string"
}
Response: { "userName", "email", "token" }
```

### Login
```http
POST /Account/login
Body: {
  "username": "string",
  "password": "string"
}
Response: { "userName", "email", "token" }
```

### Assign Role (Admin only)
```http
POST /Account/assign-role
Headers: Authorization: Bearer {token}
Body: {
  "username": "string",
  "roleName": "Admin|Manager|Guest"
}
```

---

## 🏨 ROOM MANAGEMENT

### Get All Rooms
```http
GET /room
Headers: Authorization: Bearer {token}
Response: [{ "id", "roomNumber", "floor", "roomTypeId", "currentStatus" }]
```

### Get Room by ID
```http
GET /room/{id}
Headers: Authorization: Bearer {token}
```

### Create Room (Admin/Manager)
```http
POST /room?IdRoomType={roomTypeId}
Headers: Authorization: Bearer {token}
Body: {
  "roomNumber": "string",
  "currentStatus": "Available|Reserved|Occupied|Maintenance"
}
```

### Update Room (Admin/Manager)
```http
PUT /room/{id}
Headers: Authorization: Bearer {token}
Body: {
  "roomNumber": "string",
  "roomTypeId": number,
  "currentStatus": "string"
}
```

### Delete Room (Admin/Manager)
```http
DELETE /room/{id}
Headers: Authorization: Bearer {token}
```

---

## 🏷️ ROOM TYPE MANAGEMENT

### Get All Room Types
```http
GET /RoomType
Headers: Authorization: Bearer {token}
Response: [{ "id", "name", "description", "capacity", "basePrice" }]
```

### Get Room Type by ID
```http
GET /RoomType/{id}
Headers: Authorization: Bearer {token}
```

### Create Room Type (Admin/Manager)
```http
POST /RoomType
Headers: Authorization: Bearer {token}
Body: {
  "name": "string",
  "description": "string",
  "capacity": number,
  "basePrice": number
}
```

### Update Room Type (Admin/Manager)
```http
PUT /RoomType/{id}
Headers: Authorization: Bearer {token}
Body: { "name", "description", "capacity", "basePrice" }
```

### Delete Room Type (Admin/Manager)
```http
DELETE /RoomType/{id}
Headers: Authorization: Bearer {token}
```

---

## 📅 BOOKING MANAGEMENT

### Get All Bookings (Admin/Manager)
```http
GET /Booking
Headers: Authorization: Bearer {token}
```

### Get My Bookings
```http
GET /Booking/my-bookings
Headers: Authorization: Bearer {token}
Response: [{ "id", "roomId", "checkInDate", "checkOutDate", "numberOfGuests", "totalPrice", "status" }]
```

### Get Booking by ID
```http
GET /Booking/{id}
Headers: Authorization: Bearer {token}
```

### Create Booking
```http
POST /Booking?IdRoom={roomId}
Headers: Authorization: Bearer {token}
Body: {
  "checkInDate": "2025-04-01",
  "checkOutDate": "2025-04-05",
  "numberOfGuests": 2,
  "specialRequests": "string"
}
```

### Cancel Booking
```http
DELETE /Booking/{id}
Headers: Authorization: Bearer {token}
```

---

## 🛎️ SERVICE MANAGEMENT

### Get All Services
```http
GET /Services
Headers: Authorization: Bearer {token}
Response: [{ "id", "name", "description", "price" }]
```

### Get Service by ID
```http
GET /Services/{id}
Headers: Authorization: Bearer {token}
```

### Create Service (Admin/Manager)
```http
POST /Services
Headers: Authorization: Bearer {token}
Body: {
  "name": "string",
  "description": "string",
  "price": number
}
```

### Update Service (Admin/Manager)
```http
PUT /Services/{id}
Headers: Authorization: Bearer {token}
Body: { "name", "description", "price" }
```

### Delete Service (Admin/Manager)
```http
DELETE /Services/{id}
Headers: Authorization: Bearer {token}
```

---

## 🎫 DISCOUNT MANAGEMENT

### Get All Discounts (Admin/Manager)
```http
GET /Discount
Headers: Authorization: Bearer {token}
Response: [{ "id", "code", "description", "discountPercentage", "startDate", "endDate", "isActive" }]
```

### Get Discount by ID (Admin/Manager)
```http
GET /Discount/{id}
Headers: Authorization: Bearer {token}
```

### Create Discount (Admin/Manager)
```http
POST /Discount
Headers: Authorization: Bearer {token}
Body: {
  "code": "SUMMER20",
  "description": "string",
  "discountPercentage": 20,
  "startDate": "2025-04-01",
  "endDate": "2025-06-30",
  "isActive": true
}
```

### Update Discount (Admin/Manager)
```http
PUT /Discount/{id}
Headers: Authorization: Bearer {token}
Body: { "code", "description", "discountPercentage", "startDate", "endDate", "isActive" }
```

### Delete Discount (Admin/Manager)
```http
DELETE /Discount/{id}
Headers: Authorization: Bearer {token}
```

---

## ⭐ REVIEW MANAGEMENT

### Get All Reviews
```http
GET /Review
Headers: Authorization: Bearer {token}
Response: [{ "id", "bookingId", "rating", "comment", "createAt" }]
```

### Get Review by ID
```http
GET /Review/{id}
Headers: Authorization: Bearer {token}
```

### Create Review
```http
POST /Review
Headers: Authorization: Bearer {token}
Body: {
  "bookingId": number,
  "rating": 1-5,
  "comment": "string"
}
```

### Update Review
```http
PUT /Review/{id}
Headers: Authorization: Bearer {token}
Body: {
  "rating": 1-5,
  "comment": "string"
}
```

### Delete Review
```http
DELETE /Review/{id}
Headers: Authorization: Bearer {token}
```

---

## 🚨 INCIDENT MANAGEMENT

### Get All Incidents (Admin/Manager)
```http
GET /Incident
Headers: Authorization: Bearer {token}
Response: [{ "id", "bookingId", "description", "reportedAt", "status" }]
```

### Get Incident by ID (Admin/Manager)
```http
GET /Incident/{id}
Headers: Authorization: Bearer {token}
```

### Create Incident
```http
POST /Incident
Headers: Authorization: Bearer {token}
Body: {
  "bookingId": number,
  "description": "string"
}
```

### Update Incident (Admin/Manager)
```http
PUT /Incident/{id}
Headers: Authorization: Bearer {token}
Body: {
  "description": "string",
  "status": "Pending|InProgress|Resolved"
}
```

### Delete Incident
```http
DELETE /Incident/{id}
Headers: Authorization: Bearer {token}
```

---

## 📄 INVOICE MANAGEMENT

### Get All Invoices (Admin/Manager)
```http
GET /Invoice
Headers: Authorization: Bearer {token}
```

### Get Invoice by ID (Admin/Manager)
```http
GET /Invoice/{id}
Headers: Authorization: Bearer {token}
```

### Create Invoice (Admin/Manager)
```http
POST /Invoice
Headers: Authorization: Bearer {token}
Body: {
  "bookingId": number,
  "totalAmount": number,
  "status": "Pending|Paid|Cancelled"
}
```

---

## 💳 PAYMENT MANAGEMENT

### Create Payment (Admin/Manager)
```http
POST /Payment
Headers: Authorization: Bearer {token}
Body: {
  "invoiceId": number,
  "amount": number,
  "paymentMethod": "Cash|Card|Transfer"
}
```

### Merge Invoices (Admin/Manager)
```http
POST /Payment/merge-invoices
Headers: Authorization: Bearer {token}
Body: {
  "invoiceIds": [1, 2, 3]
}
```

### Split Invoice (Admin/Manager)
```http
POST /Payment/split-invoice
Headers: Authorization: Bearer {token}
Body: {
  "invoiceId": number,
  "splitAmounts": [500000, 300000]
}
```

---

## 🔄 CHECK IN/OUT

### Check In
```http
POST /CheckInOut/check-in
Headers: Authorization: Bearer {token}
Body: {
  "bookingId": number
}
```

### Check Out
```http
POST /CheckInOut/check-out
Headers: Authorization: Bearer {token}
Body: {
  "bookingId": number
}
```

### Transfer Room
```http
POST /CheckInOut/transfer-room
Headers: Authorization: Bearer {token}
Body: {
  "bookingId": number,
  "newRoomId": number
}
```

### Extend Booking
```http
POST /CheckInOut/extend-booking
Headers: Authorization: Bearer {token}
Body: {
  "bookingId": number,
  "newCheckOutDate": "2025-04-10"
}
```

---

## 👤 PROFILE MANAGEMENT

### Get Profile
```http
GET /Profile
Headers: Authorization: Bearer {token}
Response: { "userName", "email", "phoneNumber", "address" }
```

### Update Profile
```http
PUT /Profile
Headers: Authorization: Bearer {token}
Body: {
  "phoneNumber": "string",
  "address": "string"
}
```

### Change Password
```http
POST /Profile/change-password
Headers: Authorization: Bearer {token}
Body: {
  "currentPassword": "string",
  "newPassword": "string"
}
```

---

## 📊 REPORT

### Revenue Report (Admin/Manager)
```http
GET /Report/revenue?startDate=2025-01-01&endDate=2025-12-31
Headers: Authorization: Bearer {token}
Response: {
  "totalRevenue": number,
  "totalBookings": number,
  "averageBookingValue": number
}
```

---

## 📝 GHI CHÚ

### Status Values:
- **Room Status**: Available, Reserved, Occupied, Maintenance
- **Booking Status**: Pending, Confirmed, CheckedIn, CheckedOut, Cancelled
- **Invoice Status**: Pending, Paid, Cancelled
- **Incident Status**: Pending, InProgress, Resolved
- **Payment Method**: Cash, Card, Transfer

### Authorization:
- **Public**: Register, Login
- **Authenticated**: Tất cả endpoints khác
- **Admin/Manager**: Create/Update/Delete cho Room, RoomType, Service, Discount, Invoice, Payment

---

**Tổng số: 60+ API endpoints** 🚀
