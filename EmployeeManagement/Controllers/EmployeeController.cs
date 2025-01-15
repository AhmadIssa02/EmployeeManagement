using EmployeeManagement.Models.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public string Index()
        {
            return "Welcome from MVC";
        }
        [HttpGet("{id}")]
        public IActionResult GetEmployeeById(int id)
        {
            var emp = _employeeRepository.GetById(id);
            return Ok(emp);
        }
    }
}
