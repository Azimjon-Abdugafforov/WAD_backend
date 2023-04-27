using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using WAD.Data;
using WAD.Models;
using WAD.Services.EmploployeeService;

namespace WAD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly DataContext _dbConnection;
        private readonly EmployeeService _employeeService;

        public EmployeeController(DataContext dbConnection, EmployeeService employee)
        {
            _dbConnection = dbConnection;
            _employeeService = employee;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetEmployees()
        {
            var result = await _employeeService.GetAllEmployees();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> InsertEmployee( EmployeeDto model )
        {
            if (_dbConnection.UserDto.Any(x => x.Username == model.Username))
            {
                return BadRequest("Username already exists");
            }
            try
            {
                var employee = new UserDto()
                {
                    Username = model.Username,
                    Password = model.Password,
                    Branch = model.Branch,
                    Role = "USER"
                };
                var data = await _employeeService.InsertEmployee(model);
                await _dbConnection.SaveChangesAsync();
                return Ok(data.ToString());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteEmployee (int Id)
        {
            try
            {
                var result = await _employeeService.DeleteEmployee(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }
        [HttpGet("{id}", Name = "GetEmployeeById")]
        public async Task<ActionResult<List<UserDto>>> GetEmployeeById(int Id)
        {
            return Ok(await _employeeService.GetEmployeesById(Id));
        }

        [HttpPut]
        public async Task<ActionResult<List<UserDto>>> UpdateEmployee(int Id, UserDto model)
        {
            
            return Ok(_employeeService.UpdateEmployee(Id, model));
        }


    }
}
