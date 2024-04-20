using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NextEcommerceWebApi.Data;
using NextEcommerceWebApi.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace NextEcommerceWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }    

        // Register User
        [HttpPost("register")]
        public async Task<IActionResult> userRegister([FromBody] User user)
        {
            try
            {
                var dbUser = _context.Users.Where(u => u.Email == user.Email).FirstOrDefault();

                if (dbUser != null)
                {
                    return BadRequest("User is already exists");
                }

                if (string.IsNullOrEmpty(user.UserName))
                {
                    return BadRequest("User Name is required");
                }

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                //bunu sonradan ekledim
                var newdbUser = _context.Users.Where(u => u.Id == user.Id).Select(u => new
                {
                    u.Id,
                    u.UserName,
                    u.UserRole,
                    u.Email
                }).FirstOrDefault();

                return Ok(newdbUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // Login User 
        [HttpPost("login")]
        public async Task<IActionResult> userLogin([FromBody] User user)
        {
            try
            {
                var dbUser = _context.Users.Where(u => u.Email == user.Email && u.Password == user.Password).Select(u => new
                {
                    u.Id,
                    u.UserName,
                    u.UserRole,
                    u.Email
                }).FirstOrDefault();

                if (dbUser == null)
                {
                    return BadRequest("Email or password is wrong");
                }

                var token = CreateToken(user);

                var refreshToken = GenerateRefreshToken();

                SetRefreshToken(refreshToken, user);


                return Ok(new { dbUser, token, refreshToken });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // change password
        [Authorize]
        [HttpPost("change-password")]
        public async Task<ActionResult<User>> ChangePassword([FromBody] ChangePassword model)
        {
            try
            {
                var userDb = await _context.Users.FindAsync(model.Id);

                if (userDb == null)
                {
                    return NotFound("User does not exists!");
                }

                 var currentPass = await _context.Users.Where(u => userDb.Password == model.CurrentPassword).FirstOrDefaultAsync();

                if (currentPass == null)
                {
                    return NotFound("Password is wrong");
                }

                if (string.Compare(model.NewPassword, model.ConfirmNewPassword) != 0)
                {
                    return NotFound("New Password and Confirm Password does not match !");
                }

                userDb.Password = model.NewPassword;

                await _context.SaveChangesAsync();

                return Ok("Password successfully changed.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // generate RefreshToken
        private RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(30),
                Created = DateTime.Now
            };

            return refreshToken;
        }

        // set refresh token
        private void SetRefreshToken(RefreshToken newRefreshToken,User user)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires
            };

            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

            user.RefreshToken = newRefreshToken.Token;
            user.TokenCreated = newRefreshToken.Created;
            user.TokenExpires = newRefreshToken.Expires;
        }

        // create Token
        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));


            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(3),
            signingCredentials: creds);


            var jwt = new JwtSecurityTokenHandler().WriteToken(token);


            return jwt;
        }

        // get User
        [Authorize]
        [HttpGet("get-user")]
        public async Task<ActionResult<User>>GetUserById(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                    return NotFound($"Not Found User with Id {id}");

                var newdbUser = _context.Users.Where(u => u.Id == user.Id).Select(u => new
                {
                    u.Id,
                    u.UserName,
                    u.UserRole,
                    u.Email
                }).FirstOrDefault();

                return Ok(newdbUser);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
