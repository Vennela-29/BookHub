import React from 'react'
import {useState} from 'react';

const CreateAdmin = () => {
    const[username,setUsername]=useState('');
    const[password,setPassword]=useState('');
    const[designation,setDesignation]=useState('');
    const[admin,setAdmin]=useState();
    const[message,setMessage]=useState('');
    const handleCreateAdmin=async(e)=>{
        e.preventDefault();
        const token=localStorage.getItem("token");
        const response=await fetch(`http://localhost:5000/api/Admin/CreateAdmin`,{
            method:'POST',
            headers:{
                "Authorization":`Bearer ${token}`,
                "Content-Type":"application/json"
            },
            body:JSON.stringify({
                UserName:username,
                Password:password,
                Designation:designation
            })
        });
        const Data = await response.json(); 
        if(!response.ok){
        setMessage(Data?.Error?.[0] || "Something went wrong");
        console.log(Data);
        setTimeout(() => {
            setMessage(" ");
        },2000);
        return;
        }
    setAdmin(Data);
    setMessage("Profile created successfully!!")
    console.log(Data);
    setUsername('');
    setDesignation('');
    setPassword('');
    setTimeout(() => setMessage(""),2000);
    console.log(Data);
  }

  return (
    <div className='create'>
        <h2>CREATE ADMIN PROFILE</h2>
        <form className='Create-profile' onSubmit={handleCreateAdmin}>
            <label>Username
            <input autoFocus type='text' required placeholder='Enter Username'
            value={username} onChange={(e)=>setUsername(e.target.value)}/></label>
            <label>Designation
            <input autoFocus type='text' required placeholder='Enter Designation'
            value={designation} onChange={(e)=>setDesignation(e.target.value)}/></label>
            <label>Password
            <input autoFocus type='password' required placeholder='Enter Password'
            value={password} onChange={(e)=>setPassword(e.target.value)}/></label>
            <button type='submit'>Create</button>
        </form>
        {message &&
        <div className={`message-box ${message.includes("successfully") ? "success" : "error"}`}>
            {message}
        </div>
        }
    </div>
  )
}

export default CreateAdmin