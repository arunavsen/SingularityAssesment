using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SingularityAssesment.Data;
using SingularityAssesment.Models;
using SingularityAssesment.Models.DTOs;

namespace SingularityAssesment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UserController : ControllerBase
    {
        private readonly SingularityAssesmentContext _context;
        private readonly AppSettings _appSettings;


        public UserController(SingularityAssesmentContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            return await _context.User.ToListAsync();
        }

        [HttpPost("CreateUser")]
        public IActionResult CreateUser([FromBody] RegisterDto model)
        {
            var user = _context.User.SingleOrDefault(u => u.UserName == model.UserName);
            var isUserNameUnique = user == null ? true : false;
            if (!isUserNameUnique)
            {
                return BadRequest(new { message = "UserName is already exists" });
            }

            var u = new User
            {
                UserName = model.UserName,
                Password = model.Password,
                Role = model.Role
            };
            _context.User.Add(u);
            _context.SaveChanges();

            u.Password = "";


            return Ok(u);
        }

        [HttpDelete("RemoveUser/{id}")]
        public async Task<ActionResult<User>> RemoveUserAsync(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }


            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return user;

        }
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticationDto model)
        {
            var user = _context.User.SingleOrDefault(u => u.UserName == model.UserName && u.Password == model.Password);

            //user is not found

            if (user == null)
            {
                return BadRequest(new { message = "Username or Password is incorrect" });
            }

            //if user is found generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            SecurityTokenDescriptor tokenDescriptor;
            if (user.Role !=null)
            {
                tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.Id.ToString()),
                        new Claim(ClaimTypes.Role, user.Role)
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature)
                };
            }
            else
            {
                tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
            }
            return Ok(user);
        }


        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDto model)
        {
            var user = _context.User.SingleOrDefault(u => u.UserName == model.UserName);
            var isUserNameUnique = user == null ? true : false;
            if (!isUserNameUnique)
            {
                return BadRequest(new { message = "UserName is already exists" });
            }

            var newUser = new User
            {
                UserName = model.UserName,
                Password = model.Password,
                Role = model.Role
            };
            _context.User.Add(newUser);
            _context.SaveChanges();

            newUser.Password = "";

            return Ok(newUser);
        }
    }
}