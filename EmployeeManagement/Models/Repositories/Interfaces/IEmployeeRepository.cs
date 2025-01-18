using EmployeeManagement.Models.Domain;

namespace EmployeeManagement.Models.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Employee GetById(int id);
        IList <Employee> GetAll();
        bool add(Employee employee);
        bool edit(Employee employee);
        bool delete(int id);
    }
}
