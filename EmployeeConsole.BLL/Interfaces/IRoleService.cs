using EmployeeConsole.Models;
using System.Data;

namespace EmployeeConsole.BLL.Interfaces
{
    public interface IRoleService
    {
        bool roleNameExists(string roleName);
        void AddRole(Role role);

        List<Role> DisplayAll();
    }
}
