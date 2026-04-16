import React from 'react';
import { BsPencil, BsTrash, BsSearch, BsDownload } from 'react-icons/bs';

const UserList: React.FC = () => {
  const users = [
    { id: 1, name: 'John Doe', email: 'john@example.com', role: 'Student', status: 'Active', date: 'Apr 12, 2026' },
    { id: 2, name: 'Dr. Smith', email: 'smith@learncraft.com', role: 'Instructor', status: 'Active', date: 'Mar 20, 2026' },
    { id: 3, name: 'Sarah Lee', email: 'sarah@example.com', role: 'Student', status: 'Pending', date: 'Apr 15, 2026' },
  ];

  const getRoleBadge = (role: string) => {
    switch (role) {
      case 'Student': return <span className="badge bg-primary-subtle text-primary fw-medium px-3 py-2 rounded-pill">Student</span>;
      case 'Instructor': return <span className="badge bg-purple-subtle text-purple fw-medium px-3 py-2 rounded-pill" style={{ backgroundColor: '#f3e5f5', color: '#7b1fa2' }}>Instructor</span>;
      case 'Admin': return <span className="badge bg-danger-subtle text-danger fw-medium px-3 py-2 rounded-pill">Admin</span>;
      default: return null;
    }
  };

  return (
    <div>
      <div className="d-flex justify-content-between align-items-center mb-4">
        <div>
          <h2 className="fw-bold mb-1">User Management</h2>
          <p className="text-secondary small mb-0">Manage roles, permissions, and platform access.</p>
        </div>
        <div className="d-flex gap-2">
          <button className="btn btn-outline-secondary border-0 fw-semibold d-flex align-items-center gap-2">
            <BsDownload /> Export
          </button>
          <button className="btn btn-primary px-4 py-2 border-0 fw-bold" style={{ backgroundColor: '#764ba2' }}>
            + Add User
          </button>
        </div>
      </div>

      <div className="bg-white rounded-4 shadow-sm p-4 border-0">
        <div className="row g-3 mb-4">
          <div className="col-md-4">
            <div className="input-group">
              <span className="input-group-text bg-light border-0"><BsSearch className="text-secondary" /></span>
              <input type="text" className="form-control bg-light border-0 shadow-none ps-0" placeholder="Search by name or email..." />
            </div>
          </div>
          <div className="col-md-2">
            <select className="form-select bg-light border-0 shadow-none text-secondary small fw-medium">
              <option value="">All Roles</option>
              <option value="student">Student</option>
              <option value="instructor">Instructor</option>
              <option value="admin">Admin</option>
            </select>
          </div>
        </div>

        <div className="table-responsive">
          <table className="table table-hover align-middle">
            <thead>
              <tr className="text-secondary small uppercase">
                <th className="border-0 pb-3">User</th>
                <th className="border-0 pb-3">Role</th>
                <th className="border-0 pb-3">Status</th>
                <th className="border-0 pb-3">Join Date</th>
                <th className="border-0 pb-3 text-end">Actions</th>
              </tr>
            </thead>
            <tbody>
              {users.map(user => (
                <tr key={user.id}>
                  <td className="border-light py-3">
                    <div className="d-flex align-items-center gap-3">
                      <img 
                        src={`https://ui-avatars.com/api/?name=${user.name}&background=random&color=fff`} 
                        className="rounded-circle" 
                        width="40" 
                        height="40" 
                        alt={user.name} 
                      />
                      <div>
                        <div className="fw-bold text-dark">{user.name}</div>
                        <div className="text-secondary small">{user.email}</div>
                      </div>
                    </div>
                  </td>
                  <td className="border-light">{getRoleBadge(user.role)}</td>
                  <td className="border-light">
                    <span className={`badge ${user.status === 'Active' ? 'bg-success-subtle text-success' : 'bg-warning-subtle text-warning'} px-2 py-1 rounded`}>
                      {user.status}
                    </span>
                  </td>
                  <td className="border-light text-secondary small">{user.date}</td>
                  <td className="border-light text-end">
                    <button className="btn btn-light btn-sm border-0 bg-transparent text-secondary hover-primary me-2"><BsPencil /></button>
                    <button className="btn btn-light btn-sm border-0 bg-transparent text-secondary hover-danger"><BsTrash /></button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>

        <nav className="mt-4">
          <ul className="pagination justify-content-center border-0 mb-0">
            <li className="page-item disabled"><button className="page-link border-0 text-secondary small">Previous</button></li>
            <li className="page-item active"><button className="page-link border-0 bg-primary text-white small px-3">1</button></li>
            <li className="page-item"><button className="page-link border-0 text-secondary small px-3">2</button></li>
            <li className="page-item"><button className="page-link border-0 text-secondary small">Next</button></li>
          </ul>
        </nav>
      </div>

      <style>{`
        .hover-primary:hover { color: #764ba2 !important; background-color: #f3e5f5 !important; }
        .hover-danger:hover { color: #dc3545 !important; background-color: #ffebee !important; }
        .page-link { border-radius: 8px !important; margin: 0 3px; }
      `}</style>
    </div>
  );
};

export default UserList;
