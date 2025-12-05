using LibraryManagement.Models;
using System.Collections.Generic;

namespace LibraryManagement.Repository
{
    public interface IAdminRepository
    {
        // Student operations
        IEnumerable<Student> GetAllStudents();
        Student GetStudentById(int id);
        Student GetStudentByName(string name);
        void AddStudent(Student student);
        void UpdateStudent(Student student);
        void DeleteStudent(Student student);
        bool StudentExists(string email, string phone);

        // Book operations
        IEnumerable<Student> GetStudentsWithBook(int bookId);
        void AddBook(Book book);
        bool BookExists(string bookName);

        // Admin operations
        IEnumerable<Admin> GetAllAdmins();
        void AddAdmin(Admin admin);
        bool AdminExists(string username);

        // Save changes
        void Save();
    }
}
