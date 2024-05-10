using System;
using System.Data.SqlClient;
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

        public void AddEmployeeToDb(string id, string firstName, string lastName, DateTime? dateOfBirth, string Email, string? Phone, DateTime joinDate, string location, string jobTitle, string department, string? manager, string? project)
        {
            using (SqlConnection connection = new SqlConnection(_dbService.GetConnectionString()))
            {
                connection.Open();
                string insertQuery = "INSERT INTO Employee(EmployeeID, FirstName, LastName, DateOfBirth, Email, PhoneNumber, JoiningDate, LocationName, JobTitle, Department, Manager, ProjectName) VALUES (@EmployeeID, @FirstName, @LastName, @DateOfBirth, @Email, @PhoneNumber, @JoiningDate, @LocationName, @JobTitle, @Department, @Manager, @ProjectName)";
                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeID", id);
                    command.Parameters.AddWithValue("@FirstName", firstName);
                    command.Parameters.AddWithValue("@LastName", lastName);
                    command.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@PhoneNumber", Phone);
                    command.Parameters.AddWithValue("@JoiningDate", joinDate);
                    command.Parameters.AddWithValue("@LocationName", location);
                    command.Parameters.AddWithValue("@JobTitle", jobTitle);
                    command.Parameters.AddWithValue("@Department", department);
                    command.Parameters.AddWithValue("@Manager", manager);
                    command.Parameters.AddWithValue("@ProjectName", project);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            Console.ForegroundColor= ConsoleColor.Green;
            Console.WriteLine("Employee added Successfully");
            Console.ResetColor();
        }
        public Dictionary<string, Employee> DisplayEmployees()
        {
            Dictionary<string, Employee> Employees = new Dictionary<string, Employee>();
            using (SqlConnection connection = new SqlConnection(_dbService.GetConnectionString()))
            {
                string query = "Select * from Employee";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
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
                    }
                    connection.Close();
                }
            }
            return Employees;
        }

        public int UpdateEmployeeDetails(Employee employee)
        {
            using (SqlConnection connection = new SqlConnection(_dbService.GetConnectionString()))
            {
                connection.Open();
                string updateQuery = @"UPDATE Employee
                                       SET FirstName = @FirstName, LastName = @LastName,
                                       Email = @Email, PhoneNumber = @PhoneNumber, LocationName = @LocationName,
                                       JobTitle = @JobTitle, Department = @Department, Manager = @Manager,
                                       ProjectName = @ProjectName
                                   WHERE EmployeeID = @EmployeeID";

                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("EmployeeId", employee.EmployeeId);
                    command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                    command.Parameters.AddWithValue("@LastName", employee.LastName);
                    command.Parameters.AddWithValue("@Email", employee.Email);
                    command.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);
                    command.Parameters.AddWithValue("@LocationName", employee.LocationName);
                    command.Parameters.AddWithValue("@JobTitle", employee.JobTitle);
                    command.Parameters.AddWithValue("@Department", employee.Department);
                    command.Parameters.AddWithValue("@Manager", employee.Manager);
                    command.Parameters.AddWithValue("@ProjectName", employee.ProjectName);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected;
                }
            }
        }

        public Employee DisplayEmpDetails(string employeeId)
        {
            using (SqlConnection connection = new SqlConnection(_dbService.GetConnectionString()))
            {
                string query = "Select * from Employee";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
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
                                        LocationName = reader[7].ToString(),
                                        JobTitle = reader[8].ToString(),
                                        Department = reader[9].ToString(),
                                        Manager = reader[10].ToString(),
                                        ProjectName = reader[11].ToString()
                                    };
                                    return employee;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Employee doesn't exists");
                        }
                    }
                    connection.Close();
                }
            }
            return null;
        }

        public bool DeleteEmployee(string employeeId)
        {
            using (SqlConnection connection = new SqlConnection(_dbService.GetConnectionString()))
            {
                string query = "DELETE from Employee where EmployeeID=@employeeId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@EmployeeId", employeeId);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Employee Deleted Successfully");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Employee doesn't exists");
                        return false;
                    }
                }
            }
        }

    }
}
