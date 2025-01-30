using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models.DTOs
{
    public class LoginUserDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
