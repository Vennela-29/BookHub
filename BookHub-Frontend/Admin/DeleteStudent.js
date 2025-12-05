import React, { useState } from 'react';

const DeleteUser = () => {
  // const[student,setStudent]=useState();
  const[id,setId]=useState();
  const[message,setMessage]=useState('');


  const handleDelete=async ()=>{
    const confirmed = window.confirm("Are you sure you want to delete this student?");
  if (!confirmed) return;
    const token=localStorage.getItem('token');
    const response=await fetch(`http://localhost:5000/api/Admin/students/${id}`,{
      method:'DELETE',
      headers:{
        "Authorization":`Bearer ${token}`,
        'Content-Type':'application/json'
      }
    });
    const data=await response.text();
      // console.log('Error');
      const errorMessage = data || "Invalid ID or profile not found";
      setMessage(errorMessage);
      // console.log('Deleted');
    setTimeout(() => setMessage(""),2000);
  }


  return (
    <div className='Delete'>
      <h2>Delete Student</h2>
      <div className='items'>
      <input type='text' required placeholder='Enter ID to delete student' 
      value={id} onChange={(e)=>setId(e.target.value)}/>
      <button type='button' onClick={handleDelete}>Delete</button>
      {message &&
      <div className={`message-box ${message.includes("successfully") ? "success" : "error"}`}>
        {message}
      </div>
      }
      </div>
    </div>
  )
}

export default DeleteUser