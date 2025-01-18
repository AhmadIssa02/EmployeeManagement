using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models.Domain
{
    public class Customer
    {
        //Id,Name,Email,Phone,Address

        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$")]
        public string Email { get; set; }
        [Required]
        public int Phone { get; set; }
        [Required]
        public string Address { get; set; }

    }
}
