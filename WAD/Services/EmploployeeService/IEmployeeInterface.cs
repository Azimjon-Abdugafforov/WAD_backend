using Microsoft.AspNetCore.Mvc;
using WAD.Models;

namespace WAD.Services.EmploployeeService
{
    public interface IEmployeeInterface
    {
        public  object GetAllEmployees();

        public Task<ActionResult<List<UserDto>>> GetEmployeesById(int employeeId);
        public Task<ActionResult<string>> InsertEmployee(EmployeeDto employee);
        public Task<ActionResult<string>> DeleteEmployee(int employeeId);
        public Task<ActionResult<List<UserDto>>> UpdateEmployee(int Id, UserDto model);
    }
}
