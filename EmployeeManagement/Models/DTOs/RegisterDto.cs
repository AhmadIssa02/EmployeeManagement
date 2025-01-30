using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace EmployeeManagement.Models.DTOs
{
    public class RegisterDto : LoginUserDto
    {
        [Required]
        [MaxLength(10)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(10)]
        public string LastName{ get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password",ErrorMessage= "Password and confirmation password donot match.")]
        public string ConfirmPassword
        { get; set; }
        public ICollection<string>Roles { get; set; }
    }
}
