using EmployeeConsole.Models;

namespace EmployeeConsole.DAL.Interfaces
{
    public interface IRoleJsonOperation
    {
        List<Role> LoadExistingJsonFile(string jsonFilePath, string roles);
        void SaveObjectsToJson(List<Role> objects, string jsonFilePath, string fileName);
    }
}
