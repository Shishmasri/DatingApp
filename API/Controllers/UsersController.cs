using Microsoft.AspNetCore.Mvc;
using API.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using API.Entity;
namespace API.Controllers
{
    // [ApiController]
    // [Route("api/[controller]")]
    public class UsersController : BaseAPIController
    {
        private readonly DataContext _context;
        public UsersController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task <ActionResult<IEnumerable<AppUser>>> GetUsers(){
                return await _context.Users.ToListAsync();
        }
        //api/users/2
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id){
                return await _context.Users.FindAsync(id);
        }
    }
}