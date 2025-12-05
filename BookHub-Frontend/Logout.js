import React from 'react'
import {useNavigate} from 'react-router-dom';

const Logout = () => {
    const navigate=useNavigate();
    const handleLogout=async()=>{
        localStorage.removeItem('token');
        localStorage.removeItem('username');
        localStorage.removeItem('role');
        navigate('/Login');
    }

  return (
    <div>
        <button type='button' onClick={handleLogout}>Logout</button>
    </div>
  )
}

export default Logout