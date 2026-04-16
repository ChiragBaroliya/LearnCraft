import React from 'react';
import { useNavigate } from 'react-router-dom';
import { toast } from 'react-toastify';

const Login: React.FC = () => {
  const navigate = useNavigate();

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    // Simulation of login
    toast.success('Welcome back, Admin!');
    navigate('/dashboard');
  };

  return (
    <div 
      className="d-flex align-items-center justify-content-center p-3" 
      style={{ 
        height: '100vh', 
        background: 'linear-gradient(135deg, #667eea 0%, #764ba2 100%)',
        fontFamily: "'Inter', sans-serif"
      }}
    >
      <div 
        className="bg-white p-5 rounded-4 shadow-lg w-100" 
        style={{ maxWidth: '450px' }}
      >
        <div className="text-center mb-4">
          <h1 className="fw-bolder" style={{ color: '#764ba2' }}>LearnCraft</h1>
          <p className="text-secondary fw-medium">Welcome Back</p>
        </div>
        
        <form onSubmit={handleSubmit}>
          <div className="mb-3">
            <label className="form-label fw-semibold text-secondary small uppercase">Email Address</label>
            <input 
              type="email" 
              className="form-control py-2 border-secondary-subtle shadow-none" 
              placeholder="name@example.com" 
              required 
            />
          </div>
          <div className="mb-4">
            <label className="form-label fw-semibold text-secondary small uppercase">Password</label>
            <input 
              type="password" 
              className="form-control py-2 border-secondary-subtle shadow-none" 
              placeholder="••••••••" 
              required 
            />
          </div>
          <div className="mb-4 d-flex justify-content-between align-items-center">
            <div className="form-check">
              <input type="checkbox" className="form-check-input" id="rememberMe" />
              <label className="form-check-label small text-secondary" htmlFor="rememberMe">Remember me</label>
            </div>
            <a href="#" className="small text-decoration-none" style={{ color: '#764ba2', fontWeight: 600 }}>Forgot password?</a>
          </div>
          <button 
            type="submit" 
            className="btn btn-primary w-100 py-2 border-0 fw-bold" 
            style={{ 
              backgroundColor: '#764ba2',
              transition: 'transform 0.2s',
            }}
            onMouseOver={(e) => e.currentTarget.style.transform = 'translateY(-2px)'}
            onMouseOut={(e) => e.currentTarget.style.transform = 'translateY(0)'}
          >
            Sign In
          </button>
        </form>
        
        <div className="mt-5 text-center">
          <p className="text-secondary small">
            Don't have an account? <a href="#" className="text-decoration-none fw-bold" style={{ color: '#764ba2' }}>Register</a>
          </p>
        </div>
      </div>
    </div>
  );
};

export default Login;
