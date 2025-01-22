namespace EmployeeManagement.Models.DTOs
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int salary { get; set; }
        public string? address { get; set; }
        public int DeptId { get; set; }
        public String DeptName { get; set; }
    }
}
