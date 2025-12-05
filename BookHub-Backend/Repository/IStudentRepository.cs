using LibraryManagement.Models;
using System.Collections.Generic;

namespace LibraryManagement.Repositories
{
    public interface IStudentRepository
    {
        Student GetStudentById(int id);
        Student GetStudentByEmail(string email);
        IEnumerable<Book> GetAllBooks();
        Book SearchBook(string bookName);
        bool BorrowBook(int studentId, string bookName);
        bool ReturnBook(int studentId, string bookName);
        bool Save();
    }
}
