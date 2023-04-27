using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WAD.Data;
using WAD.Models;

namespace WAD.Services.EmploployeeService
{
    public class EmployeeService : IEmployeeInterface
    {

        private readonly DataContext _dbConnection;


        public EmployeeService(DataContext dbConnection)
        {
            _dbConnection = dbConnection;
        }


        public async Task<ActionResult<string>> DeleteEmployee(int employeeId)
        {
            var user = await _dbConnection.UserDto.FindAsync(employeeId);
            if (user == null)
            {
                return "User not found";
            }
            _dbConnection.UserDto.Remove(user);
            await _dbConnection.SaveChangesAsync();
            return "Deleted";
        }


        public async Task<ActionResult<List<UserDto>>> GetAllEmployees()
        {
            return await _dbConnection.UserDto.ToListAsync();  
        }

        public async Task<ActionResult<List<UserDto>>> GetEmployeesById(int Id)
        {

            var user = await _dbConnection.UserDto.FindAsync(Id);
            
            return new List<UserDto>{ user};
        }


        public async Task<ActionResult<string>> InsertEmployee(EmployeeDto model)
        {
            try
            {
                if(model.Username == null || model.Password == null || model.Branch == null)
                {
                    return "Please fill all the fields";
                }


                var employee = new UserDto()
                {
                    Username = model.Username,
                    Password = model.Password,
                    Branch = model.Branch,
                    Role = "USER"
                };
                var data = await _dbConnection.UserDto.AddAsync(employee);
                await _dbConnection.SaveChangesAsync();
                return data.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public object UpdateEmployee(int Id, UserDto model)
        {
            var foundGuy = _dbConnection.UserDto.FindAsync(Id);

            if(foundGuy == null)
            {
                return "User not found";
            }  
            foundGuy.Result.Username = model.Username;
            foundGuy.Result.Password = model.Password;
            foundGuy.Result.Branch = model.Branch;
            _dbConnection.SaveChanges();
            return "Updated";
        }

        object IEmployeeInterface.GetAllEmployees()
        {
            throw new NotImplementedException();
        }

        Task<ActionResult<List<UserDto>>> IEmployeeInterface.UpdateEmployee(int Id, UserDto model)
        {
            throw new NotImplementedException();
        }
    }
}
