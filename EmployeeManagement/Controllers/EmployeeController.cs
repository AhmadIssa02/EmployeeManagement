using EmployeeManagement.Models.Domain;
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
        [HttpGet("{id}")]   
        public IActionResult GetEmployeeById(int id)
        {
            var emp = _employeeRepository.GetById(id);
            return Ok(emp);
        }
        [HttpGet("getAll")]
        [Produces("application/xml")]
        public IActionResult GetAll()
        {
            return Ok(_employeeRepository.GetAll());
        }
        [HttpPost]
        public IActionResult AddEmployee(Employee employee)
        {
            bool result = _employeeRepository.add(employee);
            return Ok(result);
        }
        [HttpPut]
        public IActionResult editEmployee(Employee employee) { 
            var result = _employeeRepository.edit(employee);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult deleteEmployee(int id)
        {
            var result = _employeeRepository.delete(id);
            return Ok(result);
        }

    }

}
