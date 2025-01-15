using EmployeeManagement.Models.Domain;
using EmployeeManagement.Models.Repositories.Interfaces;

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
    }
}
