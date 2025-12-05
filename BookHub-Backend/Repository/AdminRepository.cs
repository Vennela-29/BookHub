using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManagement.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly LibraryDBContext _dbContext;

        public AdminRepository(LibraryDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // ------------------------
        // STUDENT OPERATIONS
        // ------------------------
        public IEnumerable<Student> GetAllStudents()
        {
            return _dbContext.Students
                .Include(s => s.BorrowedList)
                .ThenInclude(b => b.book)
                .ToList();
        }

        public Student GetStudentById(int id)
        {
            return _dbContext.Students
                .Include(s => s.BorrowedList)
                .ThenInclude(b => b.book)
                .FirstOrDefault(s => s.Id == id);
        }

        public Student GetStudentByName(string name)
        {
            return _dbContext.Students
                .Include(s => s.BorrowedList)
                .ThenInclude(b => b.book)
                .FirstOrDefault(s => s.StudentName == name);
        }

        public void AddStudent(Student student)
        {
            _dbContext.Students.Add(student);
        }

        public void UpdateStudent(Student student)
        {
            _dbContext.Students.Update(student);
        }

        public void DeleteStudent(Student student)
        {
            _dbContext.Students.Remove(student);
        }

        public bool StudentExists(string email, string phone)
        {
            return _dbContext.Students.Any(s => s.Email == email || s.Phone == phone);
        }

        // ------------------------
        // BOOK OPERATIONS
        // ------------------------
        public void AddBook(Book book)
        {
            _dbContext.Books.Add(book);
        }

        public bool BookExists(string bookName)
        {
            return _dbContext.Books.Any(b => b.BookName == bookName);
        }

        public IEnumerable<Student> GetStudentsWithBook(int bookId)
        {
            return _dbContext.Students
                .Include(s => s.BorrowedList)
                .ThenInclude(b => b.book)
                .Where(s => s.BorrowedList.Any(bb => bb.bookId == bookId))
                .ToList();
        }

        // ------------------------
        // ADMIN OPERATIONS
        // ------------------------
        public IEnumerable<Admin> GetAllAdmins()
        {
            return _dbContext.Admins.ToList();
        }

        public void AddAdmin(Admin admin)
        {
            _dbContext.Admins.Add(admin);
        }

        public bool AdminExists(string username)
        {
            return _dbContext.Admins.Any(a => a.UserName == username);
        }

        // ------------------------
        // SAVE CHANGES
        // ------------------------
        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
