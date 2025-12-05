using AutoMapper;
using LibraryManagement.Models;
using LibraryManagement.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;

namespace LibraryManagement.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin")]
    [Route("api/Admin")]
    public class AdminController2 : ControllerBase
    {
        private readonly IAdminRepository _adminRepo;
        private readonly IMapper _mapper;
        //Constructor to initialize dependencies
        public AdminController2(IAdminRepository adminRepo,IMapper mapper)
        {
            _adminRepo = adminRepo;
            _mapper = mapper;
        }

        //Find students possessing a particular book
        [HttpGet("GetStudentswithBook/{bookId:int}")]
        [SwaggerOperation(Summary = "Get students who borrowed a specific book", Tags = new[] { "Admin" })]
        public ActionResult GetStudentswithBook(int bookId)
        {
            var students = _adminRepo.GetStudentsWithBook(bookId)
                .Select(s => new
                {
                    s.Id,
                    s.StudentName,
                    s.Year,
                    s.Department,
                    s.Email,
                    s.Phone,
                    BorrowedBooks = s.BorrowedList.Select(b => b.book.BookName).ToList()
                }).ToList();

            if (!students.Any()) return NotFound("No students found for this book");

            return Ok(students);
        }

        //View all students data
        [HttpGet("GetAllStudents")]
        [SwaggerOperation(Summary = "Get all students", Tags = new[] { "Admin" })]
        public ActionResult GetAllStudents()
        {
            var students = _adminRepo.GetAllStudents()
                .Select(s => new
                {
                    s.Id,
                    s.StudentName,
                    s.Year,
                    s.Department,
                    s.Email,
                    s.Phone,
                    BorrowedBooks = s.BorrowedList.Select(b => b.book.BookName).ToList(),
                    date1 = s.BorrowedList.Select(b => b.BorrowedOn),
                    date2 = s.BorrowedList.Select(b => b.OverdueOn)
                }).ToList();

            return Ok(students);
        }

        //Get student details with ID
        [HttpGet("studentbyID/{id:int}")]
        [SwaggerOperation(Summary = "Get student by ID", Tags = new[] { "Admin" })]
        public ActionResult GetStudentByID(int id)
        {
            var student = _adminRepo.GetStudentById(id);
            if (student == null) return NotFound("Student not found");

            var result = new Student
            {
                Id=student.Id,
                StudentName=student.StudentName,
                Year=student.Year,
                Department=student.Department,
                Email=student.Email,
                Phone=student.Phone,
                Password=student.Password
            };

            return Ok(result);
        }
        //Find Student details with name
        [HttpGet("studentbyName")]
        [SwaggerOperation(Summary = "Get student by name", Tags = new[] { "Admin" })]
        public ActionResult GetStudentByName([FromQuery] string name)
        {
            var student = _adminRepo.GetStudentByName(name);
            if (student == null) return NotFound("Student not found");

            return Ok(student);
        }
        //Create new student
        [HttpPost("CreateStudent")]
        [SwaggerOperation(Summary = "Create student profile", Tags = new[] { "Admin" })]
        public ActionResult CreateStudent(Student student)
        {
            if(student == null)
            {
                return BadRequest("Invalid details");
            }
            if (_adminRepo.StudentExists(student.Email, student.Phone))
                return Conflict("Invalid or existing student");

            student.Password = BCrypt.Net.BCrypt.HashPassword(student.Password);
            _adminRepo.AddStudent(student);
            _adminRepo.Save();
            return Ok(student);
        }

        //Update student details 
        [HttpPatch("students/{id}")]
        [SwaggerOperation(Summary = "Update student profile partially", Tags = new[] { "Admin" })]
        public ActionResult UpdateStudent(int id, [FromBody] JsonPatchDocument<Student> patchDoc)
        {
            if(patchDoc == null)
            {
                return BadRequest("Null details");
            }
            var student = _adminRepo.GetStudentById(id);
            if (student == null) return NotFound("Student not found");

            patchDoc.ApplyTo(student, ModelState);
            if (!ModelState.IsValid) return BadRequest("Invalid model state");

            _adminRepo.UpdateStudent(student);
            _adminRepo.Save();
            return Ok(student);
        }

        //Delete a student profile
        [HttpDelete("students/{id:int}")]
        [SwaggerOperation(Summary = "Delete student profile", Tags = new[] { "Admin" })]
        public ActionResult DeleteStudent(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid ID");
            }
            var student = _adminRepo.GetStudentById(id);
            if (student == null) return NotFound("Student not found");

            _adminRepo.DeleteStudent(student);
            _adminRepo.Save();
            return Ok("Student deleted successfully");
        }

        //Add book to library
        [HttpPost("AddBook")]
        [SwaggerOperation(Summary = "Add a new book", Tags = new[] { "Admin" })]
        public ActionResult AddBook(Book book)
        {
            if (book == null || _adminRepo.BookExists(book.BookName))
                return Conflict("Invalid or existing book");

            _adminRepo.AddBook(book);
            _adminRepo.Save();
            return Ok(book);
        }

        //Create new admin profile
        [HttpPost("CreateAdmin")]
        [SwaggerOperation(Summary = "Create admin profile", Tags = new[] { "Admin" })]
        public ActionResult CreateAdmin(Admin admin)
        {
            if (admin == null || _adminRepo.AdminExists(admin.UserName))
                return Conflict("Invalid or existing admin");

            admin.Password = BCrypt.Net.BCrypt.HashPassword(admin.Password);
            _adminRepo.AddAdmin(admin);
            _adminRepo.Save();
            return Ok(admin);
        }

        //View admin list
        [HttpGet("GetAllAdmins")]
        [SwaggerOperation(Summary = "Get all admins", Tags = new[] { "Admin" })]
        public ActionResult GetAllAdmins()
        {
            var admins = _adminRepo.GetAllAdmins();
            return Ok(admins);
        }
    }
}
