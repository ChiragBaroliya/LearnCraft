import React from 'react';
import { Link, Outlet, useLocation } from 'react-router-dom';
import { 
  BsSpeedometer2, 
  BsPeople, 
  BsBook, 
  BsJournalText, 
  BsBarChart, 
  BsBoxArrowLeft,
  BsBell,
  BsSearch
} from 'react-icons/bs';

const Layout: React.FC = () => {
  const location = useLocation();

  const navLinks = [
    { path: '/dashboard', label: 'Dashboard', icon: <BsSpeedometer2 /> },
    { path: '/admin/users', label: 'Users', icon: <BsPeople /> },
    { path: '/admin/courses', label: 'Courses', icon: <BsBook /> },
    { path: '/lessons', label: 'Lessons', icon: <BsJournalText /> },
    { path: '/reports', label: 'Reports', icon: <BsBarChart /> },
  ];

  return (
    <div className="d-flex" style={{ minHeight: '100vh', backgroundColor: '#f8f9fa' }}>
      {/* Sidebar */}
      <div 
        className="sidebar text-white" 
        style={{ 
          width: '260px', 
          backgroundColor: '#1a1a2e', 
          position: 'fixed', 
          height: '100vh',
          zIndex: 1000
        }}
      >
        <div className="px-4 py-4 mb-4">
          <h3 className="fw-bold mb-0">LearnCraft</h3>
        </div>
        <nav className="nav flex-column">
          {navLinks.map((link) => (
            <Link
              key={link.path}
              to={link.path}
              className={`nav-link px-4 py-3 d-flex align-items-center gap-3 text-white-50 ${
                location.pathname === link.path ? 'active border-start border-4 border-primary bg-white bg-opacity-10 text-white' : ''
              }`}
              style={{ transition: 'all 0.2s' }}
            >
              <span className="fs-5">{link.icon}</span>
              <span className="fw-medium">{link.label}</span>
            </Link>
          ))}
          <div className="mt-auto pt-4">
            <hr className="mx-4 opacity-10" />
            <Link to="/" className="nav-link px-4 py-3 d-flex align-items-center gap-3 text-white-50">
              <span className="fs-5"><BsBoxArrowLeft /></span>
              <span className="fw-medium">Logout</span>
            </Link>
          </div>
        </nav>
      </div>

      {/* Main Content Area */}
      <div className="flex-grow-1" style={{ marginLeft: '260px' }}>
        {/* Navbar */}
        <nav 
          className="navbar bg-white border-bottom px-4" 
          style={{ height: '70px', position: 'sticky', top: 0, zIndex: 900 }}
        >
          <div className="d-flex align-items-center gap-3">
            <div className="input-group" style={{ width: '300px' }}>
              <span className="input-group-text bg-light border-end-0"><BsSearch /></span>
              <input 
                type="text" 
                className="form-control bg-light border-start-0 shadow-none border" 
                placeholder="Search..." 
              />
            </div>
          </div>
          <div className="d-flex align-items-center gap-4">
            <div className="position-relative cursor-pointer">
              <BsBell className="fs-5 text-secondary" />
              <span className="position-absolute top-0 start-100 translate-middle p-1 bg-danger border border-light rounded-circle"></span>
            </div>
            <div className="d-flex align-items-center gap-2">
              <img 
                src="https://ui-avatars.com/api/?name=Admin+User&background=667eea&color=fff" 
                className="rounded-circle" 
                width="35" 
                height="35" 
                alt="Profile" 
              />
              <span className="fw-semibold text-secondary">Admin</span>
            </div>
          </div>
        </nav>

        {/* Page Content */}
        <div className="p-4">
          <Outlet />
        </div>
      </div>
    </div>
  );
};

export default Layout;
