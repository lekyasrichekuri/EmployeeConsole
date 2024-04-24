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

        public bool IsRoleNameExists(string roleName)
        {
            List<Models.Role> rolesList = _roleJsonOperation.LoadExistingJsonFile("C:\\Users\\lekyasri.c\\source\\repos\\CSharp\\EC1\\EmployeeConsole\\EmployeeConsole.Data\\Roles.Json", "Roles.Json");
            var roleNames = rolesList.Select(role => role.RoleName);
            if (roleNames.Contains(roleName))
            { 
                return false;
            }
            return true;
        }
        public bool AddRole(Role role)
        {
            List<Role> roles = _roleJsonOperation.LoadExistingJsonFile("C:\\Users\\lekyasri.c\\source\\repos\\CSharp\\EC1\\EmployeeConsole\\EmployeeConsole.Data\\Roles.Json", "Roles.Json");
            roles.Add(role);
            _roleJsonOperation.SaveObjectsToJson(roles, "C:\\Users\\lekyasri.c\\source\\repos\\CSharp\\EC1\\EmployeeConsole\\EmployeeConsole.Data\\Roles.Json", "Roles.Json");
            return true;
        }

        public List<Role> DisplayAll()
        {
            List<Models.Role> role = _roleJsonOperation.LoadExistingJsonFile("C:\\Users\\lekyasri.c\\source\\repos\\CSharp\\EC1\\EmployeeConsole\\EmployeeConsole.Data\\Roles.Json", "Roles.Json");
            return role;
        }
    }
}
