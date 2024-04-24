using EmployeeConsole.Models;
namespace EmployeeConsole.BLL.Interfaces
{
    public interface IEmployeeService
    {
        bool IsEmployeeIdExists(string employeeId);
        bool AddEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        bool DeleteEmployee(string employeeId);
        Dictionary<string, Employee> DisplayEmployees();
        Employee DisplayEmpDetails(string employeeId);
    }

}
