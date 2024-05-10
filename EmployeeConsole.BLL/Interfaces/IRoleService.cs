using EmployeeConsole.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeConsole.BLL.Interfaces
{
    public interface IRoleService
    {
        public bool IsRoleNameExists(string roleName);
        public bool AddRole(string RoleName, string Department, string RoleDescription, string LocationName);
        public List<Role> DisplayAll();
    }
}
