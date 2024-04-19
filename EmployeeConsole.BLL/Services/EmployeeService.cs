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

        public bool employeeIdExists(string empId)
        {
            Dictionary<string, Models.Employee> employees = _employeeJsonOperation.LoadExistingJsonFile("C:\\Users\\lekyasri.c\\source\\repos\\CSharp\\EC1\\EmployeeConsole\\EmployeeConsole.Data\\Employees.Json", "Employees.json");
            if (employees.ContainsKey(empId))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Employee Id already exists");
                Console.ResetColor();
                return false;
            }
            return true;
        }
        public void AddEmployee(Employee emp)
        {
            //Dictionary<string, Employee> employee = _employeeJsonOperation.LoadExistingJsonFile("C:\\Users\\lekyasri.c\\source\\repos\\CSharp\\EC1\\EmployeeConsole\\EmployeeConsole.Data\\Employees.Json", "Employees.json");
            //employee.Add(emp.EmpId, emp);
            _employeeJsonOperation.SaveObjectsToJson(emp, "C:\\Users\\lekyasri.c\\source\\repos\\CSharp\\EC1\\EmployeeConsole\\EmployeeConsole.Data\\Employees.Json", "Employees.Json");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Employee added successfully");
            Console.ResetColor();
        }

        public void UpdateEmployee(Employee emp)
        {
            //Dictionary<string, Employee> employee = _employeeJsonOperation.LoadExistingJsonFile("C:\\Users\\lekyasri.c\\source\\repos\\CSharp\\EC1\\EmployeeConsole\\EmployeeConsole.Data\\Employees.Json", "Employees.json");
            //employee[emp.EmpId] = emp;
            _employeeJsonOperation.SaveObjectsToJson(emp, "C:\\Users\\lekyasri.c\\source\\repos\\CSharp\\EC1\\EmployeeConsole\\EmployeeConsole.Data\\Employees.Json", "Employees.json");
        }
        public void DeleteEmployee(string empId)
        {
            Dictionary<string, Employee> employee = _employeeJsonOperation.LoadExistingJsonFile("C:\\Users\\lekyasri.c\\source\\repos\\CSharp\\EC1\\EmployeeConsole\\EmployeeConsole.Data\\Employees.Json", "Employees.json");
            if (!employee.ContainsKey(empId))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Employee Id does not exist");
                Console.ResetColor();
                return;
            }
            employee.Remove(empId);
           // _employeeJsonOperation.SaveObjectsToJson(employee, "C:\\Users\\lekyasri.c\\source\\repos\\CSharp\\EC1\\EmployeeConsole\\EmployeeConsole.Data\\Employees.Json", "Employees.json");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Employee deleted successfully");
            Console.ResetColor();
        }
        public Dictionary<string, Employee> DisplayEmployees()
        {
            Dictionary<string, Models.Employee> employee = _employeeJsonOperation.LoadExistingJsonFile("C:\\Users\\lekyasri.c\\source\\repos\\CSharp\\EC1\\EmployeeConsole\\EmployeeConsole.Data\\Employees.Json", "Employees.json");

            if (!employee.Any())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No employees to display");
                Console.ResetColor();
            }
            return employee;
        }

        public Employee DisplayEmpDetails(string empId)
        {
            Dictionary<string, Employee> employee = _employeeJsonOperation.LoadExistingJsonFile("C:\\Users\\lekyasri.c\\source\\repos\\CSharp\\EC1\\EmployeeConsole\\EmployeeConsole.Data\\Employees.Json", "Employees.json");

            if (!employee.ContainsKey(empId))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Employee Id does not exist");
                Console.ResetColor();
                return null;
            }
            Employee emp = employee[empId];
            return emp;
        }
        public Employee UpdateEmployee(string empId)
        {
            Dictionary<string, Employee> employee = _employeeJsonOperation.LoadExistingJsonFile("C:\\Users\\lekyasri.c\\source\\repos\\CSharp\\EC1\\EmployeeConsole\\EmployeeConsole.Data\\Employees.Json", "Employees.json");
            if (!employee.ContainsKey(empId))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Employee id does not exists");
                Console.ResetColor();
                return null;
            }
            Employee emp = employee[empId];
            return emp;
        }
    }
}
