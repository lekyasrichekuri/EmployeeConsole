using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeConsole.DAL.Interfaces;
using EmployeeConsole.BLL.Interfaces;
using EmployeeConsole.Models;
using System.Numerics;
using System.Collections;
using EmployeeConsole.DAL.Services;

namespace EmployeeConsole.BLL.Services
{
    public class RoleService :IRoleService
    {
        private readonly IDbService _dbService;
        public RoleService(IDbService dbService)
        {
            _dbService = dbService;
        }
        public List<Role> DisplayAll()
        {
            List<Role> Roles = new List<Role>();
            using (SqlConnection connection = new SqlConnection(_dbService.GetConnectionString()))
            {
                string query = "Select * from Roles";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Role role = new Role()
                                {
                                    RoleName = reader["RoleName"].ToString(),
                                    Department = reader["Department"].ToString(),
                                    Description = reader["RoleDescription"].ToString(),
                                    LocationName = reader["LocationName"].ToString()
                                };
                                Roles.Add(role);
                            }    
                        }
                    }
                    connection.Close();
                }
            }
            return Roles;
        }

        public bool AddRole(string RoleName, string Department, string RoleDescription, string LocationName)
        {
            using (SqlConnection connection = new SqlConnection(_dbService.GetConnectionString()))
            {
                connection.Open();
                string insertQuery = "INSERT INTO Roles(RoleName, Department, RoleDescription, LocationName) VALUES (@RoleName, @Department, @RoleDescription, @LocationName)";
                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
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

        public bool IsRoleNameExists(string roleName)
        {
            using (SqlConnection connection = new SqlConnection(_dbService.GetConnectionString()))
            {
                string query = " select * from roles where RoleName=@roleName";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@RoleName", roleName);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Close();
                        return false;
                    }
                    else
                    {
                        reader.Close();
                        return true;
                    }
                }
            }
        }


    }
}
