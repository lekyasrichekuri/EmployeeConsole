using EmployeeConsole.Models;
using System.Data;

namespace EmployeeConsole.BLL.Interfaces
{
    public interface IRoleService
    {
        bool IsRoleNameExists(string roleName);
        bool AddRole(Role role);
        List<Role> DisplayAll();
    }
}
