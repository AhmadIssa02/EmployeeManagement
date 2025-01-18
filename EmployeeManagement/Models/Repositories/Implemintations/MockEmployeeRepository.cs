using EmployeeManagement.Models.Domain;
using EmployeeManagement.Models.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EmployeeManagement.Models.Repositories.Implemintations
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employees;

        public MockEmployeeRepository()
        {
            _employees = new List<Employee>()
                {
                    new Employee() { Id = 1, Name = "Mary", Department = "HR", Email = "mary@pragimtech.com" },
                    new Employee() { Id = 2, Name = "John", Department = "IT", Email = "john@pragimtech.com" },
                    new Employee() { Id = 3, Name = "Sam", Department = "IT", Email = "sam@pragimtech.com" },
                    new Employee() { Id = 4, Name = "Ali", Department = "IT", Email = "Ali@pragimtech.com" },
                };

        }
        public Employee GetById(int id)
        {
            var emp = _employees.Find(emp => emp.Id == id);
            return emp;
        }

        public IList <Employee> GetAll()
        {
            return _employees;
        }
        public bool add(Employee emp)
        {
            _employees.Add(emp);
            return true;        
        }

        public bool edit (Employee employee)
        {
            var emp = _employees.Find(emp => emp.Id == employee.Id);
            emp.Name = employee.Name;
            emp.Email = employee.Email;
            emp.Department = employee.Department;
            _employees.Remove(emp);
            _employees.Add(emp);
            return true;
        }

        public bool delete(int id )
        {
            var empToRemove = _employees.FirstOrDefault(emp => emp.Id == id);
            _employees.Remove(empToRemove);
            return true;
            
         
        }


    }
}
