using EmployeeManagement.Models.Domain;

namespace EmployeeManagement.Models.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Customer getCustomerById(int id);
        IList<Customer> getAllCutsomers();
        bool addCustomer(Customer customer);
        bool editCustomer(Customer cust);
        bool deleteCustomer(int id);
    }
}
