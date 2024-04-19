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

        public void SaveObjectsToJson(Role objects, string jsonFilePath, string fileName)
        {
            //string filePath = Path.GetFullPath("Roles.json");
            //if (File.Exists(filePath))
            //{
            //    string json = JsonSerializer.Serialize(objects, new JsonSerializerOptions { WriteIndented = true });
            //    File.WriteAllText(filePath, json);
            //}
            //File.WriteAllText(jsonFilePath, json);

            string json = JsonSerializer.Serialize(objects, new JsonSerializerOptions { WriteIndented = true });
            try
            {
                if(File.Exists(jsonFilePath))
                File.AppendAllText(jsonFilePath, json);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found");
                File.Create("Roles.json");
                File.WriteAllText(jsonFilePath, json);
            }
        }
    }
}
