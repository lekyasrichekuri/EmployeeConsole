namespace EmployeeConsole.Models
{
    public class Employee
    {
        public string EmployeeId { get; set; }
        public string Name { get; set; }
        public string? DateOfBirth { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string JoiningDate { get; set; }
        public string LocationName { get; set; }
        public string JobTitle { get; set; }
        public string Department { get; set; }
        public string? Manager { get; set; }
        public string? ProjectName { get; set; }
    }
}
