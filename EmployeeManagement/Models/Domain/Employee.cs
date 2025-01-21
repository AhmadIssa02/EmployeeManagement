using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models.Domain
{
    public class Employee
    {
        [Required]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [EmailAddress]
        [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$")]
        public string Email { get; set; }
        public string Department { get; set; }
        public int salary { get; set; }

        public string? address{ get; set; }
    }
}
