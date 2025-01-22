using EmployeeManagement.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.ExtensionMethod
{
    public static class ModelBuilderExtension
    {

        public static void seedEmployee(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                 new Employee
                 {
                     Id = 1,
                     Name = "John",
                     Department = "HR",
                     Email = "ahmad",
                     address = "1234 Elm St",
                     salary = 10000,
                     DeptId = 2
                 },
                new Employee
                {
                    Id = 2,
                    Name = "John",
                    Department = "HR",
                    Email = "ahmad",
                    address = "1234 Elm St",
                    salary = 10000,
                    DeptId = 2
                },
                new Employee
                {
                    Id = 3,
                    Name = "John",
                    Department = "HR",
                    Email = "ahmad",
                    address = "1234 Elm St",
                    salary = 10000,
                    DeptId = 3
                }
            );
        }
        public static void seedDepartment(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>().HasData(
                 new Department
                 {
                     id = 1,
                     name = "Finance"
                 },
                new Department
                {
                    id = 2,
                    name = "IT"
                },
                  new Department
                  {
                      id = 3,
                      name = "HR"
                  }
            );
        }
        }
}
