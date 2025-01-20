using EmployeeManagement.Data;
using EmployeeManagement.Models.Domain;
using EmployeeManagement.Models.Repositories.Interfaces;

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
        public Employee GetById(int id)
        {
            return _dbContext.Employees.Find(id);
        }

        public bool add(Employee employee)
        {
            _dbContext.Employees.Add(employee);
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
    }
}
