using EmployeeConsole.Models;
using EmployeeConsole.BLL.Interfaces;
using EmployeeConsole.DAL.Interfaces;

namespace EmployeeConsole.BLL.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeJsonOperation _employeeJsonOperation;

        public EmployeeService(IEmployeeJsonOperation employeeJsonOperation)
        {
            _employeeJsonOperation = employeeJsonOperation;
        }

        public bool IsEmployeeIdExists(string employeeId)
        {
            Dictionary<string, Models.Employee> employees = _employeeJsonOperation.LoadExistingJsonFile("C:\\Users\\lekyasri.c\\source\\repos\\CSharp\\EC1\\EmployeeConsole\\EmployeeConsole.Data\\Employees.Json", "Employees.json");
            if (employees.ContainsKey(employeeId))
            {
                return false;
            }
            return true;
        }
        public bool AddEmployee(Employee employee)
        {
            Dictionary<string, Employee> employees = _employeeJsonOperation.LoadExistingJsonFile("C:\\Users\\lekyasri.c\\source\\repos\\CSharp\\EC1\\EmployeeConsole\\EmployeeConsole.Data\\Employees.Json", "Employees.json");
            employees.Add(employee.EmployeeId, employee);
            _employeeJsonOperation.SaveObjectsToJson(employees, "C:\\Users\\lekyasri.c\\source\\repos\\CSharp\\EC1\\EmployeeConsole\\EmployeeConsole.Data\\Employees.Json", "Employees.Json");
            return true;
        }

        public void UpdateEmployee(Employee employee)
        {
            Dictionary<string, Employee> employees = _employeeJsonOperation.LoadExistingJsonFile("C:\\Users\\lekyasri.c\\source\\repos\\CSharp\\EC1\\EmployeeConsole\\EmployeeConsole.Data\\Employees.Json", "Employees.json");
            employees[employee.EmployeeId] = employee;
            _employeeJsonOperation.SaveObjectsToJson(employees, "C:\\Users\\lekyasri.c\\source\\repos\\CSharp\\EC1\\EmployeeConsole\\EmployeeConsole.Data\\Employees.Json", "Employees.json");
        }
        public bool DeleteEmployee(string employeeId)
        {
            Dictionary<string, Employee> employee = _employeeJsonOperation.LoadExistingJsonFile("C:\\Users\\lekyasri.c\\source\\repos\\CSharp\\EC1\\EmployeeConsole\\EmployeeConsole.Data\\Employees.Json", "Employees.json");
            if (!employee.ContainsKey(employeeId))
            {
                return false; 
            }
            employee.Remove(employeeId);
             _employeeJsonOperation.SaveObjectsToJson(employee, "C:\\Users\\lekyasri.c\\source\\repos\\CSharp\\EC1\\EmployeeConsole\\EmployeeConsole.Data\\Employees.Json", "Employees.json");
            return true;
        }
        public Dictionary<string, Employee> DisplayEmployees()
        {
            Dictionary<string, Models.Employee> employee = _employeeJsonOperation.LoadExistingJsonFile("C:\\Users\\lekyasri.c\\source\\repos\\CSharp\\EC1\\EmployeeConsole\\EmployeeConsole.Data\\Employees.Json", "Employees.json");
            return employee;
        }

        public Employee DisplayEmpDetails(string employeeId)
        {
            Dictionary<string, Employee> employee = _employeeJsonOperation.LoadExistingJsonFile("C:\\Users\\lekyasri.c\\source\\repos\\CSharp\\EC1\\EmployeeConsole\\EmployeeConsole.Data\\Employees.Json", "Employees.json");

            if (!employee.ContainsKey(employeeId))
            {
                return null;
            }
            Employee employ = employee[employeeId];
            return employ;
        }
    }
}
