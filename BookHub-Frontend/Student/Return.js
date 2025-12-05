import React, { useState, useEffect } from "react";

const Return = () => {
  const [selectedBook, setSelectedBook] = useState("");
  const [message, setMessage] = useState("");
  const [loading, setLoading] = useState(false);
  const [Books, setBooks] = useState([]);

  useEffect(() => {
    const item = localStorage.getItem("Books");
    if (item) {
      setBooks(JSON.parse(item));
    }
    console.log(item);
  }, []);

  const handleReturn = async () => {
    const userId = parseInt(localStorage.getItem("studentId"));
    const token = localStorage.getItem("token");

    if (!userId) {
      setMessage("Student ID missing. Please load your profile first.");
      return;
    }

    const trimmedBookName = selectedBook.split("-")[0].trim();
    if (!trimmedBookName) {
      setMessage("Please select a book.");
      return;
    }

    setLoading(true);
    setMessage("");

    try {
      const confirmed = window.confirm(
        `Are you sure you want to return "${trimmedBookName}"?`
      );
      if (!confirmed) return;

      const response = await fetch(
        `http://localhost:5000/api/Student/Return/${userId}?BookName=${encodeURIComponent(
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
        setSelectedBook("");
        const updatedBooks = Books.filter((b) => b !== selectedBook);
        setBooks(updatedBooks);
        localStorage.setItem("Books", JSON.stringify(updatedBooks));
      } else {
        setMessage("Failed to return book: " + data);
      }
    } catch (err) {
      console.error(err);
      setMessage("Error returning book.");
    }

    setTimeout(() => setMessage(""), 2000);
    setLoading(false);
  };

  return (
    <div className="Return">
      <h2>Return Book</h2>

      {Books.length > 0 ? (
        <div style={{ marginBottom: "15px" }}>
          {Books.map((book, idx) => (
            <div key={idx}>
              <input
                type="radio"
                id={`book-${idx}`}
                name="selectedBook"
                value={book}
                checked={selectedBook === book}
                onChange={(e) => setSelectedBook(e.target.value)}
                style={{width:"20px"}}
              />
              <label htmlFor={`book-${idx}`} style={{ width:300 }}>
                {book}
              </label>
            </div>
          ))}
        </div>
      ) : (
        <p>No borrowed books.</p>
      )}

      <button onClick={handleReturn} disabled={loading || !selectedBook}>
        {loading ? "Processing..." : "Return"}
      </button>

      {message && (
        <p style={{ marginTop: "15px", fontWeight: "bold" }}>{message}</p>
      )}
    </div>
  );
};

export default Return;
