using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeConsole.Models;

namespace EmployeeConsole.BLL.Interfaces
{
    public interface ILocationService
    {
        public bool AddLocation(Location LocationName);
        public bool LocationExists(string locationName);

    }
}
