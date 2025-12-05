import React, { useState,useEffect } from "react";
import { useNavigate } from "react-router-dom";
import StudentsList from "./StudentsList";
import StudentsByBook from "./StudentsByBook";
import CreateProfile from "./CreateProfile";
import UpdateStudent from "./UpdateStudent";
import DeleteStudent from "./DeleteStudent";
import CreateAdmin from './CreateAdmin';
import AdminList from './AdminList';
import AddBook from './AddBook';
import WinWireLogo from '../WinWireLogo.png';
// import Logout from '../Logout';

const AdminDashboard = () => {
  const [activeTab, setActiveTab] = useState("home");
  const navigate=useNavigate();
useEffect(() => {
    const token = localStorage.getItem("token");
    if (!token) {
      navigate("/");
    }
  }, [navigate]);

  const handleLogout=async()=>{
        localStorage.removeItem('token');
        localStorage.removeItem('username');
        localStorage.removeItem('role');
        navigate("/");
    }

  return (
    <div className="dashboard-container">

      <aside className="sidebar">
        <h1>
  <span style={{ color: "#124B84" }}>Book</span>
  <span style={{ color: "#C74627" }}>Hub</span>
</h1>
        <h2 style={{fontStyle:"Italic"}}>Admin Panel</h2>

        <button onClick={() => setActiveTab("students")}>Student - List</button>
        <button onClick={() => setActiveTab("create-student")}>Create Student</button>
        <button onClick={() => setActiveTab("create-admin")}>Create Admin</button>
        <button onClick={() => setActiveTab("update")}>Modify Profile</button>
        <button onClick={() => setActiveTab("book")}>View books</button>
        <button onClick={() => setActiveTab("add-book")}>Add book</button>
        <button onClick={() => setActiveTab("admin-list")}>View Admins</button>
        <button onClick={handleLogout}>Logout</button>
        
      </aside>

      <main className="main-content">
         <img src={WinWireLogo} alt='Winwire Logo'/>
        {activeTab === "students" && <StudentsList />}
        {activeTab === "create-student" && <CreateProfile />}
        {activeTab === "create-admin" && <CreateAdmin />}
        {activeTab === "update" && <UpdateStudent />}
        {activeTab === "delete" && <DeleteStudent />}
        {activeTab === "admin-list" && <AdminList />}
        {activeTab === "book" && <StudentsByBook />}        
        {activeTab === "add-book" && <AddBook />}        
        {/* {activeTab === "logout" && <Logout />}         */}


        {activeTab === "home" && (
          <div>
           <h1>LIBRARY MANAGEMENT SYSTEM</h1>
          <h2>Welcome to Admin Dashboard</h2>
         
          </div>
        )}
      </main>
    </div>
  );
};

export default AdminDashboard;
