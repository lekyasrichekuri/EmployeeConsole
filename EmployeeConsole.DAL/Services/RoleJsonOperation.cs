using System.Text.Json;
using EmployeeConsole.Models;
using EmployeeConsole.DAL.Interfaces;

namespace EmployeeConsole.DAL.Services
{
    public class RoleJsonOperation : IRoleJsonOperation
    {
        public List<Role> LoadExistingJsonFile(string jsonFilePath, string roles)
        {
            //string filePath = Path.GetFullPath("Roles.json");
            //if (File.Exists(filePath))
            //{
            //    string json = File.ReadAllText(filePath);
            //    return JsonSerializer.Deserialize<List<Role>>(json) ?? new List<Role>();
            //}
            //else
            //{
            //    return new List<Role>();
            //}
            if (File.Exists(jsonFilePath))
            {
                string json = File.ReadAllText(jsonFilePath);
                return JsonSerializer.Deserialize<List<Role>>(json) ?? new List<Role>();
            }
            else
            {
                return new List<Role>();
            }
        }

        public void SaveObjectsToJson(List<Role> objects, string jsonFilePath, string fileName)
        {
            //string filePath = Path.GetFullPath("Roles.json");
            //if (File.Exists(filePath))
            //{
            //    string json = JsonSerializer.Serialize(objects, new JsonSerializerOptions { WriteIndented = true });
            //    File.WriteAllText(filePath, json);
            //}
            string json = JsonSerializer.Serialize(objects, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(jsonFilePath, json);
        }
    }
}
