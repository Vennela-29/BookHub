import React, { useState ,useEffect} from "react";

const Borrow = () => {
  const [bookName, setBookName] = useState("");
  const [message, setMessage] = useState("");
  const [loading, setLoading] = useState(false);
  const [books,setBooks]=useState([]);

     const fetchBooks = async ()=>
      {
      const token = localStorage.getItem("token");
      const response=await fetch("http://localhost:5000/api/Student/ViewAllBooks",{
        method:'GET',
        headers:{Authorization:`Bearer ${token}`},
      });
    try{
      if(response.ok)
      {
        const data=await response.json();
        setBooks(data);
  }
      else{
        console.error("Failed to fetch books - Response is not OK",response.status);
      }
    }
    catch(err)
    {
      console.log("Error Occured while fetching Books");
    }
    };

  useEffect(()=>
    {
    fetchBooks();
  },[]);
   

  const handleBorrow = async () => {
    const userId = localStorage.getItem("studentId"); 
    const token = localStorage.getItem("token");

    if (!userId) {
      setMessage("Student ID missing. Please load your profile first.");
      return;
    }

    const trimmedBookName = bookName.trim();
    if (!trimmedBookName) {
      setMessage("Please enter a Book Name.");
      return;
    }

    setLoading(true);
    setMessage("");

    try {
      const response = await fetch(
        `http://localhost:5000/api/Student/Borrow/${userId}?BookName=${encodeURIComponent(
        trimmedBookName
      )}`,
        {
          method: "PUT",
          headers: {
            Authorization: `Bearer ${token}`,
            "Content-Type": "application/json",
          },
        }
      );

      const data = await response.text();

      if (response.ok) {
        setMessage(data);
        setBookName(""); 
        await fetchBooks();
      } else {
        setMessage("" + data);
        setBookName("");
      }
    } catch (err) {
      console.error(err);
      setMessage("Error borrowing book."+err);
    }
    setTimeout(()=>setMessage(""),2000);
    setLoading(false);
  };
  return (
    <div className="Borrow">
      <h2>Borrow Book</h2>
      <input autoFocus
        type="text"
        placeholder="Enter Book Name"
        value={bookName}
        onChange={(e) => setBookName(e.target.value)}
      />
      <button onClick={handleBorrow} disabled={loading}>
        {loading ? "Processing..." : "Borrow"}
      </button>
      {message && (
        <p style={{ marginTop: "15px", fontWeight: "bold" }}>{message}</p>
      )}
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
        {books.map((book, index) => (
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
  );
};

export default Borrow;
