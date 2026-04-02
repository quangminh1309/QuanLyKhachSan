# 🔄 LUỒNG HOẠT ĐỘNG HỆ THỐNG

## 📋 VÍ DỤ: LẤY DANH SÁCH PHÒNG

### 🎯 Tổng quan luồng

```
User Click → Frontend → API Service → HTTP Request → Backend Controller 
→ Repository → Database → Trả về Model → Mapper → DTO → JSON Response 
→ Frontend nhận data → Hiển thị UI
```

---

## 📱 FRONTEND (React)

### 1️⃣ Component: `Rooms.tsx` (Tầng Presentation)

**Vị trí:** `React/Holtel manager/src/pages/Rooms.tsx`

**Nhiệm vụ:** Hiển thị giao diện và xử lý tương tác người dùng

```typescript
export default function Rooms() {
  const [rooms, setRooms] = useState<Room[]>([]);  // State lưu danh sách phòng
  const [loading, setLoading] = useState(true);     // State loading

  useEffect(() => {
    loadRooms();  // ← Gọi hàm load khi component mount
  }, []);

  const loadRooms = async () => {
    try {
      const data = await roomService.getAll();  // ← GỌI SERVICE LAYER
      setRooms(data);  // Lưu data vào state
    } catch (err) {
      setError('Không thể tải danh sách phòng');
    } finally {
      setLoading(false);
    }
  };
}
```

**Giải thích:**
- Component này là nơi user nhìn thấy
- Khi trang load, `useEffect` tự động chạy
- Gọi `roomService.getAll()` để lấy data
- Nhận data và hiển thị lên UI

---

### 2️⃣ Service: `roomService.ts` (Tầng Business Logic)

**Vị trí:** `React/Holtel manager/src/services/roomService.ts`

**Nhiệm vụ:** Xử lý logic nghiệp vụ và gọi API

```typescript
export const roomService = {
  getAll: async (): Promise<Room[]> => {
    const response = await api.get('/room');  // ← GỌI API HELPER
    return response.data;  // Trả về data
  },
};
```

**Giải thích:**
- Service này chứa các hàm liên quan đến Room
- `getAll()` gọi API endpoint `/room`
- Sử dụng `api` helper để gửi request

---

### 3️⃣ API Helper: `api.ts` (Tầng HTTP Client)

**Vị trí:** `React/Holtel manager/src/services/api.ts`

**Nhiệm vụ:** Cấu hình Axios và gửi HTTP request

```typescript
const API_BASE_URL = 'http://localhost:5162/api';

const api = axios.create({
  baseURL: API_BASE_URL,  // Base URL của backend
  headers: {
    'Content-Type': 'application/json',
  },
});

// Tự động thêm token vào mọi request
api.interceptors.request.use((config) => {
  const token = localStorage.getItem('token');
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;  // ← Thêm JWT token
  }
  return config;
});
```

**Giải thích:**
- Axios instance với base URL `http://localhost:5162/api`
- Interceptor tự động thêm JWT token vào header
- Khi gọi `api.get('/room')` → Request đến `http://localhost:5162/api/room`

---

## 🌐 HTTP REQUEST

```http
GET http://localhost:5162/api/room
Headers:
  Content-Type: application/json
  Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

**Giải thích:**
- Method: GET
- URL: `/api/room`
- Header có JWT token để xác thực

---

## 🖥️ BACKEND (.NET)

### 4️⃣ Controller: `RoomController.cs` (Tầng API)

**Vị trí:** `Manager/Manager.API/Controllers/RoomController.cs`

**Nhiệm vụ:** Nhận request, xử lý và trả response

```csharp
[Route("api/room")]
[ApiController]
public class RoomController : ControllerBase
{
    private readonly IRoomRepository _roomRepository;
    
    public RoomController(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;  // ← Dependency Injection
    }

