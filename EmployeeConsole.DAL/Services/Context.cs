using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EmployeeConsole.Models;

namespace EmployeeConsole.DAL.Services
{
    public class Context:DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Department> departments { get; set; }
        public DbSet<Location> locations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("data source=SQL-DEV; database=Lekya_EF; integrated security=SSPI; Encrypt=false");
        }
    }
}
