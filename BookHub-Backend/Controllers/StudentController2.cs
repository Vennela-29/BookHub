using AutoMapper;
using LibraryManagement.Models;
using LibraryManagement.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;

namespace LibraryManagement.Controllers
{
    [Authorize(Roles = "Student,Admin")]
    [Route("api/Student/")]
    [ApiController]
    //Extend Controller base to get HttpGet,HttpPost etc
    public class StudentController2 : ControllerBase
    {
        private readonly IStudentRepository _studentRepo;
        private APIResponse response;
        private readonly IMapper _mapper;

        //Constructor to Initialize dependencies
        public StudentController2(IStudentRepository studentRepo,IMapper mapper)
        {
            _studentRepo = studentRepo;
            response = new APIResponse();
            _mapper = mapper;
        }
        //Get your profile
        [HttpGet("ViewYourProfile")]
        [SwaggerOperation(Summary = "View Your Profile", Tags = new[] { "Student" })]
        public ActionResult<APIResponse> ViewProfile()
        {
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            if (role != "Admin")
            {
                var email = User.FindFirst(ClaimTypes.Name)?.Value;
                var student = _studentRepo.GetStudentByEmail(email);

                if (student == null)
                {
                    response.Status = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound($"Profile with email {email} not found");
                }

                response.data = new
                {
                    student.Id,
                    student.StudentName,
                    student.Year,
                    student.Department,
                    student.Email,
                    student.Phone,
                    BorrowedList = student.BorrowedList
                        .Select(b => $"{b.book.BookName} - {b.OverdueOn:yyyy-MM-dd}")
                        .ToList()
                };

                response.Status = true;
                response.StatusCode = HttpStatusCode.OK;
                return Ok(response.data);
            }
            else
            {
                // Admin can see all students
                var students = _studentRepo.GetAllBooks(); // Or add a method to get all students if needed
                return Ok(students);
            }
        }
        //Get all books data in the library
        [HttpGet("ViewAllBooks")]
        [SwaggerOperation(Summary = "View All Books", Tags = new[] { "Student" })]
        public ActionResult<APIResponse> ViewAllBooks()
        {
            var books = _studentRepo.GetAllBooks().Select(b => new
            {
                b.BookId,
                b.BookName,
                b.Author,
                b.AvailableCopies
            }).ToList();

            if (!books.Any())
                return NotFound("Books not found");

            response.data = books;
            response.Status = true;
            response.StatusCode = HttpStatusCode.OK;
            return Ok(response.data);
        }
        //search for existing books
        [HttpGet("search/{BookName}")]
        [SwaggerOperation(Summary = "Search Book", Tags = new[] { "Student" })]
        public ActionResult<APIResponse> Search(string BookName)
        {
            var book = _studentRepo.SearchBook(BookName);
            if (book == null)
                return NotFound($"Book {BookName} doesn't exist in Library.");

            response.data = book;
            response.Status = true;
            response.StatusCode = HttpStatusCode.OK;
            return Ok(response.data);
        }
        //Borrow book
        [HttpPut("Borrow/{StudentId}")]
        [SwaggerOperation(Summary = "Borrow Book", Tags = new[] { "Student" })]
        public ActionResult<APIResponse> BorrowBook(int StudentId, string BookName)
        {
            bool result = _studentRepo.BorrowBook(StudentId, BookName);

            if (!result)
            {
                response.Status = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Error.Add("Unable to borrow book. Check rules or availability.");
                return BadRequest(response.Error);
            }

            response.data = $"Book {BookName} borrowed by student {StudentId}";
            response.Status = true;
            response.StatusCode = HttpStatusCode.OK;
            return Ok(response.data);
        }
        //Return the book you borrowed
        [HttpPut("Return/{StudentId}")]
        [SwaggerOperation(Summary = "Return Book", Tags = new[] { "Student" })]
        public ActionResult<APIResponse> ReturnBook(int StudentId, string BookName)
        {
            bool result = _studentRepo.ReturnBook(StudentId, BookName);

            if (!result)
            {
                response.Status = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Error.Add("Unable to return book. Check if it was borrowed.");
                return BadRequest(response.Error);
            }

            response.data = $"Book {BookName} returned by student {StudentId}";
            response.Status = true;
            response.StatusCode = HttpStatusCode.OK;
            return Ok(response.data);
        }
    }
}
