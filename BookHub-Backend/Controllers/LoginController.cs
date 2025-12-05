using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Controllers
{
    [Route("api/auth/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class LoginController:ControllerBase
    {
        private LoginResponseDTO response;
        private readonly IConfiguration _configuration;
        private readonly LibraryDBContext _libraryContext;
        //constructor
        public LoginController(IConfiguration configuration, LibraryDBContext libraryContext)
        {
            _configuration = configuration;
            _libraryContext = libraryContext;
            response = new LoginResponseDTO();
        }
        //Login Handling method
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Login(LoginDTO model)
        {
            bool Isvalid;
            var studuser = _libraryContext.Students.FirstOrDefault(s => s.Email == model.Username);
            //student exists
            if (studuser != null)
            {
                response.Username = model.Username;
                response.Role = "Student";
                //verify password
                Isvalid = BCrypt.Net.BCrypt.Verify(model.Password, studuser.Password);
            }
            else
            {
                var admuser = _libraryContext.Admins.FirstOrDefault(s => s.UserName == model.Username);
                //credentials exist in admin table
                if (admuser != null)
                {
                    response.Username = model.Username;
                    response.Role = "Admin";
                    //verify password
                    Isvalid = BCrypt.Net.BCrypt.Verify(model.Password, admuser.Password); 
                    
                }
                else
                {
                    //Details not found in student and admin tables
                    return Unauthorized(new {message= "User not found.Please contact Admin to create Profile."});
                }
            }

            if (!ModelState.IsValid)
            {
                //Token expires or any other issue
                return BadRequest(new { message = "Invalid Username or password" });
            }
            
            if (Isvalid)
            {
                //if entered password is correct
                string issuer = _configuration.GetValue<String>("Issuer");
                string audience = _configuration.GetValue<String>("Audience");
                var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("JWTSecret"));
                var tokenhandler = new JwtSecurityTokenHandler();
                var tokendescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                          //Generated token will contain these fields
                          new Claim(ClaimTypes.Name,model.Username),
                          new Claim(ClaimTypes.Role,response.Role),
                    }),
                    Expires = DateTime.Now.AddHours(4),
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature) //algorithm used
                };
                var token = tokenhandler.CreateToken(tokendescriptor);
                response.token = tokenhandler.WriteToken(token);
                return Ok(response);
            }
            else
            {
                //if username exists but entered password is incorrect
                return Unauthorized(new { message = "Incorrect Password" });
            }
        }
    }
}
