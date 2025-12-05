import React, { useState } from 'react'

const AddBook = () => {
    const[book,setBook]=useState();
    const[bookName,setBookName]=useState('');
    const[author,setAuthor]=useState('');
    const[copies,setCopies]=useState();
    const[message,setMessage]=useState('');
    const handleAdd= async (e)=>
    {
        e.preventDefault();
        const token=localStorage.getItem("token");
        const response=await fetch(`http://localhost:5000/api/Admin/AddBook`,{
            method:'POST',
            headers:{
                "Authorization":`Bearer ${token}`,
                "Content-Type":"application/json"
            },
            body:JSON.stringify({
                BookName:bookName,
                Author:author,
                AvailableCopies:copies
            })
        });
        const data=await response.json()
        if(!response.ok){
            setMessage("Response:"+response.statusText);
            setTimeout(()=>setMessage(""),2000);
            console.log("error"+data);
            return;
        }
        setBook(data);
        console.log("success"+data);
        setMessage("Book added successfully!!");
        setTimeout(()=>setMessage(""),2000);
        setCopies('');
        setAuthor('')
        setBookName('');
    }


  return (
    <div>
        <form className='AddBook' onSubmit={handleAdd}>
            <h2>ADD BOOK TO LIBRARY</h2>
        <label>Book Name <input type='text' required placeholder='Book Name' value={bookName} 
        onChange={(e)=>setBookName(e.target.value)}
        /></label>
        <label>Author <input type='text' required placeholder='Author Name' value={author} 
        onChange={(e)=>setAuthor(e.target.value)}
        /></label>
        <label>No of Copies <input type='number' required placeholder='Copies' value={copies} 
        onChange={(e)=>setCopies(e.target.value)}
        /></label>
        <button type='submit'>ADD</button>
        {message &&
        <div className={`message-box ${message.includes("successfully") ? "success" : "error"}`}>
            {message}
        </div>
        }
        </form>

    </div>
  )
}

export default AddBook