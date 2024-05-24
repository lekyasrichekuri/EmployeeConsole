using Microsoft.Extensions.DependencyInjection;
using EmployeeConsole.PL.Services;
using EmployeeConsole.PL.Interfaces;
using EmployeeConsole.BLL.Services;
using EmployeeConsole.BLL.Interfaces;
using EmployeeConsole.DAL.Interfaces;
using EmployeeConsole.DAL.Services;
using EmployeeConsole.Models;
using Microsoft.Extensions.Configuration;

namespace EmployeeConsole.Main
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                                  .AddTransient<IEmployeeService, EmployeeService>()
                                  .AddTransient<ILocationService, LocationService>()
                                  .AddTransient<IRoleService, RoleService>()
                                  .AddTransient<IDepartmentService, DepartmentService>()
                                  .AddTransient<IDbService, DbService>()
                                  .AddTransient<LekyaEfContext>()
                                  .AddTransient<IEmployeeUi, EmployeeUi>()
                                  .AddTransient<IRoleUi, RoleUi>()
                                  .BuildServiceProvider();
            while (true)
            {
                Console.WriteLine("Select an Option:");
                Console.WriteLine("1. Employee Management");
                Console.WriteLine("2. Role Management");
                Console.WriteLine("3. Exit");
                int option;
                if (!int.TryParse(Console.ReadLine(), out option))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid option. Please enter a number.");
                    Console.ResetColor();
                    continue;
                }
                else
                {
                    switch (option)
                    {
                        case 1:
                            var employeeUI = serviceProvider.GetRequiredService<IEmployeeUi>();
                            employeeUI.EmployeeManager();
                            break;
                        case 2:
                            var roleUI = serviceProvider.GetRequiredService<IRoleUi>();
                            roleUI.RoleManager();
                            break;
                        case 3:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Enter a valid option");
                            Console.ResetColor();
                            break;
                    }
                }
            }
        }
    }
}
