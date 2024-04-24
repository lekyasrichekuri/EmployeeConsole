using EmployeeConsole.Models;

namespace EmployeeConsole.DAL.Interfaces
{
    public interface IEmployeeJsonOperation
    {
        void SaveObjectsToJson(Dictionary<string, Employee> objects, string jsonFilePath, string fileName);
        Dictionary<string, Employee> LoadExistingJsonFile(string jsonFilePath, string employees);
    }
}
