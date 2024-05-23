using System;
using System.Collections.Generic;
using System.Linq;
using EmployeeConsole.DAL.Interfaces;
using EmployeeConsole.Models;

namespace EmployeeConsole.DAL.Services
{
    public class DbService : IDbService
    {
        private readonly LekyaEfContext _context;
        public DbService(LekyaEfContext context)
        {
            _context = context;
        }

        public bool AddEmployee(string id, string firstName, string lastName, string? dateOfBirth, string email, string? phone, string joinDate, string jobTitle, string department, string location, string? manager, string? project)
        {
            try
            {
                var newEmployee = new Employee
                {
                    EmployeeId = id,
                    FirstName = firstName,
                    LastName = lastName,
                    DateOfBirth = dateOfBirth,
                    Email = email,
                    PhoneNumber = phone,
                    JoiningDate = joinDate,
                    JobTitle = jobTitle,
                    Department = department,
                    LocationName = location,
                    Manager = manager,
                    ProjectName = project
                };

                _context.Employees.Add(newEmployee);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while adding an employee: {e.Message}");
                return false;
            }
        }

        public Dictionary<string, Employee> DisplayEmployees()
        {
            var employees = new Dictionary<string, Employee>();
            try
            {
                var employeesFromDb = _context.Employees.ToList();
                foreach (var employee in employeesFromDb)
                {
                    employees.Add(employee.EmployeeId, employee);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while displaying employees: {e.Message}");
            }
            return employees;
        }

        public int UpdateEmployee(Employee employee)
        {
            try
            {
                var employeeToUpdate = _context.Employees.Find(employee.EmployeeId);

                if (employeeToUpdate != null)
                {
                    employeeToUpdate.FirstName = employee.FirstName;
                    employeeToUpdate.LastName = employee.LastName;
                    employeeToUpdate.Email = employee.Email;
                    employeeToUpdate.PhoneNumber = employee.PhoneNumber;
                    employeeToUpdate.JobTitle = employee.JobTitle;
                    employeeToUpdate.Department = employee.Department;
                    employeeToUpdate.LocationName = employee.LocationName;
                    employeeToUpdate.Manager = employee.Manager;
                    employeeToUpdate.ProjectName = employee.ProjectName;
                    return _context.SaveChanges();
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while updating an employee: {e.Message}");
                return 0;
            }
        }

        public Employee DisplayEmployeeDetails(string employeeId)
        {
            try
            {
                var employee = _context.Employees.FirstOrDefault(e => e.EmployeeId == employeeId);
                if (employee != null)
                {
                    return employee;
                }
                else
                {
                    Console.WriteLine("Employee doesn't exist");
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while displaying employee details: {e.Message}");
                return null;
            }
        }

        public bool DeleteEmployee(string employeeId)
        {
            try
            {
                var employeeToDelete = _context.Employees.Find(employeeId);
                if (employeeToDelete != null)
                {
                    _context.Employees.Remove(employeeToDelete);
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    Console.WriteLine("Employee doesn't exist");
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while deleting an employee: {e.Message}");
                return false;
            }
        }

        public List<Role> DisplayRoles()
        {
            var roles = new List<Role>();
            try
            {
                roles = _context.Roles.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while displaying roles: {e.Message}");
            }
            return roles;
        }

        public bool AddRole(string roleName, string department, string roleDescription, string locationName)
        {
            try
            {
                var newRole = new Role
                {
                    RoleName = roleName,
                    Department = department,
                    Description = roleDescription,
                    LocationName = locationName
                };

                _context.Roles.Add(newRole);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while adding the role: {e.Message}");
                return false;
            }
        }

        public bool IsEntityExists<T>(string value, string tableName, string columnName) where T : class
        {
            try
            {
                if (tableName == "Roles")
                {
                    if (_context.Roles.FirstOrDefault(r => r.RoleName == value) == null)
                        return false;
                }
                else if (tableName == "Department")
                {
                    if (_context.Departments.FirstOrDefault(r => r.DepartmentName == value) == null)
                        return false;
                }
                else if (tableName == "Locations")
                {
                    if (_context.Locations.FirstOrDefault(r => r.LocationName == value) == null)
                        return false;
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while checking if the {value} exists: {e.Message}");
                return false;
            }
        }

        public bool AddDepartmentOrLocation<T>(T entity, string tableName, string columnName) where T : class
        {
            try
            {
                _context.Set<T>().Add(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while adding {entity}: {e.Message}");
                return false;
            }
        }
    }
}