    [HttpGet]  // ← Endpoint GET /api/room
    public async Task<IActionResult> GetAll()
    {
        // 1. Gọi Repository để lấy data từ DB
        var roomModels = await _roomRepository.GetAllAsync();
        
        // 2. Kiểm tra có data không
        if (roomModels == null || roomModels.Count == 0)
        {
            return NotFound("No Room found.");
        }
        
        // 3. Convert Model → DTO bằng Mapper
        var roomDtos = roomModels.Select(s => s.ToRoomDto());
        
        // 4. Trả về JSON response
        return Ok(roomDtos);
    }
}
```

**Giải thích:**
- `[Route("api/room")]` → Định nghĩa route
- `[HttpGet]` → Xử lý GET request
- Nhận `IRoomRepository` qua Dependency Injection
- Gọi Repository để lấy data
- Convert Model sang DTO
- Trả về JSON

---

### 5️⃣ Repository: `RoomRepository.cs` (Tầng Data Access)

**Vị trí:** `Manager/Manager.API/Repository/RoomRepository.cs`

**Nhiệm vụ:** Truy vấn database

```csharp
public class RoomRepository : IRoomRepository
{
    private readonly ApplicationDBContext _dBContext;
    
    public RoomRepository(ApplicationDBContext dBContext)
    {
        _dBContext = dBContext;  // ← Entity Framework DbContext
    }

    public async Task<List<Rooms>> GetAllAsync()
    {
        // Truy vấn database lấy tất cả rooms
        var rooms = await _dBContext.Rooms.ToListAsync();
        return rooms;
    }
}
```

**Giải thích:**
- Sử dụng Entity Framework Core
- `_dBContext.Rooms` → Truy cập bảng Rooms
- `.ToListAsync()` → Lấy tất cả records
- Trả về `List<Rooms>` (Model)

---

### 6️⃣ Database Query (SQL Server)

Entity Framework tự động generate SQL:

```sql
SELECT [r].[Id], [r].[RoomNumber], [r].[RoomTypeId], 
       [r].[CurrentStatus], [r].[CreateAt], [r].[UpdateAt]
