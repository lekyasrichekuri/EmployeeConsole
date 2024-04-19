using System.Text.Json;
using EmployeeConsole.Models;
using EmployeeConsole.DAL.Interfaces;

namespace EmployeeConsole.DAL.Services
{
    public class EmployeeJsonOperation : IEmployeeJsonOperation
    {
        public void SaveObjectsToJson(Dictionary<string, Employee> objects, string jsonFilePath, string filename)
        {
            string filePath = Path.GetFullPath("Employees.json");
            if (File.Exists(filePath))
            {
                string json = JsonSerializer.Serialize(objects.Values, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, json);
            }
        }

        public Dictionary<string, Employee> LoadExistingJsonFile(string jsonFilePath, string employees)
        {
            string filePath = Path.GetFullPath("Employees.json");
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                var objectsList = JsonSerializer.Deserialize<List<Employee>>(json) ?? new List<Employee>();
                var dictionary = new Dictionary<string, Employee>();
                foreach (var obj in objectsList)
                {
                    if (obj.EmpId != null)
                    {
                        dictionary[obj.EmpId] = obj;
                    }
                }
                return dictionary;
            }
            else
            {
                return new Dictionary<string, Employee>();
            }
        }
    }
}
