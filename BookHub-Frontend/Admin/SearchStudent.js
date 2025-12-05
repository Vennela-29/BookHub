import React from 'react'
import {useState,useEffect} from 'react';
const SearchUser = ({students,onFilter}) => {
  const[search,setSearch]=useState('');

  useEffect(()=>{
    if (!Array.isArray(students)) {
      onFilter([]);
      return;
    }
      try{
      const data=students.filter(stud=>stud.studentName.toLowerCase().includes(search.toLowerCase()) 
    || stud.borrowedList?.some(book => book.toLowerCase().includes(search.toLowerCase()))  || 
      stud.department.toLowerCase().includes(search.toLowerCase()) || 
      stud.year.toString() === search.toString());
      onFilter(data);
      }
      catch(err){
        console.log('Error occured',err);
      }
  },[search,students,onFilter])

  return (
    <div className='search-student'>
      <input autoFocus type='text' placeholder='Search Student , Dept ,Year or Book' value={search}
      onChange={(e)=>setSearch(e.target.value)}
      />
    </div>
  )
}

export default SearchUser