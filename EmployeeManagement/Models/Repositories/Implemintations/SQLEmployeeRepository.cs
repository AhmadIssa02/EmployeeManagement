using EmployeeManagement.Models.Domain;
using EmployeeManagement.Models.Repositories.Interfaces;

namespace EmployeeManagement.Models.Repositories.Implemintations
{
    public class SQLEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employees;

        public SQLEmployeeRepository()
        {
            _employees = new List<Employee>()
                {
                    new Employee() { Id = 1, Name = "Ahmad", Department = "HR", Email = "mary@pragimtech.com" },
                    new Employee() { Id = 2, Name = "Mohammad", Department = "IT", Email = "john@pragimtech.com" },
                    new Employee() { Id = 3, Name = "wasan", Department = "IT", Email = "sam@pragimtech.com" },
                    new Employee() { Id = 4, Name = "dima", Department = "IT", Email = "Ali@pragimtech.com" },
                };

        }
        public Employee GetById(int id)
        {
            var emp = _employees.Find(emp => emp.Id == id);
            return emp;
        }
    }
}
