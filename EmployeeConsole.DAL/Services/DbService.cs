using System.Data.SqlClient;
using EmployeeConsole.DAL.Interfaces;

namespace EmployeeConsole.DAL.Services
{
    public class DbService: IDbService
    {
        public string GetConnectionString()
        {
            return "data source=SQL-Dev; database=Lekya_DB; integrated security=SSPI";
        }
    }
}
