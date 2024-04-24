using System;
using System.Text.Json;
using EmployeeConsole.Models;
using EmployeeConsole.DAL.Interfaces;

namespace EmployeeConsole.DAL.Services
{
    public class RoleJsonOperation : IRoleJsonOperation
    {
        public List<Role> LoadExistingJsonFile(string jsonFilePath, string roles)
        {
            try
            {
                string json = File.ReadAllText(jsonFilePath);
                return JsonSerializer.Deserialize<List<Role>>(json) ?? new List<Role>();
            }
            catch(FileNotFoundException)
            {
                Console.WriteLine("Exception: File doesn't exists");
                return new List<Role>();
            }
        }

        public void SaveObjectsToJson(List<Role> objects, string jsonFilePath, string fileName)
        {
            string json = JsonSerializer.Serialize(objects, new JsonSerializerOptions { WriteIndented = true });
            try
            {
                if(File.Exists(jsonFilePath))
                    File.WriteAllText(jsonFilePath, json);
                else
                {
                    File.Create("Roles.json");
                    File.WriteAllText(jsonFilePath, json);
                }

            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found");
            }
        }
    }
}
