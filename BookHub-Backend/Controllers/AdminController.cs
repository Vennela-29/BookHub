//using AutoMapper;
//using Azure;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.JsonPatch;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using LibraryManagement.Models;
//using Swashbuckle.AspNetCore.Annotations;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Text;
//using System.Threading.Tasks;

//namespace LibraryManagement.Controllers
//{
//    [ApiController]
//    [Authorize(Roles = "Admin")]
//    [Route("api/Admin")]
//    public class AdminController : ControllerBase
//    {
//        private readonly LibraryDBContext studDB;
//        private readonly IMapper mapper;
//        private APIResponse response;
//        public AdminController(LibraryDBContext dbContext, IMapper _mapper)
//        {
//            studDB = dbContext;
//            response = new APIResponse();
//            mapper = _mapper;
//        }
//        [HttpGet]
//        [Route("GetStudentswithBook/{BookName}")]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [ProducesResponseType(StatusCodes.Status404NotFound)]
//        public ActionResult<IEnumerable<APIResponse>> GetStudentswithBook(string BookName)
//        {
//            var book = studDB.Books.Include(a => a.BorrowedList).FirstOrDefault(b => b.BookName.ToLower().Contains(BookName.ToLower()));
//            if (book == null)
//            {
//                return NotFound($"Book {BookName} not found");
//            }
//            var studlist = studDB.Students.Include(b => b.BorrowedList).ThenInclude(bb => bb.book).Where(s => s.BorrowedList.Any(bb => bb.bookId == book.BookId)).Select(s => new
//            {
//                s.Id,
//                s.StudentName,
//                s.Year,
//                s.Department,
//                s.Email,
//                s.Phone,
//                Bb = s.BorrowedList.Select(b => b.book.BookName).ToList()
//            }).ToList();
//            return Ok(studlist);
//        }
//        [HttpGet]
//        [Route("GetAllStudents")]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [ProducesResponseType(StatusCodes.Status400BadRequest)]
//        //[SwaggerOperation(Summary = "Get All Students", Tags = new[] { "Admin" })]
//        public ActionResult<IEnumerable<APIResponse>> GetAllStudents()
//        {
//            try
//            {
//var stud = studDB.Students.Include(a => a.BorrowedList).ThenInclude(b => b.book).Select(s => new
//{
//    s.Id,
//    s.StudentName,
//    s.Department,
//    s.Year,
//    s.Phone,
//    s.Email,
//    BorrowedList = s.BorrowedList.Select(b => b.book.BookName).ToList(),
//date1 = s.BorrowedList.Select(b => b.BorrowedOn),
//    date2 = s.BorrowedList.Select(b => b.OverdueOn)
//}).ToList();
//                response.data = stud;
//                response.Status = true;
//                response.StatusCode = HttpStatusCode.OK;
//                return Ok(response.data);
//            }
//            catch (Exception ex)
//            {
//                response.Error.Add(ex.Message);
//                response.Status = false;
//                response.StatusCode = HttpStatusCode.InternalServerError;
//                return BadRequest(response.Error);
//            }

//        }
//        [HttpGet]
//        [Route("GetAllAdmins")]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        public ActionResult<IEnumerable<APIResponse>> GetAllAdmins()
//        {
//            try
//            {
//                var admin = studDB.Admins.Select(s => new
//                {
//                    s.AdminId,
//                    s.UserName,
//                    s.Designation
//                }).ToList();
//                response.data = admin;//mapper.Map<List<StudentDTO>>(stud);
//                response.Status = true;
//                response.StatusCode = HttpStatusCode.OK;
//                return Ok(response.data);
//            }
//            catch (Exception ex)
//            {
//                response.Error.Add(ex.Message);
//                response.Status = false;
//                response.StatusCode = HttpStatusCode.InternalServerError;
//                return Ok(response.Error);
//            }

