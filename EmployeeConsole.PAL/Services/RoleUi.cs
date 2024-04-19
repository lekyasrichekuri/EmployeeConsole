using System.Text.RegularExpressions;
using EmployeeConsole.PAL.Interfaces;
using EmployeeConsole.BLL.Interfaces;
namespace EmployeeConsole.PAL.Services
{
    public class RoleUi : IRoleUi
    {
        private readonly IRoleService _roleService;
        public RoleUi( IRoleService roleService)
        {
            _roleService = roleService;
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
                switch (option)
                {
                    case 1:
                        AddRole();
                        break;
                    case 2:
                        DisplayAll();
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

        public void AddRole()
        {
            Models.Role r = new Models.Role();
            string roleName = ValidateText("Role Name");
            r.RoleName = roleName;

            if (_roleService.roleNameExists(roleName) == false)
            {
                return;
            }

            string department = ValidateText("Department");
            r.Department = department;

            string description = ValidateText("Description");
            r.Description = description;

            string location = ValidateText("Location");
            r.Location = location;

            _roleService.AddRole(r);
        }

        public void DisplayAll()
        {
            List<Models.Role> role = _roleService.DisplayAll();
            foreach (var r in role)
            {
                Console.WriteLine("Role Details:");
                Console.WriteLine($"Role Name: {r.RoleName}");
                Console.WriteLine($"Department: {r.Department}");
                Console.WriteLine($"Description: {r.Description}");
                Console.WriteLine($"Location: {r.Location}");
                Console.WriteLine("======================================");
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
    }

}

