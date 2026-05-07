import React from 'react';
import { 
  BsPeople, 
  BsBook, 
  BsPlayCircle, 
  BsCurrencyDollar,
  BsPersonPlus,
  BsCheckCircle,
  BsPencil,
  BsBarChart
} from 'react-icons/bs';

const Dashboard: React.FC = () => {
  const stats = [
    { label: 'Total Students', value: '1,280', icon: <BsPeople />, color: 'primary', trend: '+12%', up: true },
    { label: 'Active Courses', value: '45', icon: <BsBook />, color: 'success', trend: '+5 new', up: true },
    { label: 'Lesson Views', value: '12,450', icon: <BsPlayCircle />, color: 'warning', trend: '-2%', up: false },
    { label: 'Revenue', value: '$24,500', icon: <BsCurrencyDollar />, color: 'info', trend: '+18%', up: true },
  ];

  const activities = [
    { type: 'student', title: 'New Student Registered', desc: 'John Doe just joined the platform.', icon: <BsPersonPlus />, color: 'primary' },
    { type: 'course', title: 'Course Completed', desc: 'Alice Smith finished "Advanced React".', icon: <BsCheckCircle />, color: 'success' },
    { type: 'update', title: 'Course Updated', desc: 'Instructor Bob updated "Modern CSS".', icon: <BsPencil />, color: 'warning' },
  ];

  return (
    <div>
      <div className="d-flex justify-content-between align-items-center mb-4">
        <div>
          <h2 className="fw-bold mb-1">Welcome Back, Admin!</h2>
          <p className="text-muted small mb-0">Here's what's happening on your platform today.</p>
        </div>
        <button 
          className="btn btn-primary px-4 py-2 border-0 fw-bold" 
          style={{ background: 'linear-gradient(135deg, #667eea 0%, #764ba2 100%)' }}
        >
          + New Course
        </button>
      </div>

      <div className="row g-4 mb-4">
        {stats.map((stat, index) => (
          <div className="col-md-3" key={index}>
            <div className="bg-white p-4 rounded-4 shadow-sm h-100 border-0 transition-all hover-translate-y">
              <div 
                className={`rounded-3 mb-3 d-flex align-items-center justify-content-center bg-${stat.color} bg-opacity-10 text-${stat.color}`} 
                style={{ width: '48px', height: '48px', fontSize: '20px' }}
              >
                {stat.icon}
              </div>
              <div className="text-secondary small fw-semibold uppercase mb-1">{stat.label}</div>
              <div className="h3 fw-bold mb-1">{stat.value}</div>
              <div className={`small ${stat.up ? 'text-success' : 'text-danger'}`}>
                {stat.trend} {stat.up ? 'increase' : 'decrease'}
              </div>
            </div>
          </div>
        ))}
      </div>

      <div className="row g-4">
        <div className="col-md-8">
          <div className="bg-white p-4 rounded-4 shadow-sm h-100 border-0">
            <h5 className="fw-bold mb-4">Platform Usage Overview</h5>
            <div 
              className="rounded-3 d-flex align-items-center justify-content-center bg-light border border-dashed" 
              style={{ height: '300px', color: '#adb5bd' }}
            >
              <div className="text-center">
                <BsBarChart className="fs-1 mb-2" />
                <p className="mb-0 fw-medium">Activity Chart Visualization</p>
                <p className="small mb-0">Integration with Chart.js pending</p>
              </div>
            </div>
          </div>
        </div>
        <div className="col-md-4">
          <div className="bg-white p-4 rounded-4 shadow-sm h-100 border-0">
            <h5 className="fw-bold mb-4">Recent Activity</h5>
            <div className="activity-list">
              {activities.map((activity, index) => (
                <div key={index} className={`d-flex gap-3 mb-4 ${index === activities.length - 1 ? '' : 'border-bottom pb-3'}`}>
                  <div 
                    className={`rounded-circle bg-light d-flex align-items-center justify-content-center text-${activity.color}`} 
                    style={{ width: '40px', height: '40px', flexShrink: 0 }}
                  >
                    {activity.icon}
                  </div>
                  <div>
                    <div className="fw-semibold small">{activity.title}</div>
                    <div className="text-secondary small" style={{ fontSize: '0.8rem' }}>{activity.desc}</div>
                  </div>
                </div>
              ))}
            </div>
            <button className="btn btn-light w-100 btn-sm fw-bold py-2 text-secondary">View All Activity</button>
          </div>
        </div>
      </div>

    </div>
  );
};

export default Dashboard;
