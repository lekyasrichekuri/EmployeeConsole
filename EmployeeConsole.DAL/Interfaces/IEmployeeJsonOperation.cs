using EmployeeConsole.Models;

namespace EmployeeConsole.DAL.Interfaces
{
    public interface IEmployeeJsonOperation
    {
        void SaveObjectsToJson(Dictionary<string, Employee> objects);
        Dictionary<string, Employee> LoadExistingJsonFile();
    }
}
