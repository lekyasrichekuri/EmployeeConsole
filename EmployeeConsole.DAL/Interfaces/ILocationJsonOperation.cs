using EmployeeConsole.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeConsole.DAL.Interfaces
{
    public interface ILocationJsonOperation
    {
        List<Location> LoadExistingJsonFile();
        void SaveObjectsToJson(List<Location> objects);
    }
}
