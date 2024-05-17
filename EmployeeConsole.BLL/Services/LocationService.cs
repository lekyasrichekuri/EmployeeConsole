using System.Data.SqlClient;
using EmployeeConsole.DAL.Interfaces;
using EmployeeConsole.BLL.Interfaces;
using EmployeeConsole.Models;


namespace EmployeeConsole.BLL.Services
{
    public class LocationService:ILocationService
    {
        private readonly IDbService _dbService;
        public LocationService(IDbService dbService)
        {
            _dbService = dbService;
        }

        public bool IsLocationExists(string locationName)
        {
            if(_dbService.IsEntityExists<Location>(locationName, "Locations", "LocationName"))
                return false;    
            return true;
        }

        public bool AddLocation(Location location)
        {
            if(_dbService.AddDepartmentOrLocation<Location>(location,"Locations","LocationName"))
                return true;
            return false;
        }
    }
}
