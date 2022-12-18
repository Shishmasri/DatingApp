using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.Entity;
using Microsoft.AspNetCore.Mvc;
using API.DTO;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseAPIController
    {
        private readonly DataContext _context;
        public AccountController(DataContext context)
        {
            _context = context;

        }
        [HttpPost("register")]
        public async Task<ActionResult<AppUser>> Register(RegisterDtos registerDto){
            if(await UserExists(registerDto.Username)) return BadRequest("User is taken");
            using var hmac=new HMACSHA512();
            var user= new AppUser{
                UserName=registerDto.Username.ToLower(),
                passwordHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                passwordSalt=hmac.Key
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
        private async Task<bool> UserExists(string username){
            return await _context.Users.AnyAsync(x=>x.UserName==username.ToLower());
        }
    }
}