using Azure;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class EmployeeService
    {
        private DbsolbegContext context;

        public EmployeeService(DbsolbegContext context)
        {
            this.context = context;
        }

        public async Task<ApiResponse<List<Employee>>> Add(string name, string lastname, int age, string sex)
        {
            var response = new ApiResponse<List<Employee>> ();

            if (age < 18 || age > 100)
            {
                response.Success = false;
                response.Message = "Age must be between 18 and 100.";
                return response;
            }

            if (sex != "M" && sex != "F")
            {
                response.Success = false;
                response.Message = "Sex must be 'M' or 'F'.";
                return response;
            }

            var newEmployee = new Employee
            {
                FirstName = name,
                LastName = lastname,
                Age = age,
                Sex = sex
            };

            context.Employees.Add(newEmployee);
            await context.SaveChangesAsync();

            response.Data = await context.Employees.ToListAsync();

            return response;
        }

        public async Task<ApiResponse<List<Employee>>> Get()
        {
            var response = new ApiResponse<List<Employee>>();
            try
            {
                response.Data = await context.Employees.ToListAsync();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;

        }

        public async Task<ApiResponse<List<Employee>>> Delete(int id)
        {
            var response = new ApiResponse<List<Employee>>();
            try
            {
                var employee = await context.Employees.FirstOrDefaultAsync(e => e.Id == id);
                if (employee == null) throw new Exception("Employee not found");
                context.Employees.Remove(employee);

                await context.SaveChangesAsync();
                response.Data = await context.Employees.ToListAsync();

                response.Message = $"The Employee: {employee.FirstName} {employee.LastName} was deleted.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ApiResponse<List<Employee>>> EditEmployee(int id, string firstName, string lastName, int age, string sex)
        {
            var response = new ApiResponse<List<Employee>>();

            try
            {

                if (age < 18 || age > 100)
                {
                    response.Success = false;
                    response.Message = "Age must be between 18 and 100.";
                    return response;
                }

                if (sex != "M" && sex != "F")
                {
                    response.Success = false;
                    response.Message = "Sex must be 'M' or 'F'.";
                    return response;
                }

                var employee = await context.Employees.FirstOrDefaultAsync(e => e.Id == id);

                if (employee is null) throw new Exception("Employee not found");
                employee.FirstName = firstName;
                employee.LastName = lastName;
                employee.Age = age;
                employee.Sex = sex;

                await context.SaveChangesAsync();

                response.Data = await context.Employees.ToListAsync();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        internal async Task<ApiResponse<Employee>> GetEmployee(int id)
        {
            var response = new ApiResponse<Employee>();
            try
            {
                var employee = await context.Employees.FirstOrDefaultAsync(e => e.Id == id);
                if (employee == null) throw new Exception("Employee not found");
                response.Data = employee;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
