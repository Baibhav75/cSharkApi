using Microsoft.AspNetCore.Mvc;
using MyApiProject.Models;
using MyApiProject.Services;
using System.Linq;

namespace MyApiProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly MongoService _mongo;

        public AuthController(MongoService mongo)
        {
            _mongo = mongo;
        }

        // ✅ SIGNUP
        [HttpPost("signup")]
        public IActionResult Signup(User user)
        {
            var existingUser = _mongo.GetAll()
                .FirstOrDefault(x => x.Email == user.Email);

            if (existingUser != null)
                return BadRequest("User already exists ❌");

            _mongo.Create(user);

            return Ok("User registered successfully ✅");
        }

        // ✅ LOGIN
        [HttpPost("login")]
        public IActionResult Login(User login)
        {
            var user = _mongo.Get(login.Email, login.Password);

            if (user == null)
                return Unauthorized("Invalid credentials ❌");

            return Ok("Login successful 🚀");
        }

        // ✅ OPTIONAL: CHECK USERS
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            return Ok(_mongo.GetAll());
        }
    }
}