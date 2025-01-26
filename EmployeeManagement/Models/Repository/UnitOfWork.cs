using EmployeeManagement.Data;
using EmployeeManagement.Models.Domain;
using EmployeeManagement.Models.IRepository;

namespace EmployeeManagement.Models.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        private IGenericRepository<Employee> _Employees;
        private IGenericRepository<Department> _Departments;

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IGenericRepository<Employee> Employees => _Employees ??= new GenericRepository<Employee> (_dbContext);
        public IGenericRepository<Department> Departments => _Departments ??= new GenericRepository<Department>(_dbContext);
        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);  
        }

        public async Task Save()
        {
            await _dbContext?.SaveChangesAsync();
        }
    }
}
