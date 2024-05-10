using EmployeeConsole.Models;

namespace EmployeeConsole.PL.Interfaces
{
    public interface IRoleUi
    {
        void AddRole();
        void DisplayAllRoles();
        void RoleManager();
        string ValidateText(string type);
        public string ValidateLocation(string type);
    }
}
