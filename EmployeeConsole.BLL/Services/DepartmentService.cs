using EmployeeConsole.DAL.Interfaces;
using EmployeeConsole.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeConsole.BLL.Interfaces;

namespace EmployeeConsole.BLL.Services
{
    public class DepartmentService:IDepartmentService
    {
        private readonly IDbService _dbService;
        public DepartmentService(IDbService dbService)
        {
            _dbService = dbService;
        }

        public bool IsDepartmentExists(string departmentName)
        {
            if(_dbService.IsEntityExists<Department>(departmentName,"Department","DepartmentName"))
                return false;
            return true;
        }

        public bool AddDepartment(Department departmentName)
        {
            if(_dbService.AddDepartmentOrLocation<Department>(departmentName,"Department","DepartmentName"))
                return true;
            return false;
        }
    }
}
