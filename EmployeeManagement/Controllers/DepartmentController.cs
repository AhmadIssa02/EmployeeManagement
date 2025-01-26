using AutoMapper;
using EmployeeManagement.Models.Domain;
using EmployeeManagement.Models.IRepository;
using EmployeeManagement.Models.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    public class DepartmentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DepartmentController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            
        }
        [HttpGet("{id}")]
        //public async Task <IActionResult> GetDepartment(int id)
        //{
        //    var department = await _unitOfWork.Departments.Get()
        //}
        [HttpGet("GetAll")]
        public async Task <IActionResult> GetDepartments()
        {
            var departments = await _unitOfWork.Departments.GetAll();
            var results = _mapper.Map<IList<Department>>(departments);
            return Ok(results);

        }
        //[HttpPost]

    }
}
