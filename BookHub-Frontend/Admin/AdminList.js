import React from 'react'
import {useState,useEffect} from 'react';

const AdminList = () => {
    const[admin,setAdmin]=useState([]);
    const[message,setMessage]=useState('');
    useEffect(()=>{
        const token=localStorage.getItem("token");
        const fetchAdmins=async()=>{
          try{
          const response=await fetch(`http://localhost:5000/api/Admin/GetAllAdmins`,{
            method:'GET',
            headers:{'Authorization':`Bearer ${token}`}
          });
          if(!response.ok){
            setMessage(`Error ${response.status}`)
          }
          const data=await response.json();
          if(data.length===0){
            setMessage("Admin table is empty");
          }
            setAdmin(data);
        }
        catch(err){
          setMessage("Error occured while fetching Students");
        }
        };
        fetchAdmins();
      },[])  
    

  return (
    <div>
        {admin.length>0 &&
        <div className='Admin-table'>
        <p style={{fontSize:12}}>{admin.length} entries</p>
        {message &&
        <p>{message}</p>}
        <table border='1' cellPadding='10' style={{borderCollapse:'collapse'}}>
            <thead>
                <tr>
                    <th>Admin Id</th>
                    <th>Username</th>
                    <th>Designation</th>
                </tr>
            </thead>
            <tbody>
            {admin.map((admin,index)=>(
                <tr key={index}>
                    <td>{admin.adminId}</td>
                    <td>{admin.userName}</td>
                    <td>{admin.designation}</td>
                </tr>
            ))}
            </tbody>
        </table>
        </div>
        }
    </div>
  )
}

export default AdminList