using EmployeeManagement.Data;
using EmployeeManagement.Models.Domain;
using EmployeeManagement.Models.DTOs;
using EmployeeManagement.Models.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Models.Repositories.Implemintations
{
    public class SQLEmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _dbContext;

        public SQLEmployeeRepository( AppDbContext dbContext)
        {
            _dbContext = dbContext;

        }
        public IList <Employee> GetAll()
        {
            return _dbContext.Employees.ToList();
        }
        public EmployeeDTO GetById(int id)
        {
            var employee = _dbContext.Employees.Include(emp => emp.dept).FirstOrDefault(emp => emp.Id == id);
            var employeeDto = empToDto(employee);
            return employeeDto;
        }

        public bool add(EmployeeDTO employee)
        {   
            var emp = DtoToEmployee(employee);

            _dbContext.Employees.Add(emp);
            _dbContext.SaveChanges();
            return true;
        }

        public bool edit(Employee employee)
        {
            var emp = _dbContext.Employees.Attach(employee);
            emp.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _dbContext.SaveChanges();
            return true;
        }

        public bool delete(int id)
        {
            var emp= _dbContext.Employees.Find(id);
            if(emp != null) {
                _dbContext.Employees.Remove(emp);
                _dbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    private EmployeeDTO empToDto( Employee employee)
        {
            var employeeDto = new EmployeeDTO()
            {
                Id = employee.Id,
                Name = employee.Name,
                address = employee.address,
                Email = employee.Email,
                salary = employee.salary,
                DeptName = employee.dept.name,
            };
            return employeeDto;

        }
        private Employee DtoToEmployee(EmployeeDTO empDto)
        {
            var employee = new Employee
            {
                Id = empDto.Id,
                Name = empDto.Name,
                address = empDto.address,
                Email = empDto.Email,
                salary = empDto.salary,
                DeptId = empDto.DeptId,

            };
            return employee;
        }
    }
}
