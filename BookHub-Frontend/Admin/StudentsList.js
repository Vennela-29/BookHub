import React from 'react'
import {useState,useEffect} from 'react';
import SearchStudent from './SearchStudent';

const AllUsers = () => {
  const[filteredStudents,setFilteredStudents]=useState([]);
  const[students,setStudents]=useState([]);
  const [message,setMessage]=useState('');
  useEffect(()=>{
    const token=localStorage.getItem("token");
    const fetchStudents=async()=>{
      try{
      const response=await fetch(`http://localhost:5000/api/Admin/GetAllStudents`,{
        method:'GET',
        headers:{'Authorization':`Bearer ${token}`}
      });
      if(!response.ok){
        setMessage(`Error ${response.status}`)
        return;
      }
      const data=await response.json();
      if(data.length===0){
        setMessage("Student data not found");
      }
        setStudents(data);
        setFilteredStudents(data); 
    }
    catch(err){
      setMessage("Error occurred while fetching Students");
    }
    };
    fetchStudents();
  },[])  


  return (
    <div className='student-list'>
      {/* <h2>Student List</h2> */}
       <SearchStudent 
       students={students}
       onFilter={setFilteredStudents}
       />
       {message &&
          <p>{message}</p>
        }
        {filteredStudents.length ===0 && !message &&
        <p>Data not found</p>
          }
       {filteredStudents.length>0 &&
       <div className='student-table'>
        <p style={{fontSize:12}}>{filteredStudents.length} entries</p>
        <table border='1' cellPadding='10' style={{borderCollapse:'collapse'}}>
          <thead>
            <tr>
              <th>Id</th>
              <th>Name</th>
              <th>Year</th>
              <th>Dept</th>
              <th>Email</th>
              <th>Phone</th>
              <th>Books</th>
              {/* <th>Borrowed on</th> */}
              <th>Overdue on</th>
            </tr>
          </thead>
          <tbody>
        {filteredStudents.map((student,index)=>(
          <tr key={index}>
            <td>{student.id}</td>
            <td>{student.studentName}</td>
            <td>{student.year}</td>
            <td>{student.department}</td>
            <td>{student.email}</td>
            <td>{student.phone}</td>
            <td style={{ whiteSpace: "pre-line" }}>{student.borrowedBooks?.join("\n") || "-"}</td>
            {/* <td>{student.date1?.join(",") || " "}</td> */}
            <td style={{ whiteSpace: "pre-line" }}>{student.date2?.join(" \n") || "-"}</td>
          </tr>
        ))}
        </tbody>
        </table>  
       </div>
       }
    </div>
  )
}

export default AllUsers