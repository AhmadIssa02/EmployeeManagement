using AutoMapper;
using EmployeeManagement.Models.Domain;
using EmployeeManagement.Models.DTOs;
using EmployeeManagement.Models.IRepository;
using EmployeeManagement.Models.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<DepartmentController> _logger;
        public DepartmentController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<DepartmentController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetDepartment(int id)
        {
            try
            {
                _logger.LogInformation("Get Department Start");
                var department = await _unitOfWork.Departments.Get(d =>d.id == id);
                var result = _mapper.Map<DepartmentDto>(department);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal server error:" + ex.StackTrace);
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task <IActionResult> GetDepartments()
        {
            try
            {
                _logger.LogInformation("Get Department Start");
                var departments = await _unitOfWork.Departments.GetAll();
                var results = _mapper.Map<IList<DepartmentDto>>(departments);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal server error:" + ex.StackTrace);
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> AddDepartment([FromBody] Department department)
        {
            try
            {
                //if (!ModelState.IsValid)
                //{
                //    return BadRequest(ModelState);

                //}
                await _unitOfWork.Departments.Insert(department);
                await _unitOfWork.Save();
                return Ok(department);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Range")]
        public async Task<IActionResult> AddDepartments([FromBody] List<Department> departments)
        {
            try
            {
                await _unitOfWork.Departments.InsertRange(departments);
                await _unitOfWork.Save();
                return Ok(departments);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateDepartment([FromBody] Department department)
        {

            try
            {

                _unitOfWork.Departments.Update(department);
                await _unitOfWork.Save();
                return Ok(department);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteDepartment(int id)
        {

            try
            {

                await _unitOfWork.Departments.Delete(id);
                await _unitOfWork.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("Range")]
        public async Task<IActionResult> DeleteDepartments(List<Department> departments)
        {

            try
            {

                _unitOfWork.Departments.DeleteRange(departments);
                await _unitOfWork.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
