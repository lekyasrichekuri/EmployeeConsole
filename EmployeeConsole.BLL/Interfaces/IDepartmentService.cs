using EmployeeConsole.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeConsole.BLL.Interfaces
{
    public interface IDepartmentService
    {
        public bool AddDepartment(Department Department);
        public bool IsDepartmentExists(string department) ;
    }
}
