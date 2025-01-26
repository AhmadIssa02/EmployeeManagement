using EmployeeManagement.Models.Domain;

namespace EmployeeManagement.Models.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository <Employee> Employees { get; }
        IGenericRepository<Department> Departments { get;}

        Task Save();
    }
}
