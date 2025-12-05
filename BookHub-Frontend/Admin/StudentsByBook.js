import React from 'react'
import {useState,useEffect} from 'react';
import SearchBooks from './SearchBooks';

const BookHolder = () => {
  const[filteredBooks,setFilteredBooks]=useState([]);
  const[book,setBook]=useState('');
  const[message,setMessage]=useState('');
  useEffect(()=>{
    const token=localStorage.getItem("token");
  const fetchBooks=async()=>{
    const response=await fetch(`http://localhost:5000/api/Student/ViewAllBooks`,{
      method:'GET',
      headers:{
        "Authorization":`Bearer ${token}`,
        "Content-Type":"application/json"
      }
    });
    if(!response.ok){
      setMessage("Book not assigned to anyone");
      return;
    }
    const data=await response.json();
      if(data.length===0){
        setMessage("Books table is empty");
      }
        setBook(data);
        setFilteredBooks(data); 
  };
  fetchBooks();
},[]);
  return (
    <div>
     <SearchBooks book={book} onFilter={setFilteredBooks}/>
     {message &&
          <p>{message}</p>
        }
        {filteredBooks.length ===0 && !message &&
        <p>Data not found</p>
          }
       {filteredBooks.length>0 &&
       <div className='book-table'>
        <p style={{fontSize:12}}>{filteredBooks.length} entries</p>
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
      </div>
       }
    </div>
  )
}

export default BookHolder;