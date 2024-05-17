using EmployeeConsole.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeConsole.BLL.Interfaces
{
    public interface IEmployeeService
    {
        public void AddEmployee(string id, string firstName, string lastName, DateTime? dateOfBirth, string Email, string? Phone, DateTime joinDate, string jobTitle, string department, string location, string? manager, string? project);
        public Dictionary<string, Employee> DisplayEmployees();
        public int UpdateEmployeeDetails(Employee employee);
        public Employee DisplayEmpDetails(string employeeId);
        public bool DeleteEmployee(string employeeId);
    }
}
