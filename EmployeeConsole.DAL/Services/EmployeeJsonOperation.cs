﻿using System.Text.Json;
using EmployeeConsole.Models;
using EmployeeConsole.DAL.Interfaces;
using System.Collections.Generic;

namespace EmployeeConsole.DAL.Services
{
    public class EmployeeJsonOperation : IEmployeeJsonOperation
    {
        public string jsonFilePath = "C:\\Users\\lekyasri.c\\source\\repos\\CSharp\\EC1\\EmployeeConsole\\EmployeeConsole.Data\\Employees.Json" ;
        public void SaveObjectsToJson(Dictionary<string,Employee> objects)
        {
            string json = JsonSerializer.Serialize(objects.Values, new JsonSerializerOptions { WriteIndented = true });
            try
            {
                if (File.Exists(jsonFilePath))
                    File.WriteAllText(jsonFilePath, json);
                else
                {
                    File.Create("Employees.json");
                    File.WriteAllText(jsonFilePath, json);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("File not found");
            }

        }

        public Dictionary<string, Employee> LoadExistingJsonFile()
        {
            try
            {
                string json = File.ReadAllText(jsonFilePath);
                var objectsList = JsonSerializer.Deserialize<List<Employee>>(json) ?? new List<Employee>();
                var dictionary = new Dictionary<string, Employee>();
                foreach (var objects in objectsList)
                {
                    if (objects.EmployeeId != null)
                    {
                        dictionary[objects.EmployeeId] = objects;
                    }
                }
                return dictionary;
            }
            catch(Exception)
            {
                Console.WriteLine("Exception: File doesn't exists");
                return new Dictionary<string, Employee>();
            }
        }
    }
}
