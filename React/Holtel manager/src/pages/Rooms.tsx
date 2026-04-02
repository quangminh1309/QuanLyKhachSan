import { useState, useEffect } from 'react';
import { roomService } from '../services/roomService';
import type { Room } from '../services/roomService';
import './Rooms.css';

export default function Rooms() {
  const [rooms, setRooms] = useState<Room[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');

  useEffect(() => {
    loadRooms();
  }, []);

  const loadRooms = async () => {
    try {
      const data = await roomService.getAll();
      setRooms(data);
    } catch (err: any) {
      setError('Không thể tải danh sách phòng');
    } finally {
      setLoading(false);
    }
  };

  const getStatusBadge = (status: string) => {
    const statusMap: Record<string, { label: string; className: string }> = {
      Available: { label: 'Còn trống', className: 'badge-success' },
      Reserved: { label: 'Đã đặt', className: 'badge-warning' },
      Occupied: { label: 'Đang sử dụng', className: 'badge-danger' },
      Maintenance: { label: 'Bảo trì', className: 'badge-info' },
    };
    const info = statusMap[status] || { label: status, className: 'badge-info' };
    return <span className={`badge ${info.className}`}>{info.label}</span>;
  };

  if (loading) {
    return (
      <div className="rooms-page">
        <div className="container">
          <div className="loading">Đang tải...</div>
        </div>
      </div>
    );
  }

  if (error) {
    return (
      <div className="rooms-page">
        <div className="container">
          <div className="error-message">{error}</div>
        </div>
      </div>
    );
  }

  return (
    <div className="rooms-page">
      <div className="container">
        <div className="page-header">
          <h1>Danh sách phòng</h1>
          <p>Tổng số: {rooms.length} phòng</p>
        </div>

        <div className="rooms-grid">
          {rooms.map((room) => (
            <div key={room.id} className="room-card">
              <div className="room-header">
                <h3>Phòng {room.roomNumber}</h3>
                {getStatusBadge(room.currentStatus)}
              </div>
              <div className="room-details">
                <p>Tầng: {room.floor}</p>
                <p>Loại phòng: #{room.roomTypeId}</p>
              </div>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
}
