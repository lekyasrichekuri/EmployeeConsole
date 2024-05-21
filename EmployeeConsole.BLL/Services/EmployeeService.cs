using EmployeeConsole.BLL.Interfaces;
using EmployeeConsole.DAL.Interfaces;
using EmployeeConsole.Models;

namespace EmployeeConsole.BLL.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IDbService _dbService;
        public EmployeeService(IDbService dbService)
        {
            _dbService = dbService;
        }

        public void AddEmployee(string id, string firstName, string lastName, string? dateOfBirth, string Email, string? Phone, string joinDate, string jobTitle, string department, string location, string? manager, string? project)
        {
            if (_dbService.AddEmployee(id, firstName, lastName, dateOfBirth, Email, Phone, joinDate, jobTitle, department, location, manager, project))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Employee added Successfully");
                Console.ResetColor();
            }
        }
        public Dictionary<string, Employee> DisplayEmployees()
        {
            return _dbService.DisplayEmployees();
        }

        public int UpdateEmployeeDetails(Employee employee)
        {
            return _dbService.UpdateEmployee(employee);
        }

        public Employee DisplayEmpDetails(string employeeId)
        {
            return _dbService.DisplayEmployeeDetails(employeeId);
        }

        public bool DeleteEmployee(string employeeId)
        {
            return _dbService.DeleteEmployee(employeeId);
        }
    }
}