FROM [Rooms] AS [r]
```

**Giải thích:**
- EF Core tự động tạo câu SQL
- Query bảng `Rooms`
- Lấy tất cả columns

---

### 7️⃣ Model: `Rooms.cs` (Tầng Domain)

**Vị trí:** `Manager/Manager.API/Models/Rooms.cs`

**Nhiệm vụ:** Đại diện cho bảng trong database

```csharp
public class Rooms
{
    public int Id { get; set; }
    public string RoomNumber { get; set; }
    public int RoomTypeId { get; set; }
    public string CurrentStatus { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime UpdateAt { get; set; }

    // Navigation property
    public RoomType RoomType { get; set; } = null!;
}
```

**Giải thích:**
- Model map với bảng `Rooms` trong DB
- Chứa tất cả properties của Room
- Navigation property để join với RoomType

---

### 8️⃣ Mapper: `RoomMapper.cs` (Tầng Mapping)

**Vị trí:** `Manager/Manager.API/Mappers/RoomMapper.cs`

**Nhiệm vụ:** Convert Model ↔ DTO

```csharp
public static class RoomMapper
{
    public static RoomDto ToRoomDto(this Rooms room)
    {
        return new RoomDto
        {
            Id = room.Id,
            RoomNumber = room.RoomNumber,
            RoomTypeId = room.RoomTypeId,
            CurrentStatus = room.CurrentStatus,
            CreateAt = room.CreateAt,
            UpdateAt = room.UpdateAt
        };
    }
}
```

**Giải thích:**
- Extension method để convert
- `Rooms` (Model) → `RoomDto` (DTO)
- DTO là object trả về cho Frontend
- Không trả Navigation properties (tránh circular reference)

---

## 📤 HTTP RESPONSE

```json
[
  {
    "id": 1,
    "roomNumber": "101",
    "roomTypeId": 1,
    "currentStatus": "Available",
    "createAt": "2025-01-01T00:00:00",
    "updateAt": "2025-01-01T00:00:00"
  },
  {
    "id": 2,
    "roomNumber": "102",
    "roomTypeId": 1,
    "currentStatus": "Reserved",
    "createAt": "2025-01-01T00:00:00",
    "updateAt": "2025-01-01T00:00:00"
  }
]
```

**Status Code:** 200 OK

---

## 🔙 FRONTEND NHẬN RESPONSE

### 9️⃣ Quay lại `roomService.ts`

```typescript
const response = await api.get('/room');
return response.data;  // ← Trả về array of rooms
```

### 🔟 Quay lại `Rooms.tsx`

```typescript
const data = await roomService.getAll();  // ← Nhận array
setRooms(data);  // ← Lưu vào state
```

### 1️⃣1️⃣ Render UI

```typescript
{rooms.map((room) => (
  <div key={room.id} className="room-card">
    <h3>Phòng {room.roomNumber}</h3>
    <p>Tầng: {room.floor}</p>
    <p>Trạng thái: {room.currentStatus}</p>
  </div>
))}
```

---

## 📊 KIẾN TRÚC TỔNG QUAN

```
┌─────────────────────────────────────────────────────────────┐
│                        FRONTEND (React)                      │
├─────────────────────────────────────────────────────────────┤
│  1. Rooms.tsx (Component)                                    │
│     ↓ gọi                                                    │
│  2. roomService.ts (Service Layer)                           │
│     ↓ gọi                                                    │
│  3. api.ts (HTTP Client - Axios)                             │
│     ↓ gửi HTTP Request                                       │
└─────────────────────────────────────────────────────────────┘
                            ↓
                    HTTP GET /api/room
                    + JWT Token
                            ↓
┌─────────────────────────────────────────────────────────────┐
│                       BACKEND (.NET)                         │
├─────────────────────────────────────────────────────────────┤
│  4. RoomController.cs (API Controller)                       │
│     ↓ gọi                                                    │
│  5. RoomRepository.cs (Data Access)                          │
│     ↓ query                                                  │
│  6. Database (SQL Server)                                    │
│     ↓ trả về                                                 │
│  7. Rooms.cs (Model)                                         │
│     ↓ convert                                                │
│  8. RoomMapper.cs (Mapper)                                   │
│     ↓ trả về DTO                                             │
│  4. RoomController.cs → JSON Response                        │
└─────────────────────────────────────────────────────────────┘
                            ↓
                    HTTP Response (JSON)
                            ↓
┌─────────────────────────────────────────────────────────────┐
│                        FRONTEND (React)                      │
├─────────────────────────────────────────────────────────────┤
│  9. api.ts nhận response                                     │
│     ↓                                                        │
│  10. roomService.ts trả data                                 │
│     ↓                                                        │
│  11. Rooms.tsx render UI                                     │
└─────────────────────────────────────────────────────────────┘
```

---

## 🎓 CÁC TẦNG (LAYERS)

### Frontend:
1. **Presentation Layer** - `Rooms.tsx` (UI)
2. **Service Layer** - `roomService.ts` (Business Logic)
3. **HTTP Client Layer** - `api.ts` (Communication)

### Backend:
4. **API Layer** - `RoomController.cs` (Endpoints)
5. **Data Access Layer** - `RoomRepository.cs` (Database)
6. **Domain Layer** - `Rooms.cs` (Models)
7. **Mapping Layer** - `RoomMapper.cs` (Conversions)

---

## 🔑 KHÁI NIỆM QUAN TRỌNG

### DTO (Data Transfer Object)
- Object dùng để truyền data giữa Frontend ↔ Backend
- Chỉ chứa data cần thiết
- Không chứa logic

### Model
- Đại diện cho bảng trong database
- Chứa đầy đủ properties
- Có Navigation properties

### Repository Pattern
- Tách biệt logic truy vấn database
- Dễ test, dễ maintain
- Có thể thay đổi database mà không ảnh hưởng Controller

### Dependency Injection
- Controller nhận Repository qua constructor
- Không tạo instance trực tiếp
- Dễ test, dễ thay thế implementation

---

## 🚀 TÓM TẮT NHANH

1. **User mở trang** → Component `Rooms.tsx` load
2. **Component gọi** → `roomService.getAll()`
3. **Service gọi** → `api.get('/room')`
4. **Axios gửi** → HTTP GET request + JWT token
5. **Backend nhận** → `RoomController.GetAll()`
6. **Controller gọi** → `_roomRepository.GetAllAsync()`
7. **Repository query** → Database qua Entity Framework
8. **Database trả** → List<Rooms> (Model)
9. **Mapper convert** → Model → DTO
10. **Controller trả** → JSON response
11. **Frontend nhận** → Parse JSON thành object
12. **Component render** → Hiển thị UI

**Thời gian:** ~100-500ms tùy số lượng phòng

---

**Hy vọng giải thích này giúp bạn hiểu rõ luồng hoạt động! 🎉**
