using System.ComponentModel.DataAnnotations;
namespace EmployeeConsole.Models
{
    public class Role
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string RoleName { get; set; }
        [Required, MaxLength(50)]
        public string Department { get; set; }
        public string? Description { get; set; }
        [Required, MaxLength(50)]
        public string LocationName { get; set; }
    }
}
