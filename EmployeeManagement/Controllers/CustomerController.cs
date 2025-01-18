using EmployeeManagement.Models.Domain;
using EmployeeManagement.Models.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository customerRepository;
        public CustomerController(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }
        [HttpGet("getAll")]
        public IActionResult getAllCustomers()
        {
            return Ok(customerRepository.getAllCutsomers());
        }
        [HttpGet("{id}")]
        public IActionResult getCustomerById(int id)
        {
            Customer customer = customerRepository.getCustomerById(id);
            if(customer == null)
            {
                return NotFound($"Customer with ID {id} not found.");
            }
            else
            {
                return Ok(customer);
            }
        }

        [HttpPut]
        public IActionResult editCustomer(Customer cust)
        {
            return Ok(customerRepository.editCustomer(cust));
        }

        [HttpDelete("{id}")]
        public IActionResult deleteCustomer(int id)
        {
            Console.WriteLine("hello from controller");
            return Ok(customerRepository.deleteCustomer(id));
        }

        [HttpPost]
        public IActionResult addCustomer(Customer cust)
        {
            return Ok(customerRepository.addCustomer(cust));
        }
        

    }
}
