//using AutoMapper;
//using Azure;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.JsonPatch;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Infrastructure;
//using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
//using Microsoft.EntityFrameworkCore;
//using LibraryManagement;
//using LibraryManagement.Models;
//using Swashbuckle.AspNetCore.Annotations;
//using System.Net;
//using System.Security.Claims;
//using Xunit.Sdk;

    //namespace LibraryManagement.Controllers
    //{
    //    [Authorize(Roles = "Student,Admin")]
    //    [Route("api/[controller]")]
    //    [ApiController]

    //    public class StudentController : ControllerBase
    //    {
    //        private readonly LibraryDBContext studDB;
    //        private readonly IMapper mapper;
    //        private APIResponse response;
    //        public StudentController(LibraryDBContext _studDB, IMapper _mapper)
    //        {
    //            studDB = _studDB;
    //            mapper = _mapper;
    //            response = new APIResponse();
    //        }
    //        [HttpGet("ViewYourProfile")]
    //        [ProducesResponseType(StatusCodes.Status200OK)]
    //        [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //        [ProducesResponseType(StatusCodes.Status404NotFound)]
    //        [SwaggerOperation(Summary = "View Your Profile", Tags = new[] { "Student" })]
    //        public ActionResult<APIResponse> ViewProfile()
    //        {
    //            var role = User.FindFirst(ClaimTypes.Role)?.Value;
    //            if (role != "Admin")
    //            {
    //                var userName = User.FindFirst(ClaimTypes.Name)?.Value;
    //                try
    //                {
    //                    var stud = studDB.Students.Include(bb => bb.BorrowedList).ThenInclude(b => b.book).Select(s => new
    //                    {
    //                        s.Id,
    //                        s.StudentName,
    //                        s.Year,
    //                        s.Department,
    //                        s.Email,
    //                        s.Phone,
    //                        BorrowedList = s.BorrowedList.Select(b => $"{b.book.BookName} - {b.OverdueOn:yyyy-MM-dd}")
    //                         .ToList()
    //                    }).FirstOrDefault(s => s.Email == userName);
    //                    if (stud == null)
    //                    {
    //                        response.Status = false;
    //                        response.StatusCode = HttpStatusCode.NotFound;
    //                        return NotFound($"Profile with UserName {userName} not found");
    //                    }
    //                    //var studdata = mapper.Map<StudentDTO>(stud);
    //                    response.data = stud;
    //                    response.Status = true;
    //                    response.StatusCode = HttpStatusCode.OK;
    //                    return Ok(response.data);
    //                }
    //                catch (Exception ex)
    //                {
    //                    response.Error.Add(ex.Message);
    //                    response.Status = false;
    //                    response.StatusCode = HttpStatusCode.InternalServerError;
    //                    return Ok(response.Error);
    //                }
    //            }
//            else
//{
//    return Ok(studDB.Students.Include(bb => bb.BorrowedList).ThenInclude(b => b.book).Select(s => new
//    {
//        s.Id,
//        s.StudentName,
//        s.Year,
//        s.Department,
//        s.Email,
//        s.Phone,
//        BorrowedList = s.BorrowedList.Select(s => s.book.BookName).ToList()
//    }));
//}
//        }
//        [HttpGet("ViewAllBooks")]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [ProducesResponseType(StatusCodes.Status404NotFound)]
//        [SwaggerOperation(Summary = "ViewAllBooks", Tags = new[] { "Student" })]
//        public ActionResult<APIResponse> ViewAllBooks()
//        {
//            var books = studDB.Books.Select(s => new
//            {
//                s.BookId,
//                s.BookName,
//                s.Author,
//                s.AvailableCopies
//            }).ToList();
//            if (books == null || books.Count == 0)
//            {
//                return NotFound("Books not found");
//            }
//            return Ok(books);
//        }
//        [HttpGet("search/{BookName}")]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [ProducesResponseType(StatusCodes.Status404NotFound)]
//        [SwaggerOperation(Summary = "Search-Book", Tags = new[] { "Student" })]
//        public ActionResult<APIResponse> Search(string BookName)
//        {
//            var book = studDB.Books.FirstOrDefault(b => b.BookName.ToLower().Contains(BookName.ToLower()));
//            if (book == null)
//            {
//                return NotFound($"Book with name {BookName} doesn't exist in Library.");
//            }
//            return Ok(book);
//        }
//        [HttpPut("Borrow/{StudentId}")]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [ProducesResponseType(StatusCodes.Status400BadRequest)]
//        [ProducesResponseType(StatusCodes.Status404NotFound)]
//        [ProducesResponseType(StatusCodes.Status403Forbidden)]

