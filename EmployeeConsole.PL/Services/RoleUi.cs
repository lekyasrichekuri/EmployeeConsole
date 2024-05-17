using System.Text.RegularExpressions;
using EmployeeConsole.PL.Interfaces;
using EmployeeConsole.BLL.Interfaces;
using EmployeeConsole.Models;
using EmployeeConsole.BLL.Services;
using System.Text;
namespace EmployeeConsole.PL.Services
{
    public class RoleUi : IRoleUi
    {
        private readonly IRoleService _roleService;
        private readonly ILocationService _locationService;
        private readonly IDepartmentService _departmentService;
        public RoleUi( IRoleService roleService, ILocationService locationService, IDepartmentService departmentService)
        {
            _roleService = roleService;
            _locationService = locationService;
            _departmentService = departmentService;
        }
        public void RoleManager()
        {
            bool isValid = true;
            while (isValid)
            {
                Console.WriteLine("Select an option:");
                Console.WriteLine("1. Add Role");
                Console.WriteLine("2. Display All");
                Console.WriteLine("3. Go Back");
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
                            AddRole();
                            break;
                        case 2:
                            DisplayAllRoles();
                            break;
                        case 3:
                            isValid = false;
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

        public void AddRole()
        {
            Models.Role role = new Models.Role();

            int id = GenerateId();
            role.Id = id;   

            string roleName = ValidateText("Role Name");
            role.RoleName = roleName;

            if (_roleService.IsRoleNameExists(roleName) == false)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Role already exists");
                Console.ResetColor();
                AddRole();
                return;
            }

            string department = ValidateDepartment("Department");
            role.Department = department;

            string description = ValidateText("Description");
            role.Description = description;

            string location = ValidateLocation("Location");
            role.LocationName = location;

            if(_roleService.AddRole(id,roleName,department,description,location))
            { 
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Role added successfully");
                Console.ResetColor();
            }
        }

        public int GenerateId()
        {
            List<Role> roles = _roleService.DisplayAll();
            int maximumId = 0;
            foreach(var role in roles)
            {
                if (role.Id > maximumId)
                {
                    maximumId = role.Id;
                }
            }
            return maximumId+1;
        }
        public void DisplayAllRoles()
        {
            List<Models.Role> roles = _roleService.DisplayAll();
            if (!roles.Any())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No roles to display");
                Console.ResetColor();
            }
            else
            {
                foreach (var role in roles)
                {
                    Console.WriteLine("Role Details:");
                    Console.WriteLine($"ID: {role.Id}");
                    Console.WriteLine($"Role Name: {role.RoleName}");
                    Console.WriteLine($"Department: {role.Department}");
                    Console.WriteLine($"Description: {role.Description}");
                    Console.WriteLine($"Location: {role.LocationName}");
                    Console.WriteLine("======================================");
                }
            }
        }

        public string ValidateText(string type)
        {
            string text = "";
            bool isValidText = false;
            while (!isValidText)
            {
                Console.WriteLine($"Enter {type}:");
                text = Console.ReadLine() ?? "";
                string pattern = @"^[A-Za-z\s]+$";
                if (type == "Description" && string.IsNullOrEmpty(text))
                {
                    isValidText = true;
                }
                else if (Regex.IsMatch(text, pattern))
                {
                    isValidText = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Enter a valid {type} containing only letters.");
                    Console.ResetColor();
                }
            }
            return text;
        }
        public string ValidateLocation(string type)
        {
            Location selectedLocation = null;
            Console.WriteLine($"Enter {type}:");
            string input = Console.ReadLine()?? "";
            if (_locationService.IsLocationExists(input))
            {
                selectedLocation = new Location { LocationName = input };
                _locationService.AddLocation(selectedLocation);
            }
            return input;
        }
        public string ValidateDepartment(string type)
        {
            Department selectedDepartment = null;
            Console.WriteLine($"Enter {type}:");
            string input = Console.ReadLine() ?? "";
            if (_departmentService.IsDepartmentExists(input))
            {
                selectedDepartment = new Department { DepartmentName = input };
                _departmentService.AddDepartment(selectedDepartment);
            }
            return input;
        }
    }
}
