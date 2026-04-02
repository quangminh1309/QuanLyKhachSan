import { Link, useNavigate } from 'react-router-dom';
import { useAuth } from '../contexts/AuthContext';
import './Header.css';

export default function Header() {
  const { user, logout, isAuthenticated } = useAuth();
  const navigate = useNavigate();

  const handleLogout = () => {
    logout();
    navigate('/login');
  };

  return (
    <header className="header">
      <div className="header-container">
        <Link to="/" className="logo">
          Hotel Manager
        </Link>
        <nav className="nav">
          <Link to="/" className="nav-link">Trang chủ</Link>
          {isAuthenticated && (
            <>
              <Link to="/bookings" className="nav-link">Đặt phòng của tôi</Link>
              <Link to="/rooms" className="nav-link">Phòng</Link>
            </>
          )}
        </nav>
        <div className="user-menu">
          {isAuthenticated ? (
            <>
              <span className="user-name">{user?.userName}</span>
              <button onClick={handleLogout} className="logout-btn">
                Đăng xuất
              </button>
            </>
          ) : (
            <>
              <Link to="/login" className="btn btn-outline">Đăng nhập</Link>
              <Link to="/register" className="btn btn-primary">Đăng ký</Link>
            </>
          )}
        </div>
      </div>
    </header>
  );
}
