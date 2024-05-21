using System.ComponentModel.DataAnnotations;
namespace EmployeeConsole.Models
{
    public class Employee
    {
        public string EmployeeId { get; set; }
        [Required, MaxLength(50)]
        public string FirstName { get; set; }
        [Required, MaxLength(50)]
        public string LastName { get; set; }
        public string? DateOfBirth { get; set; }
        [Required, MaxLength(50)]
        public string Email { get; set; }
        [MaxLength(10)]
        public string? PhoneNumber { get; set; }
        [Required]
        public string JoiningDate { get; set; }
        [Required, MaxLength(50)]
        public string JobTitle { get; set; }
        [Required, MaxLength(50)]
        public string Department { get; set; }
        [Required, MaxLength(50)]
        public string LocationName { get; set; }
        [MaxLength(50)]
        public string? Manager { get; set; }
        [MaxLength(50)]
        public string? ProjectName { get; set; }
    }
}
