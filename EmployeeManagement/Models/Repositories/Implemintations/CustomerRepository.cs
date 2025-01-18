using EmployeeManagement.Models.Domain;
using EmployeeManagement.Models.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EmployeeManagement.Models.Repositories.Implemintations
{
    public class CustomerRepository : ICustomerRepository
    {
        List<Customer> customers;
        public CustomerRepository()
        {
            customers = new List<Customer>()
            {
                new Customer() { Id = 1, Name = "Ahmad", Address = "Amman", 
                                 Email = "ahmad@gmail.com", Phone = 123456789 }
            };
        }

        bool ICustomerRepository.addCustomer(Customer customer)
        {
            customers.Add(customer);
            return true;
        }

        bool ICustomerRepository.deleteCustomer(int id)
        {
            Console.WriteLine("hello from repo");

            Customer customer = customers.Find(x => x.Id == id);
            if (customer != null){
                Console.WriteLine("hello from if");

                customers.Remove(customer);
                return true;
            }
            else
            {
                Console.WriteLine("hello from else");

                return false;
            }
        }

        bool ICustomerRepository.editCustomer(Customer cust)
        {
            Customer customer = customers.Find(x => x.Id == cust.Id);
            if (customer == null)
            {
                return false;
            }
            else
            {
                customer.Name = cust.Name;
                customer.Address = cust.Address;
                customer.Email = cust.Email;
                customer.Phone = cust.Phone;
                customers.Remove(customer);
                customers.Add(customer);
                return true;
            }
        }

        IList<Customer> ICustomerRepository.getAllCutsomers()
        {
            return customers;
        }

        Customer ICustomerRepository.getCustomerById(int id)
        {
            var customer = customers.Find(x => x.Id == id);
            return customer;
        }
    }
}
