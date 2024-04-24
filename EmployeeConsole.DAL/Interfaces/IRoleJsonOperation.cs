using EmployeeConsole.Models;

namespace EmployeeConsole.DAL.Interfaces
{
    public interface IRoleJsonOperation
    {
        List<Role> LoadExistingJsonFile();
        void SaveObjectsToJson(List<Role> objects);
    }
}
