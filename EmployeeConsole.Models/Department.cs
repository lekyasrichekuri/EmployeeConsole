using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EmployeeConsole.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string DepartmentName { get; set; }
    }
}