//        }
//        [HttpPost("CreateStudent")]
//        [SwaggerOperation(Summary = "Create Student Profile", Tags = new[] { "Admin" })]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [ProducesResponseType(StatusCodes.Status400BadRequest)]
//        [ProducesResponseType(StatusCodes.Status409Conflict)]
//        public ActionResult<APIResponse> CreateStudent(Student student)
//        {
//            try
//            {
//                if (student == null)
//                {
//                    response.Status = false;
//                    response.StatusCode = HttpStatusCode.BadRequest;
//                    return BadRequest("Invalid student details to Create profile");
//                }
//                bool exists = studDB.Students.Any(s => s.Email == student.Email || s.Phone == student.Phone);
//                if (exists)
//                {
//                    response.Status = false;
//                    response.StatusCode = HttpStatusCode.Conflict;
//                    response.Error.Add("Email or Phone already exists");
//                    return Conflict(response);
//                }
//                student.Password = BCrypt.Net.BCrypt.HashPassword(student.Password);
//                studDB.Students.Add(student);
//                studDB.SaveChanges();
//                int newid = student.Id;
//                var studDTO = mapper.Map<StudentDTO>(student);
//                studDTO.Id = newid;
//                response.data = studDTO;
//                response.StatusCode = HttpStatusCode.OK;
//                response.Status = true;
//                return Ok(response.data);
//            }
//            catch (Exception ex)
//            {
//                response.Error.Add(ex.Message);
//                response.Status = false;
//                response.StatusCode = HttpStatusCode.InternalServerError;
//                return BadRequest($"Exception occured - {response.Error}");
//            }

//        }
//        [HttpPost("AddBook")]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [ProducesResponseType(StatusCodes.Status400BadRequest)]
//        [ProducesResponseType(StatusCodes.Status409Conflict)]
//        public ActionResult<APIResponse> AddBook(Book book)
//        {
//            try
//            {
//                if (book == null)
//                {
//                    return BadRequest("Empty Book data");
//                }
//                bool exists = studDB.Books.Any(b => b.BookName == book.BookName);
//                if (exists)
//                {
//                    response.Status = false;
//                    response.StatusCode = HttpStatusCode.Conflict;
//                    response.Error.Add("Book already exists");
//                    return Conflict(response);
//                }
//                studDB.Books.Add(book);
//                studDB.SaveChanges();
//                response.data = book;
//                response.StatusCode = HttpStatusCode.OK;
//                response.Status = true;
//                return Ok(response.data);
//            }
//            catch (Exception ex)
//            {
//                response.Error.Add(ex.Message);
//                response.Status = false;
//                response.StatusCode = HttpStatusCode.InternalServerError;
//                return BadRequest($"Exception occured - {response.Error}");
//            }
//        }

//        [HttpPost("CreateAdmin")]
//        [SwaggerOperation(Summary = "Create Student Profile", Tags = new[] { "Admin" })]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [ProducesResponseType(StatusCodes.Status400BadRequest)]
//        [ProducesResponseType(StatusCodes.Status409Conflict)]
//        public ActionResult<APIResponse> CreateAdmin(Admin admin)
//        {
//            try
//            {
//                if (admin == null)
//                {
//                    response.Status = false;
//                    response.StatusCode = HttpStatusCode.BadRequest;
//                    return BadRequest("Invalid details to Create  Admin profile");
//                }
//                bool exists = studDB.Admins.Any(a => a.UserName == admin.UserName);
//                if (exists)
//                {
//                    response.Status = false;
//                    response.StatusCode = HttpStatusCode.Conflict;
//                    response.Error.Add("Admin Username already exists");
//                    return Conflict(response);
//                }
//                admin.Password = BCrypt.Net.BCrypt.HashPassword(admin.Password);
//                //};
//                studDB.Admins.Add(admin);
//                studDB.SaveChanges();
//                int newid = admin.AdminId;
//                response.data = admin;
//                response.StatusCode = HttpStatusCode.OK;
//                response.Status = true;
//                return Ok(response.data);
//            }
//            catch (Exception ex)
//            {
//                response.Error.Add(ex.Message);
//                response.Status = false;
//                response.StatusCode = HttpStatusCode.InternalServerError;
//                return BadRequest($"Exception occured - {response.Error}");
//            }

//        }
//        [HttpPatch("students/{id}")]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [ProducesResponseType(StatusCodes.Status400BadRequest)]
//        [ProducesResponseType(StatusCodes.Status404NotFound)]
//        public ActionResult<APIResponse> UpdateStudent(int id, [FromBody] JsonPatchDocument<Student> patchdoc)
//        {
//            try
//            {
//                if (patchdoc == null)
//                {
//                    response.Status = false;
//                    response.StatusCode = HttpStatusCode.BadRequest;
//                    return BadRequest("Invalid Update details");
//                }
//                var stud = studDB.Students.FirstOrDefault(s => s.Id == id);
//                if (stud == null)
//                {
//                    response.Status = false;
//                    response.StatusCode = HttpStatusCode.NotFound;
//                    return NotFound("Student not Found");
//                }
//                patchdoc.ApplyTo(stud);
//                if (!ModelState.IsValid)
//                {
//                    return BadRequest("Invalid ModelState");
//                }
//                studDB.SaveChanges();
//                response.data = stud;
//                response.Status = true;
//                response.StatusCode = HttpStatusCode.OK;
//                return Ok(response.data);

