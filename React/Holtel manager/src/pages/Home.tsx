import { Link } from 'react-router-dom';
import { useAuth } from '../contexts/AuthContext';
import './Home.css';

export default function Home() {
  const { isAuthenticated } = useAuth();

  return (
    <div className="home-page">
      <section className="hero">
        <div className="hero-content">
          <h1 className="hero-title">
            Chào mừng đến với <span className="highlight">Hotel Manager</span>
          </h1>
          <p className="hero-subtitle">
            Hệ thống quản lý khách sạn hiện đại, đơn giản và tiện lợi
          </p>
          <div className="hero-actions">
            {isAuthenticated ? (
              <>
                <Link to="/rooms" className="btn btn-primary btn-lg">
                  Xem phòng
                </Link>
                <Link to="/bookings" className="btn btn-outline btn-lg">
                  Đặt phòng của tôi
                </Link>
              </>
            ) : (
              <>
                <Link to="/register" className="btn btn-primary btn-lg">
                  Đăng ký ngay
                </Link>
                <Link to="/login" className="btn btn-outline btn-lg">
                  Đăng nhập
                </Link>
              </>
            )}
          </div>
        </div>
      </section>

      <section className="features">
        <div className="container">
          <h2 className="section-title">Tính năng nổi bật</h2>
          <div className="features-grid">
            <div className="feature-card">
              <h3>Quản lý phòng</h3>
              <p>Theo dõi trạng thái phòng trống, đã đặt một cách dễ dàng</p>
            </div>
            <div className="feature-card">
              <h3>Đặt phòng nhanh</h3>
              <p>Đặt phòng chỉ với vài thao tác đơn giản</p>
            </div>
            <div className="feature-card">
              <h3>Thanh toán tiện lợi</h3>
              <p>Nhiều phương thức thanh toán linh hoạt</p>
            </div>
            <div className="feature-card">
              <h3>Báo cáo chi tiết</h3>
              <p>Thống kê doanh thu và tình hình kinh doanh</p>
            </div>
            <div className="feature-card">
              <h3>Hỗ trợ 24/7</h3>
              <p>Đội ngũ hỗ trợ luôn sẵn sàng giúp đỡ</p>
            </div>
            <div className="feature-card">
              <h3>Đánh giá dịch vụ</h3>
              <p>Khách hàng có thể đánh giá và phản hồi</p>
            </div>
          </div>
        </div>
      </section>

      <section className="cta">
        <div className="cta-content">
          <h2>Sẵn sàng bắt đầu?</h2>
          <p>Đăng ký ngay hôm nay để trải nghiệm dịch vụ tốt nhất</p>
          {!isAuthenticated && (
            <Link to="/register" className="btn btn-primary btn-lg">
              Đăng ký miễn phí
            </Link>
          )}
        </div>
      </section>
    </div>
  );
}
