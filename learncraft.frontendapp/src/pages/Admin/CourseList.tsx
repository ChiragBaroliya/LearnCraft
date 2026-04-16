import React from 'react';
import { BsPencil, BsTrash, BsSearch, BsPeople, BsCheckCircle, BsEye } from 'react-icons/bs';

const CourseList: React.FC = () => {
  const courses = [
    { 
      id: 1, 
      title: 'React Mastery: Zero to Pro', 
      desc: 'Learn React.js with hooks, context API, and specialized projects.', 
      students: '1,200', 
      status: 'Published', 
      image: 'React+Mastery', 
      color: '667eea' 
    },
    { 
      id: 2, 
      title: 'Node.js API Development', 
      desc: 'Build scalable backends using Express, MongoDB, and JWT Auth.', 
      students: '850', 
      status: 'Published', 
      image: 'Node.js+Backend', 
      color: '764ba2' 
    },
    { 
      id: 3, 
      title: 'Complete UI/UX Bootcamp', 
      desc: 'Master Figma and design principles for modern web & mobile apps.', 
      students: '0', 
      status: 'Draft', 
      image: 'UI/UX+Design', 
      color: '4facfe' 
    },
  ];

  return (
    <div>
      <div className="d-flex justify-content-between align-items-center mb-4">
        <div>
          <h2 className="fw-bold mb-1">Course Management</h2>
          <p className="text-secondary small mb-0">Total 45 courses published across 5 categories.</p>
        </div>
        <button className="btn btn-primary px-4 py-2 border-0 fw-bold" style={{ background: 'linear-gradient(135deg, #667eea 0%, #764ba2 100%)' }}>
          + Create New Course
        </button>
      </div>

      <div className="row g-3 mb-4">
        <div className="col-md-4">
          <div className="input-group">
            <span className="input-group-text bg-white border-0 shadow-sm"><BsSearch className="text-secondary" /></span>
            <input type="text" className="form-control border-0 shadow-sm placeholder-secondary small" placeholder="Search courses..." />
          </div>
        </div>
        <div className="col-md-2">
          <select className="form-select border-0 shadow-sm text-secondary small fw-medium">
            <option>All Categories</option>
            <option>Development</option>
            <option>Design</option>
          </select>
        </div>
      </div>

      <div className="row g-4">
        {courses.map(course => (
          <div className="col-md-4" key={course.id}>
            <div className="card border-0 rounded-4 shadow-sm overflow-hidden h-100 transition-all hover-scale">
              <div 
                className="position-relative" 
                style={{ 
                  height: '160px', 
                  backgroundImage: `url('https://placehold.co/600x400/${course.color}/fff?text=${course.image}')`,
                  backgroundSize: 'cover',
                  backgroundPosition: 'center'
                }}
              >
                <span className={`position-absolute top-0 end-0 m-3 badge ${course.status === 'Published' ? 'bg-success' : 'bg-warning text-dark'} fw-bold px-3 py-2 rounded-pill shadow-sm`}>
                  {course.status}
                </span>
              </div>
              <div className="card-body p-4">
                <h5 className="fw-bold mb-2 text-dark">{course.title}</h5>
                <p className="text-secondary small mb-4 line-clamp-2">{course.desc}</p>
                <hr className="opacity-10 mb-4" />
                <div className="d-flex justify-content-between align-items-center">
                  <span className="text-secondary small fw-semibold d-flex align-items-center gap-2">
                    <BsPeople className="text-primary" /> {course.students} Students
                  </span>
                  <div className="d-flex gap-2">
                    <button className="btn btn-light btn-sm border-0 bg-light p-2 text-primary" title="View"><BsEye /></button>
                    <button className="btn btn-light btn-sm border-0 bg-light p-2 text-secondary" title="Edit"><BsPencil /></button>
                    <button className="btn btn-light btn-sm border-0 bg-light p-2 text-danger" title="Delete"><BsTrash /></button>
                  </div>
                </div>
              </div>
            </div>
          </div>
        ))}
      </div>

      <style>{`
        .hover-scale { transition: all 0.3s cubic-bezier(0.4, 0, 0.2, 1); }
        .hover-scale:hover { transform: scale(1.02); box-shadow: 0 15px 30px rgba(0,0,0,0.1) !important; }
        .line-clamp-2 {
          display: -webkit-box;
          -webkit-line-clamp: 2;
          -webkit-box-orient: vertical;
          overflow: hidden;
        }
      `}</style>
    </div>
  );
};

export default CourseList;