//            }
//            catch (Exception ex)
//            {
//                response.Error.Add(ex.Message);
//                response.Status = false;
//                response.StatusCode = HttpStatusCode.InternalServerError;
//                return Ok(response.Error);
//            }

//        }
//        [HttpGet("studentbyName")]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [ProducesResponseType(StatusCodes.Status400BadRequest)]
//        [ProducesResponseType(StatusCodes.Status404NotFound)]
//        //[SwaggerOperation(Summary = "Get Student By Name", Tags = new[] { "Admin" })]
//        public ActionResult<Student> GetStudentByname(string name)
//        {
//            try
//            {
//                if (name == null)
//                {
//                    response.Status = false;
//                    response.StatusCode = HttpStatusCode.BadRequest;
//                    return BadRequest("Invalid name");
//                }
//                var stud = studDB.Students.FirstOrDefault(s => s.StudentName == name);
//                if (stud == null)
//                {
//                    response.Status = false;
//                    response.StatusCode = HttpStatusCode.NotFound;
//                    return NotFound("Student not found");
//                }
//                var studDTO = mapper.Map<StudentDTO>(stud);
//                response.data = studDTO;
//                response.Status = true;
//                response.StatusCode = HttpStatusCode.OK;
//                return Ok(response.data);
//            }
//            catch (Exception ex)
//            {
//                response.Error.Add(ex.Message);
//                response.Status = false;
//                response.StatusCode = HttpStatusCode.InternalServerError;
//                return Ok(response.Error);
//            }
//        }
//        [HttpDelete("students/{id:int}")]
//        //[Route("GetAllStudents")]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [ProducesResponseType(StatusCodes.Status400BadRequest)]
//        [ProducesResponseType(StatusCodes.Status404NotFound)]
//        //[SwaggerOperation(Summary = "Delete Profile", Tags = new[] { "Admin" })]
//        public ActionResult<bool> DeleteProfile(int id)
//        {
//            try
//            {
//                if (id <= 0)
//                {
//                    response.Status = false;
//                    response.StatusCode = HttpStatusCode.BadRequest;
//                    return BadRequest("Invalid ID");
//                }
//                var stud = studDB.Students.FirstOrDefault(s => s.Id == id);
//                if (stud == null)
//                {
//                    response.Status = false;
//                    response.StatusCode = HttpStatusCode.NotFound;
//                    return NotFound("Profile not found");
//                }
//                studDB.Students.Remove(stud);
//                stud.BorrowedList.Clear();
//                studDB.SaveChanges();
//                response.Status = true;
//                response.StatusCode = HttpStatusCode.OK;
//                return Ok("Profile deleted successfully!!");
//            }
//            catch (Exception ex)
//            {
//                response.Error.Add(ex.Message);
//                response.Status = false;
//                response.StatusCode = HttpStatusCode.InternalServerError;
//                return Ok(ex.Message);
//            }
//        }

//        [HttpGet("studentbyID/{id:int}")]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [ProducesResponseType(StatusCodes.Status400BadRequest)]
//        [ProducesResponseType(StatusCodes.Status404NotFound)]
//        //[SwaggerOperation(Summary = "Get Student By Name", Tags = new[] { "Admin" })]
//        public ActionResult<Student> GetStudentByID(int id)
//        {
//            try
//            {
//                if (id <= 0)
//                {
//                    response.Status = false;
//                    response.StatusCode = HttpStatusCode.BadRequest;
//                    return BadRequest("Invalid ID");
//                }
//                var stud = studDB.Students.FirstOrDefault(s => s.Id == id);
//                if (stud == null)
//                {
//                    response.Status = false;
//                    response.StatusCode = HttpStatusCode.NotFound;
//                    return NotFound("Student not found");
//                }
//                //var studDTO = mapper.Map<StudentDTO>(stud);
//                response.data = stud;
//                response.Status = true;
//                response.StatusCode = HttpStatusCode.OK;
//                return Ok(response.data);
//            }
//            catch (Exception ex)
//            {
//                response.Error.Add(ex.Message);
//                response.Status = false;
//                response.StatusCode = HttpStatusCode.InternalServerError;
//                return Ok(response.Error);
//            }
//        }
//    }
//}
