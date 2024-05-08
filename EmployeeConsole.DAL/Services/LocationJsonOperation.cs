using EmployeeConsole.DAL.Interfaces;
using EmployeeConsole.Models;
using System.Text.Json;

namespace EmployeeConsole.DAL.Services
{
    public class LocationJsonOperation : ILocationJsonOperation
    {
        public string jsonFilePath = "C:\\Users\\lekyasri.c\\source\\repos\\CSharp\\EC1\\EmployeeConsole\\EmployeeConsole.Data\\Locations.Json";
        public List<Location> LoadExistingJsonFile()
        {
            try
            {
                string json = File.ReadAllText(jsonFilePath);
                return JsonSerializer.Deserialize<List<Location>>(json) ?? new List<Location>();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Exception: File doesn't exists");
                return new List<Location>();
            }
        }

        public void SaveObjectsToJson(List<Location> objects)
        {
            string json = JsonSerializer.Serialize(objects, new JsonSerializerOptions { WriteIndented = true });
            try
            {
                if (File.Exists(jsonFilePath))
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
