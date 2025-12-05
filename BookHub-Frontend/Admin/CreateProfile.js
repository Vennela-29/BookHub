import React from 'react'
import {useState} from 'react';
import { useNavigate } from 'react-router-dom';

const CreateUser = () => {
  const[student,setStudent]=useState('')
  const[message,setMessage]=useState('')
  const[name,setName]=useState('')
  const[year,setYear]=useState('')
  const[dept,setDept]=useState('')
  const[email,setEmail]=useState('')
  const[password,setPassword]=useState('')
  const[phone,setPhone]=useState('')
  const navigate=useNavigate();

  const token=localStorage.getItem("token");
  const handleCreate= async (e)=>{
    e.preventDefault();
    const response=await fetch(`http://localhost:5000/api/Admin/CreateStudent`,{
      method:'POST',
      headers:{
        "Authorization":`Bearer ${token}`,
        'Content-Type': 'application/json'
      },
      body:JSON.stringify({
        studentName:name,
        Year:year,
        Department:dept,
        email:email,
        phone:phone,
        password:password
      })
    });
    if(!response.ok){
      const errorData = await response.json(); 
      setMessage(errorData?.Error?.[0] || "Something went wrong");
      setTimeout(() => {
      setMessage(" ");
      },2000);
      // navigate('/Admin'); 
      return;
    }
    const data= await response.json();
    setStudent(data);
    setMessage("Profile created successfully!!")
    setName('');
    setYear('');
    setDept('');
    setEmail('');
    setPhone('');
    setPassword('');
   setTimeout(() => {
  setMessage("");
  },2000)
  // navigate('/Admin'); 
    console.log(data);
  }

  return (
    <div className='create'>
      <h2>CREATE  STUDENT PROFILE</h2>
      <form className='Create-profile' onSubmit={handleCreate}>
        <label>Name
        <input  autoFocus type='text' required placeholder='Name'
        value={name} onChange={(e)=>setName(e.target.value)}
        /></label>
        <label>Year   
        <select className='dropdown' value={year} onChange={(e)=>setYear(e.target.value)}> 
          <option value=''>--Select--</option>
          <option value='1'>1</option>
          <option value='2'>2</option>
          <option value='3'>3</option>
          <option value='4'>4</option>
        </select></label>
        <label>Department  
         <select className='dropdown' value={dept} onChange={(e)=>setDept(e.target.value)}>
          <option value=''>--Select--</option>
          <option value='CSE'>CSE</option>
          <option value='ECE'>ECE</option>
          <option value='EEE'>EEE</option>
          <option value='MECH'>MECH</option>
          <option value='CIVIL'>CIVIL</option>
        </select></label>
        <label>Email  
        <input type='email' required placeholder='Email'
        value={email} onChange={(e)=>setEmail(e.target.value)}
        /></label>
        <label>Phone  
        <input type='number' required placeholder='Phone no'
        minLength={10} maxLength={10}
        value={phone} onChange={(e)=>setPhone(e.target.value)}
        /></label>
        {phone.length > 0 && phone.length !== 10 && (
        <span style={{color: 'red', fontSize:12}}>Phone number must be 10 digits</span>
  )}
        <label>Password  
          <input type='password' required placeholder='Password'
          minLength={6} maxLength={20}
          value={password} onChange={(e)=>setPassword(e.target.value)}
          />
        </label>
        {((password >0 && password.length <6)  || password.length >20) && (
        <span style={{color: 'red'}}>Password must be atleast 6 and atmost 20 characters</span>
        )}
        <button type='submit' id='submit-button'>Submit</button>
      </form>
      {message &&
      <div className={`message-box ${message.includes("successfully") ? "success" : "error"}`}>
        {message}
      </div>
      }
    </div>
  )
}

export default CreateUser