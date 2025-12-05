import React from 'react'
import {useState,useEffect} from 'react';
import { useNavigate } from 'react-router-dom';
import Profile from './Profile';
import ViewBooks from './ViewBooks';
import Borrow from './Borrow';
import Return from './Return';
import WinWireLogo from '../WinWireLogo.png';

const Students = () => {
  const userName = localStorage.getItem("username");
  const[activePage,setActivePage]=useState("Profile");
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
    <div className='Student-profile'>
            <div className='sidebar'>
        <h1>
  <span style={{ color: "#124B84" }}>Book</span>
  <span style={{ color: "#C74627" }}>Hub</span>
</h1>
    <h2 style={{fontStyle:"Italic"}}>Welcome,{userName.split('@')[0]}!</h2>
        <ul>
          <li onClick={() => setActivePage('Profile')}>Profile</li>
          <li onClick={() => setActivePage('ViewBooks')}>View Books</li>
          <li onClick={() => setActivePage('Borrow')}>Borrow Book</li>
          <li onClick={() => setActivePage('Return')}>Return Book</li>
          <li onClick={handleLogout}>Logout</li>
        </ul>
        </div>
        <div className="main-area">
        <img src={WinWireLogo} alt='Winwire Logo'/>
        {activePage === 'Profile' && <Profile />}
        {activePage === 'ViewBooks' && <ViewBooks />}
        {activePage === 'Borrow' && <Borrow />}
        {activePage === 'Return' && <Return />}
      </div>
      </div>
  )
}

export default Students