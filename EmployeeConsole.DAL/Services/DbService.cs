using System.Data.SqlClient;
using EmployeeConsole.DAL.Interfaces;
using EmployeeConsole.Models;

namespace EmployeeConsole.DAL.Services
{
    public class DbService : IDbService
    {
        private string ConnectionString = "data source=SQL-Dev; database=Lekya_DB; integrated security=SSPI";
        public bool AddEmployee(string id, string firstName, string lastName, DateTime? dateOfBirth, string Email, string? Phone, DateTime joinDate, string jobTitle, string department, string location, string? manager, string? project)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    string insertQuery = "INSERT INTO Employee(EmployeeID, FirstName, LastName, DateOfBirth, Email, PhoneNumber, JoiningDate, JobTitle, Department, LocationName, Manager, ProjectName) VALUES (@EmployeeID, @FirstName, @LastName, @DateOfBirth, @Email, @PhoneNumber, @JoiningDate, @JobTitle, @Department, @LocationName, @Manager, @ProjectName)";
                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        Dictionary<string, object> parameters = new Dictionary<string, object>
                    {
                        { "@EmployeeID", id },
                        { "@FirstName", firstName },
                        { "@LastName", lastName },
                        { "@DateOfBirth", dateOfBirth },
                        { "@Email", Email },
                        { "@PhoneNumber", Phone },
                        { "@JoiningDate", joinDate },
                        { "@JobTitle", jobTitle },
                        { "@Department", department },
                        { "@LocationName", location },
                        { "@Manager", manager },
                        { "@ProjectName", project }
                    };
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                        }
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                }
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine("An error has occurred while adding an employee " + e.Message);
                return false;
            }
        }

        public Dictionary<string, Employee> DisplayEmployees()
        {
            Dictionary<string, Employee> Employees = new Dictionary<string, Employee>();
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    string query = "Select * from Employee";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Employee employee = new Employee()
                                {
                                    EmployeeId = reader[0].ToString(),
                                    FirstName = reader[1].ToString(),
                                    LastName = reader[2].ToString(),
                                    DateOfBirth = reader[3].ToString(),
                                    Email = reader[4].ToString(),
                                    PhoneNumber = reader[5].ToString(),
                                    JoiningDate = reader[6].ToString(),
                                    LocationName = reader[7].ToString(),
                                    JobTitle = reader[8].ToString(),
                                    Department = reader[9].ToString(),
                                    Manager = reader[10].ToString(),
                                    ProjectName = reader[11].ToString()
                                };
                                Employees.Add(reader[0].ToString(), employee);
                            }
                        }
                        connection.Close();
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("An error has occurred while displaying the employees" + e.Message);
            }
            return Employees;
            
        }

        public int UpdateEmployee(Employee employee)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    string updateQuery = @"UPDATE Employee
                                       SET FirstName = @FirstName, LastName = @LastName,
                                       Email = @Email, PhoneNumber = @PhoneNumber, JobTitle = @JobTitle,
                                       Department = @Department,LocationName = @LocationName, Manager = @Manager,
                                       ProjectName = @ProjectName
                                       WHERE EmployeeID = @EmployeeID";

                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        Dictionary<string, string> parameters = new Dictionary<string, string>
                        {
                            { "@EmployeeId", nameof(employee.EmployeeId) },
                            { "@FirstName", nameof(employee.FirstName) },
                            { "@LastName", nameof(employee.LastName) },
                            { "@Email", nameof(employee.Email) },
                            { "@PhoneNumber", nameof(employee.PhoneNumber) },
                            { "@JobTitle", nameof(employee.JobTitle) },
                            { "@Department", nameof(employee.Department) },
                            { "@LocationName", nameof(employee.LocationName) },
                            { "@Manager", nameof(employee.Manager) },
                            { "@ProjectName", nameof(employee.ProjectName) }
                        };
                        foreach (var mapping in parameters)
                        {
                            command.Parameters.AddWithValue(mapping.Key, typeof(Employee).GetProperty(mapping.Value).GetValue(employee));
                        }
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected;
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("An error has occurred while updating an employee" + e.Message);
                return 0;
            }
        }

        public Employee DisplayEmployeeDetails(string employeeId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    string query = "Select * from Employee";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (reader["EmployeeID"].ToString() == employeeId)
                                {
                                    Employee employee = new Employee()
                                    {
                                        EmployeeId = reader[0].ToString(),
                                        FirstName = reader[1].ToString(),
                                        LastName = reader[2].ToString(),
                                        DateOfBirth = reader[3].ToString(),
                                        Email = reader[4].ToString(),
                                        PhoneNumber = reader[5].ToString(),
                                        JoiningDate = reader[6].ToString(),
                                        JobTitle = reader[7].ToString(),
                                        Department = reader[8].ToString(),
                                        LocationName = reader[9].ToString(),
                                        Manager = reader[10].ToString(),
                                        ProjectName = reader[11].ToString()
                                    };
                                    return employee;
                                }
                            }
                            Console.WriteLine("Employee doesn't exists");
                        }
                        connection.Close();
                    }
                }
                return new Employee() { };
            }
            catch (Exception e)
            {
                Console.WriteLine("An error has occurred while displaying employee details" + e.Message);
                return new Employee() { };
            }
        }

        public bool DeleteEmployee(string employeeId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    string query = "DELETE from Employee where EmployeeID=@employeeId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@EmployeeId", employeeId);
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                            return true;
                        return false;
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("An error has occurred while deleting an employee" + e.Message);
                return false;
            }
        }

        public List<Role> DisplayRoles()
        {
            List<Role> Roles = new List<Role>();
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    string query = "Select * from Roles";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Role role = new Role()
                                {
                                    Id = (int)reader["Id"],
                                    RoleName = reader["RoleName"].ToString(),
                                    Department = reader["Department"].ToString(),
                                    Description = reader["RoleDescription"].ToString(),
                                    LocationName = reader["LocationName"].ToString()
                                };
                                Roles.Add(role);
                            }
                        }
                        connection.Close();
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("An error has occurred while displaying the roles" + e.Message);
            }
            return Roles;
        }

        public bool AddRole(int Id, string RoleName, string Department, string RoleDescription, string LocationName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    string insertQuery = "INSERT INTO Roles(Id, RoleName, Department, RoleDescription, LocationName) VALUES (@Id, @RoleName, @Department, @RoleDescription, @LocationName)";
                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Id", Id);
                        command.Parameters.AddWithValue("@RoleName", RoleName);
                        command.Parameters.AddWithValue("@Department", Department);
                        command.Parameters.AddWithValue("@RoleDescription", RoleDescription);
                        command.Parameters.AddWithValue("@LocationName", LocationName);
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                }
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine("An error occurred while adding the role" + e.Message);
                return false;
            }
        }
        public bool IsEntityExists<T>(string value, string tableName, string columnName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    string query = $"SELECT * FROM {tableName} WHERE {columnName}=@Value";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@Value", value);
                        SqlDataReader reader = command.ExecuteReader();
                        return reader.HasRows;
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine($"An error has occurred while checking if the {value} exists" + e.Message);
                return false;
            }
        }

        public bool AddDepartmentOrLocation<T>(T entity, string tableName, string columnName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    string insertQuery = $"INSERT INTO {tableName}({columnName}) VALUES (@Value)";
                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Value", entity.GetType().GetProperty(columnName).GetValue(entity));
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                }
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine($"An error has occurred while adding {entity}" + e.Message);
                return false;
            }
        }
    }
}
