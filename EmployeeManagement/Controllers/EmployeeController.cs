using AutoMapper;
using EmployeeManagement.Models.Domain;
using EmployeeManagement.Models.DTOs;
using EmployeeManagement.Models.IRepository;
using EmployeeManagement.Models.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<EmployeeController> _logger;
        public EmployeeController(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork, IMapper mapper, ILogger<EmployeeController> logger)
        {
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        [HttpGet("{id}")]   
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var emp = await _unitOfWork.Employees.Get(e => e.Id == id, include: q => q.Include(e => e.dept));
            var result = _mapper.Map<EmployeeDTO>(emp);
  //          Console.WriteLine("hi");
//            _logger.LogTrace("hello");
            return Ok(result);
        }
        [HttpGet("getAll")]
        //[Produces("application/xml")]
        public async Task<IActionResult>GetAll()
        {
            var emps = await _unitOfWork.Employees.GetAll(include: q => q.Include(e => e.dept));
            var result = _mapper.Map<IList<EmployeeDTO>>(emps);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult AddEmployee(Models.DTOs.EmployeeDTO employeeDto)
        {
            bool result = _employeeRepository.add(employeeDto);
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
