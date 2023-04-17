using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using WAD.Data;
using WAD.Models;

namespace WAD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly DataContext _dbConnection;

        public EmployeeController(DataContext dbConnection)
        {
            _dbConnection = dbConnection;
        }

        [HttpGet, Authorize]
        public async Task<ActionResult<List<UserDto>>> GetEmployees()
        {
            return Ok(await _dbConnection.UserDto.ToListAsync());
        }
    }
}
