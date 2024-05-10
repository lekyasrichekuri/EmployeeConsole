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

        public bool LocationExists(string locationName)
        {
            using (SqlConnection connection = new SqlConnection(_dbService.GetConnectionString()))
            {
                string query = " select * from locations where LocationName=@locationName";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@LocationName", locationName);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Close();
                        return false;
                    }
                    else
                    {
                        reader.Close();
                        return true;
                    }
                }
            }
        }
        public bool AddLocation(Location location)
        {
            using (SqlConnection connection = new SqlConnection(_dbService.GetConnectionString()))
            {
                connection.Open();
                string insertQuery = "INSERT INTO Locations(LocationName) VALUES (@LocationName)";
                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@LocationName", location.LocationName);
                    command.ExecuteNonQuery();
                }
            }
            return true;
        }


    }
}
