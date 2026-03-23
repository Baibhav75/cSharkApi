using Microsoft.AspNetCore.Mvc;
using MyApiProject.Data;
using MyApiProject.Models;
using System.Linq;

namespace MyApiProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ SIGNUP
        [HttpPost("signup")]
        public IActionResult Signup(User user)
        {
            var existingUser = _context.Users
                .FirstOrDefault(x => x.Email == user.Email);

            if (existingUser != null)
                return BadRequest("User already exists ❌");

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok("User registered successfully ✅");
        }

        // ✅ LOGIN
        [HttpPost("login")]
        public IActionResult Login(User login)
        {
            var user = _context.Users
                .FirstOrDefault(x => x.Email == login.Email && x.Password == login.Password);

            if (user == null)
                return Unauthorized("Invalid credentials ❌");

            return Ok("Login successful 🚀");
        }
    }
}