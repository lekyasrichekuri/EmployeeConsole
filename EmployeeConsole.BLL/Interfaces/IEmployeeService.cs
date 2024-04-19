using EmployeeConsole.Models;
namespace EmployeeConsole.BLL.Interfaces
{
    public interface IEmployeeService
    {
        bool employeeIdExists(string empId);
        void AddEmployee(Employee emp);
        void UpdateEmployee(Employee emp);
        void DeleteEmployee(string empid);
        Dictionary<string, Employee> DisplayEmployees();
        Employee DisplayEmpDetails(string empid);
        Employee UpdateEmployee(string empid);
    }

}
