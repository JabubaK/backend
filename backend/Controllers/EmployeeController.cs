using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService employeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<List<Employee>>>> Add(string firstName, string lastName, int age, string sex)
        {
            return Ok(await employeeService.Add(firstName, lastName, age, sex));
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<Employee>>>> Get()
        {
            return Ok(await employeeService.Get());
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<Employee>>> Get(int id)
        {
            return Ok(await employeeService.GetEmployee(id));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<Employee>>> Edit(int id,string firstName, string lastName, int age, string sex)
        {
            return Ok(await employeeService.EditEmployee(id, firstName, lastName, age, sex));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<List<Employee>>>> Delete(int id)
        {
            return Ok(await employeeService.Delete(id));
        }
    }
}
