﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeConsole.Models;

namespace EmployeeConsole.DAL.Interfaces
{
    public interface IDbService
    {
        public bool AddEmployee(string id, string firstName, string lastName, string dateOfBirth, string Email, string? Phone, string joinDate, string jobTitle, string department, string location, string? manager, string? project);
        public Dictionary<string, Employee> DisplayEmployees();
        public int UpdateEmployee(Employee employee);
        public Employee DisplayEmployeeDetails(string employeeId);
        public bool DeleteEmployee(string employeeId);
        public List<Role> DisplayRoles();
        public bool AddRole(string roleName, string department, string roleDescription, string locationName);
        public bool IsEntityExists<T>(string value, string tableName, string columnName) where T : class;
        public bool AddDepartmentOrLocation<T>(T entity, string tableName, string columnName) where T : class;
    }
}
