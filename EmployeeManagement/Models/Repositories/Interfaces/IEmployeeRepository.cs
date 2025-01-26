using EmployeeManagement.Models.Domain;
using EmployeeManagement.Models.DTOs;

namespace EmployeeManagement.Models.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        EmployeeDTO GetById(int id);
        IList <EmployeeDTO> GetAll();
        bool add(EmployeeDTO employee);
        bool edit(Employee employee);
        bool delete(int id);
    }
}
