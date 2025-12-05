using AutoMapper;
using LibraryManagement.Controllers;
using LibraryManagement.Models;
using LibraryManagement.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Xunit;

namespace LibraryManagement.Testing.StudentTests
{
    public class StudentControllerTests
    {
        private readonly LibraryDBContext _db;
        private readonly StudentController2 _controller;
        private readonly IStudentRepository _repo;
        //Constructor
        public StudentControllerTests()
        {
            var options = new DbContextOptionsBuilder<LibraryDBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _db = new LibraryDBContext(options);

            SeedDatabase();

            var mapperCfg = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Student, StudentDTO>();
            });

            var mapper = mapperCfg.CreateMapper();

            _repo = new StudentRepository(_db);
            _controller = new StudentController2(_repo, mapper);
        }

        private void SeedDatabase()
        {
            var student = new Student
            {
                Id = 1,
                StudentName = "ABC",
                Email = "ABC@gmail.com",
                Department = "CSE",
                Year = 2,
                Phone = "8234567890",
                Password = "ABC1234"
            };

            _db.Students.Add(student);

            var book1 = new Book { BookId = 1, BookName = "Book1", Author = "Author1", AvailableCopies = 2 };
            var book2 = new Book { BookId = 2, BookName = "Book2", Author = "Author2", AvailableCopies = 1 };

            _db.Books.AddRange(book1, book2);
            _db.SaveChanges();
        }

        private void SetUser(string email, string role)
        {
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, email),
                        new Claim(ClaimTypes.Role, role)
                    }, "mock"))
                }
            };
        }

        //Profile for Valid details
        [Fact]
        public void ViewProfile_ForStudent_ReturnsOk()
        {
            //Arrange
            SetUser("ABC@gmail.com", "Student");
            //Act
            var result = _controller.ViewProfile();
            //Assert
            var ok = Assert.IsType<OkObjectResult>(result.Result);

            Assert.NotNull(ok.Value);
        }
        //Boks fetched correctly or not
        [Fact]
        public void ViewAllBooks_ReturnsOk()
        {
            //Act
            var result = _controller.ViewAllBooks();
            //Assert
            var ok = Assert.IsType<OkObjectResult>(result.Result);

            var books = Assert.IsAssignableFrom<IEnumerable<object>>(ok.Value);
            Assert.True(books.Any());
        }
        //Search works for existing book
        [Fact]
        public void Search_WhenBookExists_ReturnsOk()
        {
            //Act
            var result = _controller.Search("Book1");
            //Assert
            var ok = Assert.IsType<OkObjectResult>(result.Result);

            Assert.NotNull(ok.Value);
        }
        //Borrow works if book is available
        [Fact]
        public void BorrowBook_WhenAvailable_ReturnsOk()
        {
            SetUser("ABC@gmail.com", "Student");

            var result = _controller.BorrowBook(1, "Book1");
            var ok = Assert.IsType<OkObjectResult>(result.Result);

            Assert.Contains("borrowed", ok.Value.ToString(), StringComparison.OrdinalIgnoreCase);
        }
        //Return works if student has book
        [Fact]
        public void ReturnBook_WhenBorrowed_ReturnsOk()
        {
            SetUser("ABC@gmail.com", "Student");

            _controller.BorrowBook(1, "Book1");
            var result = _controller.ReturnBook(1, "Book1");

            var ok = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Contains("returned", ok.Value.ToString(), StringComparison.OrdinalIgnoreCase);
        }
        //If book list is empty
        [Fact]
        public void ViewAllBooks_IfEmpty_ReturnsNotFound()
        {
            _db.Books.RemoveRange(_db.Books);
            _db.SaveChanges();

            var result = _controller.ViewAllBooks();
            var notFound = Assert.IsType<NotFoundObjectResult>(result.Result);

            Assert.Equal("Books not found", notFound.Value);
        }
    }
}
