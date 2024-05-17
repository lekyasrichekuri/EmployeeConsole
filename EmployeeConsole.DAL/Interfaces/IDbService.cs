using EmployeeConsole.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeConsole.DAL.Interfaces
{
    public interface IDbService
    {
        public bool AddEmployee(string id, string firstName, string lastName, DateTime? dateOfBirth, string Email, string? Phone, DateTime joinDate, string jobTitle, string department, string location, string? manager, string? project);
        public Dictionary<string, Employee> DisplayEmployees();
        public int UpdateEmployee(Employee employee);
        public Employee DisplayEmployeeDetails(string employeeId);
        public bool DeleteEmployee(string employeeId);
        public List<Role> DisplayRoles();
        public bool AddRole(int Id, string RoleName, string Department, string RoleDescription, string LocationName);
        public bool IsEntityExists<T>(string value, string tableName, string columnName);
        public bool AddDepartmentOrLocation<T>(T entity, string tableName, string columnName);
    }
}
