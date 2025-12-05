import React from 'react'
import {useState} from 'react';

const UpdateUser = () => {
  const[student,setStudent]=useState('')
  const[message,setMessage]=useState('')
  const[id,setId]=useState();
  const[name,setName]=useState('');
  const[year,setYear]=useState('');
  const[dept,setDept]=useState('');
  const[email,setEmail]=useState('');
  const[phone,setPhone]=useState('');
  const[password,setPassword]=useState('');

  const fetchData=async ()=>{
    const resp=await fetch(`http://localhost:5000/api/Admin/studentbyID/${id}`,{
      method:'GET',
      headers:{
        'Authorization':`Bearer ${token}`,
        'Content-Type':'Application/json'
      }
    });
    if(resp.ok)
    {
      const data=await resp.json();
      console.log(data);
      setName(data.studentName || '');
      setYear(data.year || '');
      setDept(data.department);
      setEmail(data.email || '');
      setPhone(data.phone?.toString() || '');
      setStudent(data);
      setPassword(data.password);
    }
    else{
      setMessage("Error occured "+resp.status);
      setTimeout(() => setMessage(""),2000);
      setStudent('');
      setId('');
    setName('');
    setYear('');
    setDept('');
    setEmail('');
    setPhone('');
    setPassword('');
    }
  }

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
    setStudent('');
      setId('');
    setName('');
    setYear('');
    setDept('');
    setEmail('');
    setPhone('');
    setPassword('');
  }

  const token=localStorage.getItem("token");
  const handleUpdate= async (e)=>{
    e.preventDefault();
    const response=await fetch(`http://localhost:5000/api/Admin/students/${id}`,{
      method:'PATCH',
      headers:{
        "Authorization":`Bearer ${token}`,
        'Content-Type': 'application/json-patch+json'
      },
      body:JSON.stringify([
       { op: "replace", path: "/studentName", value: name },
       { op: "replace", path: "/year", value: year },
       { op: "replace", path: "/department", value: dept },
       { op: "replace", path: "/email", value: email },
       { op: "replace", path: "/phone", value: phone },
       { op: "replace", path: "/password", value: password }
      ])
    });
    if(!response.ok){
      const errorData = await response.json(); 
      setMessage(errorData?.Error?.[0] || "Something went wrong");
      setTimeout(() => {
      setMessage(" ");
      },2000);
      return;
    }
    const data= await response.json();
    setStudent(data);
    setMessage("Profile updated successfully!!")
    setId('');
    setName('');
    setYear('');
    setDept('');
    setEmail('');
    setPhone('');
    setPassword('');
   setTimeout(() => {
  setMessage("");
  },2000)
    console.log(data);
  }

  

  return (
    <div className='Update-container'>
      <div className='Update'>
      <h2>UPDATE PROFILE</h2>
      <input type='number' required placeholder='Enter Student ID to update' 
      value={id} onChange={(e)=>setId(e.target.value)} />
      <button onClick={fetchData}>Fetch data</button>
      {message && <p>{message}</p>}
      </div>
      <form className='Update-profile' onSubmit={handleUpdate}>
        <label>Name
        <input  autoFocus type='text' required placeholder='Name'
        value={name} onChange={(e)=>setName(e.target.value)}
        disabled={!student}/></label>
        <label>Year   
        <select className='dropdown' value={year} onChange={(e)=>setYear(e.target.value)} disabled={!student}> 
          <option value=''>--Select--</option>
          <option value='1'>1</option>
          <option value='2'>2</option>
          <option value='3'>3</option>
          <option value='4'>4</option>
        </select></label>
        <label>Department  
         <select className='dropdown' value={dept} onChange={(e)=>setDept(e.target.value)} disabled={!student}>
          <option value=''>--Select--</option>
          <option value='CSE'>CSE</option>
          <option value='ECE'>ECE</option>
          <option value='EEE'>EEE</option>
          <option value='MECH'>MECH</option>
          <option value='CIVIL'>CIVIL</option>
        </select></label>
        <label>Email  
        <input type='email' required placeholder='Email'
        value={email} onChange={(e)=>setEmail(e.target.value)} disabled={!student}
        /></label>
        <label>Phone  
        <input type='number' required placeholder='Phone no'
        minLength={10} maxLength={10}
        value={phone} onChange={(e)=>setPhone(e.target.value)} disabled={!student}
        /></label>
        <label>Password 
        <input type='password' required placeholder='Password'
        minLength={6} maxLength={20}
        value={password} onChange={(e)=>setPassword(e.target.value)} disabled={!student}
        /></label>
        <div className='button-group'>
        <button type='submit' disabled={!student}>Update</button>
        <button type='button' onClick={handleDelete} disabled={!student}>Delete</button>
        </div>
      </form>
    </div>
  )
}

export default UpdateUser