using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManagement.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly LibraryDBContext _context;

        public StudentRepository(LibraryDBContext context)
        {
            _context = context;
        }

        public Student GetStudentById(int id)
        {
            return _context.Students
                .Include(s => s.BorrowedList)
                .ThenInclude(b => b.book)
                .FirstOrDefault(s => s.Id == id);
        }

        public Student GetStudentByEmail(string email)
        {
            return _context.Students
                .Include(s => s.BorrowedList)
                .ThenInclude(b => b.book)
                .FirstOrDefault(s => s.Email == email);
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _context.Books.ToList();
        }

        public Book SearchBook(string bookName)
        {
            return _context.Books.FirstOrDefault(b => b.BookName.ToLower().Contains(bookName.ToLower()));
        }

        public bool BorrowBook(int studentId, string bookName)
        {
            var student = GetStudentById(studentId);
            var book = _context.Books.FirstOrDefault(b => b.BookName.ToLower() == bookName.ToLower());

            if (student == null || book == null) return false;

            if (student.BorrowedList.Count >= 3) return false;

            bool alreadyOwns = _context.BorrowedBooks.Any(bb => bb.studentId == studentId && bb.bookId == book.BookId);
            if (alreadyOwns) return false;

            if (book.AvailableCopies <= 0) return false;

            var borrowedBook = new BorrowedBook
            {
                studentId = studentId,
                bookId = book.BookId,
                BorrowedOn = DateTime.Now.ToShortDateString()
            };

            _context.BorrowedBooks.Add(borrowedBook);
            book.AvailableCopies--;
            Save();
            return true;
        }

        public bool ReturnBook(int studentId, string bookName)
        {
            var student = GetStudentById(studentId);
            var book = _context.Books.FirstOrDefault(b => b.BookName.ToLower() == bookName.ToLower());

            if (student == null || book == null) return false;

            var borrowedBook = _context.BorrowedBooks
                .FirstOrDefault(bb => bb.studentId == studentId && bb.bookId == book.BookId);

            if (borrowedBook == null) return false;

            _context.BorrowedBooks.Remove(borrowedBook);
            book.AvailableCopies++;
            Save();
            return true;
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
