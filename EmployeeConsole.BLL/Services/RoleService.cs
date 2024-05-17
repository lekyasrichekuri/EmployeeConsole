using EmployeeConsole.DAL.Interfaces;
using EmployeeConsole.BLL.Interfaces;
using EmployeeConsole.Models;

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
            return _dbService.DisplayRoles();
        }

        public bool AddRole(int Id,string RoleName, string Department, string RoleDescription, string LocationName)
        {
            if(_dbService.AddRole(Id, RoleName, Department, RoleDescription, LocationName))
                return true;
            return false;
        }

        public bool IsRoleNameExists(string roleName)
        {
            if(_dbService.IsEntityExists<Role>(roleName, "Roles", "RoleName"))
                return false;
            return true;
        }
    }
}
