using EmployeeManagement.Models.Domain;

namespace EmployeeManagement.Models.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Employee GetById(int id);
    }
}
