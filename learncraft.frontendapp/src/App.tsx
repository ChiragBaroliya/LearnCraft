import React from 'react';
import logo from './logo.svg';
import './App.css';
import {BrowserRouter, Route, Routes} from 'react-router-dom';
import Login from './components/Login';
import Layout from './components/Layout';
import Dashboard from './pages/Dashboard/Dashboard';
import UserList from './pages/Admin/UserList';
import CourseList from './pages/Admin/CourseList';
import { ToastContainer, toast } from 'react-toastify';

function App() {
  return (
    <BrowserRouter>
      <div className="App">
        <ToastContainer />
        <Routes>
          <Route path="/" element={<Login />} />
          <Route element={<Layout />}>
            <Route path="/dashboard" element={<Dashboard />} />
            <Route path="/admin/users" element={<UserList />} />
            <Route path="/admin/courses" element={<CourseList />} />
          </Route>
        </Routes>
      </div>
    </BrowserRouter>
  );
}

export default App;
