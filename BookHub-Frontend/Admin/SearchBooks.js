import React from 'react'
import {useState,useEffect} from 'react';
const SearchBooks = ({book,onFilter}) => {
    const[search,setSearch]=useState('');
    useEffect(()=>{
    if (!Array.isArray(book)) {
      onFilter([]);
      return;
    }
      try{
      const data=book.filter(b=>b.bookName.toLowerCase().includes(search.toLowerCase()) || b.author.toLowerCase().includes(search.toLowerCase()));
      onFilter(data);
      }
      catch(err){
        console.log('Error occured',err);
      }
  },[search,book,onFilter])


  return (
    <div className='search-book'>
      <input autoFocus type='text' placeholder='Search book or Author' value={search}
      onChange={(e)=>setSearch(e.target.value)}
      />
    </div>
  )
}

export default SearchBooks