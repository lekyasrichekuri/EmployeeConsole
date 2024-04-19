using EmployeeConsole.Models;
using EmployeeConsole.BLL.Interfaces;
using EmployeeConsole.DAL.Interfaces;
using System.Data;
using System.Reflection;

namespace EmployeeConsole.BLL.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleJsonOperation _roleJsonOperation;
        public RoleService(IRoleJsonOperation roleJsonOperation)
        {
            _roleJsonOperation = roleJsonOperation;
        }

        public bool roleNameExists(string roleName)
        {
            List<Models.Role> rolesList = _roleJsonOperation.LoadExistingJsonFile("C:\\Users\\lekyasri.c\\source\\repos\\CSharp\\EC1\\EmployeeConsole\\EmployeeConsole.Data\\Roles.Json", "Roles.Json");
            var roleNames = rolesList.Select(r => r.RoleName);
            if (roleNames.Contains(roleName))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Role already exists");
                Console.ResetColor();
                return false;
            }
            return true;
        }
        public void AddRole(Role role)
        {
            //List<Role> roles = _roleJsonOperation.LoadExistingJsonFile("C:\\Users\\lekyasri.c\\source\\repos\\CSharp\\EC1\\EmployeeConsole\\EmployeeConsole.Data\\Roles.Json", "Roles.Json");
            //roles.Add(role);
            _roleJsonOperation.SaveObjectsToJson(role, "C:\\Users\\lekyasri.c\\source\\repos\\CSharp\\EC1\\EmployeeConsole\\EmployeeConsole.Data\\Roles.Json", "Roles.Json");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Role added successfully");
            Console.ResetColor();
        }

        public List<Role> DisplayAll()
        {
            List<Models.Role> role = _roleJsonOperation.LoadExistingJsonFile("C:\\Users\\lekyasri.c\\source\\repos\\CSharp\\EC1\\EmployeeConsole\\EmployeeConsole.Data\\Roles.Json", "Roles.Json");

            if (!role.Any())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No roles to display");
                Console.ResetColor();
            }
            return role;
        }
    }
}
