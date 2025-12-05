using AutoMapper;
using LibraryManagement.Controllers;
using LibraryManagement.Models;
using LibraryManagement.Repositories;
using LibraryManagement.Repository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Xunit;

namespace LibraryManagement.Tests
{
    public class AdminControllerTests
    {
        private readonly LibraryDBContext _context;
        private readonly AdminRepository _adminRepo;
        private readonly IMapper _mapper;
        private readonly AdminController2 _controller;
        //Constructor
        public AdminControllerTests()
        {
            var options = new DbContextOptionsBuilder<LibraryDBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new LibraryDBContext(options);
            // Seed test data
            SeedData();
            _adminRepo = new AdminRepository(_context);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Student, StudentDTO>());
            _mapper = config.CreateMapper();
            _controller = new AdminController2(_adminRepo, _mapper);
        }
        //In memeory data
        private void SeedData()
        {
            _context.Students.AddRange(
                new Student { Id = 1, StudentName = "A", Email = "A@gmail.com", Phone = "8946757574", Password = "A1234", Department = "CSE", Year = 2 },
                new Student { Id = 2, StudentName = "B", Email = "B@gmail.com", Phone = "9476774622", Password = "B1234", Department = "ECE", Year = 3 }
            );

            _context.Books.AddRange(
                new Book { BookId = 1, BookName = "Book1", Author = "Author1", AvailableCopies = 2 },
                new Book { BookId = 2, BookName = "Book2", Author = "Author2", AvailableCopies = 1 }
            );

            _context.Admins.Add(new Admin { AdminId = 1, UserName = "Admin1", Designation = "Admin", Password = "Admin123" });

            _context.BorrowedBooks.Add(new BorrowedBook { bookId = 1, studentId = 1 });

            _context.SaveChanges();


        }
        //Students exist
        [Fact]
        public void GetAllStudents_ReturnsOk_WithStudents()
        {
            var result = _controller.GetAllStudents();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var students = Assert.IsAssignableFrom<IEnumerable<object>>(okResult.Value);
            Assert.NotEmpty(students);
        }
        //Student ID exists in In memory database
        [Fact]
        public void GetStudentByID_ReturnsOk_WhenValidId()
        {
            var result = _controller.GetStudentByID(1);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var student = Assert.IsType<Student>(okResult.Value);
            Assert.Equal("A", student.StudentName);
        }
        //ID doesn't exist
        [Fact]
        public void GetStudentByID_ReturnsNotFound_WhenIdNotExist()
        {
            var result = _controller.GetStudentByID(999);
            var notFound = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Student not found", notFound.Value);
        }
        //Null student details passed
        [Fact]
        public void CreateStudent_ReturnsBadRequest_WhenNull()
        {
            var result = _controller.CreateStudent(null);
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid details", badRequest.Value);
        }
        //Student already exists - checked with email & phone
        [Fact]
        public void CreateStudent_ReturnsConflict_WhenEmailExists()
        {
            var student = new Student { StudentName = "A", Email = "A@gmail.com", Phone = "8946757574", Password = "A1234", Department = "CSE", Year = 2 };
            var result = _controller.CreateStudent(student);
            var conflict = Assert.IsType<ConflictObjectResult>(result);
            //var apiRes = Assert.IsType<APIResponse>(conflict.Value);
            Assert.Contains("Invalid or existing student", conflict.Value.ToString());
        }
        //Vaid student is created
        [Fact]
        public void CreateStudent_ReturnsOk_WhenValid()
        {
            var student = new Student { StudentName = "C", Email = "C@gmail.com", Phone = "7765267748", Password = "C1234", Department = "CSE", Year = 1 };
            var result = _controller.CreateStudent(student);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var newStudent = Assert.IsType<Student>(okResult.Value);
            Assert.Equal("C", newStudent.StudentName);
        }
        //Null details to be updated
        [Fact]
        public void UpdateStudent_ReturnsBadRequest_WhenNull()
        {
            JsonPatchDocument<Student> patch = null;
            var result = _controller.UpdateStudent(1, patch);
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Null details", badRequest.Value);
        }
        //Correct details to be updated
        [Fact]
        public void UpdateStudent_ReturnsOk_WhenValid()
        {
            var patch = new JsonPatchDocument<Student>();
            patch.Replace(s => s.StudentName, "UpdatedName");
            var result = _controller.UpdateStudent(1, patch);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var updatedStudent = Assert.IsType<Student>(okResult.Value);
            Assert.Equal("UpdatedName", updatedStudent.StudentName);
        }
        //Student gets deleted
        [Fact]
        public void DeleteStudent_ReturnsOk_WhenValid()
        {
            var result = _controller.DeleteStudent(2);
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Student deleted successfully", okResult.Value);
        }
        //Student doesn't exist - can't delete
        [Fact]
        public void DeleteStudent_ReturnsNotFound_WhenIdNotExist()
        {
            var result = _controller.DeleteStudent(999);
            var notFound = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Student not found", notFound.Value);
        }
        //Invalid details passed
        [Fact]
        public void DeleteStudent_ReturnsBadRequest_WhenInvalidId()
        {
            var result = _controller.DeleteStudent(-5);
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid ID", badRequest.Value);
        }
    }
}
