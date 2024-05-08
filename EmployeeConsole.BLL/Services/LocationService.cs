using EmployeeConsole.DAL.Interfaces;
using EmployeeConsole.Models;
using EmployeeConsole.BLL.Interfaces;
using EmployeeConsole.DAL.Services;

namespace EmployeeConsole.BLL.Services
{
    public class LocationService: ILocationService
    {
        private readonly ILocationJsonOperation _locationJsonOperation;
        public LocationService(ILocationJsonOperation locationJsonOperation)
        {
            _locationJsonOperation = locationJsonOperation;
        }

        public bool LocationExists(string locationName)
        {
            List<Models.Location> locationsList = _locationJsonOperation.LoadExistingJsonFile();
            var locationNames = locationsList.Select(location => location.LocationName);
            if (locationNames.Contains(locationName))
            {
                return false;
            }
            return true;
        }

        public List<Location> DisplayAll()
        {
            List<Models.Location> location = _locationJsonOperation.LoadExistingJsonFile();
            return location;
        }

        public bool AddLocation(Location location)
        {
            List<Location> locations = _locationJsonOperation.LoadExistingJsonFile();
            locations.Add(location);
            _locationJsonOperation.SaveObjectsToJson(locations);
            return true;
        }
    }
}
