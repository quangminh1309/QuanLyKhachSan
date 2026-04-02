import { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { bookingService } from '../services/bookingService';
import type { Booking } from '../services/bookingService';
import './Bookings.css';

export default function Bookings() {
  const [bookings, setBookings] = useState<Booking[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');

  useEffect(() => {
    loadBookings();
  }, []);

  const loadBookings = async () => {
    try {
      const data = await bookingService.getMyBookings();
      setBookings(data);
    } catch (err: any) {
      setError('Không thể tải danh sách đặt phòng');
    } finally {
      setLoading(false);
    }
  };

  const handleCancel = async (id: number) => {
    if (!confirm('Bạn có chắc muốn hủy đặt phòng này?')) return;

    try {
      await bookingService.cancel(id);
      loadBookings();
    } catch (err: any) {
      alert('Không thể hủy đặt phòng');
    }
  };

  const getStatusBadge = (status: string) => {
    const statusMap: Record<string, { label: string; className: string }> = {
      Pending: { label: 'Chờ xác nhận', className: 'badge-warning' },
      Confirmed: { label: 'Đã xác nhận', className: 'badge-success' },
      CheckedIn: { label: 'Đã nhận phòng', className: 'badge-info' },
      CheckedOut: { label: 'Đã trả phòng', className: 'badge-info' },
      Cancelled: { label: 'Đã hủy', className: 'badge-danger' },
    };
    const info = statusMap[status] || { label: status, className: 'badge-info' };
    return <span className={`badge ${info.className}`}>{info.label}</span>;
  };

  const formatDate = (dateString: string) => {
    return new Date(dateString).toLocaleDateString('vi-VN');
  };

  if (loading) {
    return (
      <div className="bookings-page">
        <div className="container">
          <div className="loading">Đang tải...</div>
        </div>
      </div>
    );
  }

  if (error) {
    return (
      <div className="bookings-page">
        <div className="container">
          <div className="error-message">{error}</div>
        </div>
      </div>
    );
  }

  return (
    <div className="bookings-page">
      <div className="container">
        <div className="page-header">
          <h1>Đặt phòng của tôi</h1>
          <p>Quản lý các đặt phòng của bạn</p>
        </div>

        {bookings.length === 0 ? (
          <div className="empty-state">
            <h2>Chưa có đặt phòng nào</h2>
            <p>Bạn chưa có đặt phòng nào. Hãy đặt phòng ngay!</p>
            <Link to="/rooms" className="btn btn-primary">
              Xem phòng
            </Link>
          </div>
        ) : (
          <div className="bookings-list">
            {bookings.map((booking) => (
              <div key={booking.id} className="booking-card">
                <div className="booking-info">
                  <h3>Phòng #{booking.roomId}</h3>
                  <div className="booking-details">
                    <p>Nhận phòng: {formatDate(booking.checkInDate)}</p>
                    <p>Trả phòng: {formatDate(booking.checkOutDate)}</p>
                    <p>Số khách: {booking.numberOfGuests}</p>
                    <p>Tổng tiền: {booking.totalPrice.toLocaleString('vi-VN')} VNĐ</p>
                    {booking.specialRequests && (
                      <p>Yêu cầu: {booking.specialRequests}</p>
                    )}
                  </div>
                </div>
                <div className="booking-actions">
                  {getStatusBadge(booking.status)}
                  {(booking.status === 'Pending' || booking.status === 'Confirmed') && (
                    <button
                      onClick={() => handleCancel(booking.id)}
                      className="btn btn-danger"
                    >
                      Hủy đặt phòng
                    </button>
                  )}
                </div>
              </div>
            ))}
          </div>
        )}
      </div>
    </div>
  );
}
