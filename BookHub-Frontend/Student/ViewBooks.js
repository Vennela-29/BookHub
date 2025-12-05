import React from 'react'
import { useState,useEffect } from 'react';
import Search from './Search';
const ViewBooks = () => {
const[books,setBooks]=useState([]);
const[filteredBooks,setFilteredBooks]=useState([]);
useEffect(()=>
  {
  const fetchBooks = async ()=>
    {
    const token = localStorage.getItem("token");
    try{
    const response=await fetch("http://localhost:5000/api/Student/ViewAllBooks",{
      method:'GET',
      headers:{Authorization:`Bearer ${token}`},
    });
    if(response.ok)
    {
      const data=await response.json();
      setBooks(data);
      setFilteredBooks(data);
}
    else{
      console.log("Failed to fetch books - Response is not OK",response.status);
    }
  }

  catch(err)
  {
    console.error("Error Occured while fetching Books"+err);
  };
}
  fetchBooks();
},[]);

  return (
    <div className='Books-container'>
      {books.length ===0 &&
      <p>No books Found</p>}
      <Search books={books} onFilter={setFilteredBooks} />
      {filteredBooks.length>0 &&
      <table border='1' cellPadding='8' cellSpacing='0'>
        <thead>
        <tr>
          <th>Book ID</th>
          <th>Book Name</th>
          <th>Author</th>
          <th>Available copies</th>
        </tr>
        </thead>
        <tbody>
        {filteredBooks.map((book, index) => (
        <tr key={index}>
          <td>{book.bookId}</td>
          <td>{book.bookName}</td>
          <td>{book.author}</td>
          <td>{book.availableCopies}</td>
        </tr>
        ))}
        </tbody>
      </table>
      }
      {filteredBooks.length ===0 &&
      <p>Book or Author not found</p>
      }
    </div>
  )
}

export default ViewBooks