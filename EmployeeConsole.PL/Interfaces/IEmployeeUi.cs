using EmployeeConsole.Models;

namespace EmployeeConsole.PL.Interfaces
{
    public interface IEmployeeUi
    {
        void DisplayEmployees();
        void AddEmployee();
        string GenerateEmployeeId();
        string DisplayJobTitles();
        string DisplayDepartmentsForRole(string selectedRole);
        string DisplayLocationsForRoleAndDepartment(string selectedRole, string selectedDepartment);
        void UpdateEmployee();
        void DisplayEmpDetails();
        void DeleteEmployee();
        void EmployeeManager();
        string ValidateEmployeeId();
        string ValidateText(string type);
        DateTime ValidateDate(string type);
        string ValidateEmail();
        string ValidatePhoneNumber();
    }
}
