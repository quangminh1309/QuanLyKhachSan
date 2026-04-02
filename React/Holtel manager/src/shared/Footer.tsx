import './Footer.css';

export default function Footer() {
  return (
    <footer className="footer">
      <div className="footer-container">
        <div className="footer-content">
          <div className="footer-section">
            <h3>Hotel Manager</h3>
            <p>Hệ thống quản lý khách sạn hiện đại</p>
          </div>
          <div className="footer-section">
            <h4>Liên hệ</h4>
            <p>Email: contact@hotelmanager.com</p>
            <p>Điện thoại: (028) 1234 5678</p>
          </div>
          <div className="footer-section">
            <h4>Địa chỉ</h4>
            <p>123 Đường ABC, Quận 1</p>
            <p>TP. Hồ Chí Minh</p>
          </div>
        </div>
        <div className="footer-bottom">
          <p>© 2025 Hotel Manager. All rights reserved.</p>
        </div>
      </div>
    </footer>
  );
}
