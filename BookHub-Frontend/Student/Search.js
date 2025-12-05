import React from 'react'
import {useState,useEffect} from 'react';
const Search = ({books,onFilter}) => {

  const[search,setSearch]=useState('');
  const[message,setMessage]=useState('');
  
useEffect(()=>{
  try{
    const response=books.filter(b=>b.bookName.toLowerCase().includes(search.toLowerCase()) || b.author.toLowerCase().includes(search.toLowerCase()));
    onFilter(response);
  }
  catch(err){
    setMessage("Error occured"+err);
  }
},[search,books])
  return (
    <div className='Search'>
      <input  autoFocus type='text' required placeholder='Search Book or Author' 
      value={search}
      onChange={(e)=>setSearch(e.target.value)}/>
      <p>{message}</p>
    </div>
  )
}

export default Search