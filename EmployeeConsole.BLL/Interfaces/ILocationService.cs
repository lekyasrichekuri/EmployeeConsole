using EmployeeConsole.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeConsole.BLL.Interfaces
{
    public interface ILocationService
    {
        List<Location> DisplayAll();
        public bool LocationExists(string locationName);
        public bool AddLocation(Location location);
    }
}
