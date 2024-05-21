using System.Data.SqlClient;
using EmployeeConsole.DAL.Interfaces;
using EmployeeConsole.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeConsole.DAL.Services
{
    public class DbService : IDbService
    {
        public bool AddEmployee(string id, string firstName, string lastName, string? dateOfBirth, string Email, string? Phone, string joinDate, string jobTitle, string department, string location, string? manager, string? project)
        {
            try
            {
                using (var myContext = new Context())
                {
                    myContext.Employees.Add(new Employee()
                    {
                        EmployeeId = id,
                        FirstName = firstName,
                        LastName = lastName,
                        DateOfBirth = dateOfBirth,
                        Email = Email,
                        PhoneNumber = Phone,
                        JoiningDate = joinDate,
                        JobTitle = jobTitle,
                        Department = department,
                        LocationName = location,
                        Manager = manager,
                        ProjectName = project
                    }
                    );
                    myContext.SaveChanges();
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("An error has occurred while adding an employee\n " + e.Message);
                return false;
            }
        }

        public Dictionary<string, Employee> DisplayEmployees()
        {
            Dictionary<string, Employee> Employees = new Dictionary<string, Employee>();
            try
            {
                using (var myContext = new Context())
                {
                    var employeesFromDb = myContext.Employees.ToList();
                    foreach (var employee in employeesFromDb)
                    {
                        Employees.Add(employee.EmployeeId, employee);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error has occurred while displaying the employees\n" + e.Message);
            }
            return Employees;
        }

        public int UpdateEmployee(Employee employee)
        {
            try
            {
                using (var myContext = new Context())
                {
                    var employeeToUpdate = myContext.Employees.Find(employee.EmployeeId);

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
                        return myContext.SaveChanges();
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error has occurred while updating an employee\n" + e.Message);
                return 0;
            }
        }

        public Employee DisplayEmployeeDetails(string employeeId)
        {
            try
            {
                using (var myContext = new Context())
                {
                    var employee = myContext.Employees.FirstOrDefault(e => e.EmployeeId == employeeId);
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
            }
            catch (Exception e)
            {
                Console.WriteLine("An error has occurred while displaying employee details\n" + e.Message);
                return null;
            }
        }

        public bool DeleteEmployee(string employeeId)
        {
            try
            {
                using (var myContext = new Context())
                {
                    var employeeToDelete = myContext.Employees.Find(employeeId);
                    if (employeeToDelete != null)
                    {
                        myContext.Employees.Remove(employeeToDelete);
                        myContext.SaveChanges();
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Employee doesn't exist");
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error has occurred while deleting an employee\n" + e.Message);
                return false;
            }
        }
    
        public List<Role> DisplayRoles()
        {
            List<Role> Roles = new List<Role>();
            try
            {
                using (var myContext = new Context())
                {
                    Roles = myContext.Roles.ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error has occurred while displaying the roles \n" + e.Message);
            }
            return Roles;
        }

        public bool AddRole( string RoleName, string Department, string RoleDescription, string LocationName)
        {
            try
            {
                using (var myContext = new Context())
                {
                    var newRole = new Role
                    {
                       // Id = Id,
                        RoleName = RoleName,
                        Department = Department,
                        Description = RoleDescription,
                        LocationName = LocationName
                    };

                    myContext.Roles.Add(newRole);
                    myContext.SaveChanges();
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred while adding the role \n" + e.InnerException.Message);
                return false;
            }
        }

        public bool IsEntityExists<T>(string value, string tableName, string columnName) where T : class
        {
            try
            {
                using (var myContext = new Context())
                {
                    if (tableName == "Roles")
                    {
                        if (myContext.Roles.FirstOrDefault(r => r.RoleName == value) == null)
                            return false;
                    }
                    else if (tableName == "Department")
                    {
                        if (myContext.departments.FirstOrDefault(r=>r.DepartmentName==value) == null)
                            return false;
                    }
                    else if (tableName == "Locations")
                    {
                        if (myContext.locations.FirstOrDefault(r=>r.LocationName==value) == null)
                            return false;
                    }
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error has occurred while checking if the {value} exists \n" + e.Message);
                return false;
            }
        }

        public bool AddDepartmentOrLocation<T>(T entity, string tableName, string columnName) where T : class
        {
            try
            {
                using (var myContext = new Context())
                {
                    myContext.Set<T>().Add(entity);
                    myContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error has occurred while adding {entity}: {e.Message}");
                return false;
            }
        }
    }
}