//        [SwaggerOperation(Summary = "Borrow Book", Tags = new[] { "Student" })]
//        public ActionResult<APIResponse> BorrowBook(int StudentId, string BookName)
//        {
//            try
//            {
//                if (StudentId <= 0 || BookName == null)
//                {
//                    response.Status = false;
//                    response.StatusCode = HttpStatusCode.BadRequest;
//                    throw new InvalidDataException("Invalid Data");
//                }
//                var stud = studDB.Students.Include(s => s.BorrowedList).FirstOrDefault(s => s.Id == StudentId);
//                var book = studDB.Books.FirstOrDefault(a => a.BookName.ToLower() == BookName.ToLower());
//                if (book == null)
//                {
//                    response.Status = false;
//                    response.StatusCode = HttpStatusCode.NotFound;
//                    return NotFound("Book doesn't exist");
//                }
//                if (stud == null)
//                {
//                    response.Status = false;
//                    response.StatusCode = HttpStatusCode.NotFound;
//                    return NotFound("Student not found");
//                }
//                var count = stud.BorrowedList.Count;
//                if (count >= 3)
//                {
//                    response.Status = false;
//                    response.StatusCode = HttpStatusCode.Forbidden;
//                    return StatusCode(403, "Only 3 books allowed at a time.Please return borrowed books first.");
//                }
//                bool AlreadyOwns = studDB.BorrowedBooks.Any(bb => bb.studentId == StudentId && bb.bookId == book.BookId);
//                if (book.AvailableCopies > 0)
//                {
//                    if (!AlreadyOwns)
//                    {
//                        var bbook = new BorrowedBook()
//                        {
//                            bookId = book.BookId,
//                            studentId = stud.Id,
//                            BorrowedOn = DateTime.Now.ToShortDateString()
//                        };
//                        book.AvailableCopies--;
//                        studDB.BorrowedBooks.Add(bbook);
//                        studDB.SaveChanges();
//                    }
//                    else
//                    {
//                        return Conflict($"You already own a copy of {BookName}");
//                    }
//                }
//                else
//                {
//                    return Ok($"All copies of {BookName} are Borrowed");
//                }


//                var studdata = mapper.Map<StudentDTO>(stud);
//                response.data = studdata;
//                return Ok($"{BookName} book assigned to student {stud.StudentName} with ID {stud.Id}");

//            }
//            catch (Exception ex)
//            {
//                response.Error.Add(ex.InnerException?.Message ?? ex.Message);
//                response.Status = false;
//                response.StatusCode = HttpStatusCode.InternalServerError;
//                return Ok(response.Error);
//            }
//        }
//        [HttpPut("Return/{StudentId}")]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [ProducesResponseType(StatusCodes.Status400BadRequest)]
//        [ProducesResponseType(StatusCodes.Status404NotFound)]
//        [SwaggerOperation(Summary = "Return Book", Tags = new[] { "Student" })]

//        public ActionResult ReturnBook(int StudentId, string BookName)
//        {
//            try
//            {
//                if (StudentId <= 0 || BookName == null)
//                {
//                    response.Status = false;
//                    response.StatusCode = HttpStatusCode.BadRequest;
//                    return BadRequest("Invalid ID or BookName");
//                }
//                var stud = studDB.Students.FirstOrDefault(s => s.Id == StudentId);
//                var book = studDB.Books.FirstOrDefault(a => a.BookName.ToLower() == BookName.ToLower());
//                if (stud == null || book == null)
//                {
//                    response.Status = false;
//                    response.StatusCode = HttpStatusCode.NotFound;
//                    return NotFound("Student or book not found");
//                }
//                var borrowedBook = studDB.BorrowedBooks
//            .FirstOrDefault(bb => bb.studentId == StudentId && bb.bookId == book.BookId);
//                if (borrowedBook != null)
//                {
//                    borrowedBook.book.AvailableCopies++;
//                    studDB.BorrowedBooks.Remove(borrowedBook);
//                    studDB.SaveChanges();
//                }
//                else
//                {
//                    return NotFound("Either book doesn't exist or You never Borrowed it");
//                }

//                return Ok($"{BookName} book returned by {stud.StudentName}");

//            }
//            catch (Exception ex)
//            {
//                response.Error.Add(ex.Message);
//                response.Status = false;
//                response.StatusCode = HttpStatusCode.InternalServerError;
//                return Ok($"Exception occured.\n{response.Error}");
//            }

//        }

//    }
//}
