using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;
using EmployeeConsole.BLL.Interfaces;
using EmployeeConsole.PL.Interfaces;
using EmployeeConsole.PL.Exceptions;
using EmployeeConsole.Models;

namespace EmployeeConsole.PL.Services
{
    public class EmployeeUi : IEmployeeUi
    {
        private readonly IEmployeeService _employeeService;
        private readonly IRoleService _roleService;
        public EmployeeUi(IEmployeeService employeeService,IRoleService roleService)
        { 
            _employeeService = employeeService;
            _roleService = roleService;
        }
     
        public void EmployeeManager()
        {
            bool isValid = true;
            while (isValid)
            {
                Console.WriteLine("Select an option:");
                Console.WriteLine("1. Add Employee");
                Console.WriteLine("2. Display All Employees");
                Console.WriteLine("3. Display one employee");
                Console.WriteLine("4. Edit Employee");
                Console.WriteLine("5. Delete Employee");
                Console.WriteLine("6. Go Back");
                int empOption;
                if (!int.TryParse(Console.ReadLine(), out empOption))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid option. Please enter a number.");
                    Console.ResetColor();
                    continue;
                }
                else
                {
                    switch (empOption)
                    {
                        case 1:
                            AddEmployee();
                            break;
                        case 2:
                            DisplayEmployees();
                            break;
                        case 3:
                            DisplayEmpDetails();
                            break;
                        case 4:
                            UpdateEmployee();
                            break;
                        case 5:
                            DeleteEmployee();
                            break;
                        case 6:
                            isValid = false;
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Enter a Valid Option");
                            Console.ResetColor();
                            break;
                    }
                }
            }
        }

        public void DisplayEmployees()
        {
            Dictionary<string, Models.Employee> employee = _employeeService.DisplayEmployees();
            if (!employee.Any())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No employees to display");
                Console.ResetColor();
            }            
            else
            {
                foreach (var employ in employee)
                {
                    Console.WriteLine("Employee Details:");
                    Console.WriteLine($"Employee Id: {employ.Key}");
                    Console.WriteLine($"First Name: {employ.Value.FirstName}");
                    Console.WriteLine($"Last Name: {employ.Value.LastName}");
                    Console.WriteLine($"Role: {employ.Value.JobTitle}");
                    Console.WriteLine($"Department: {employ.Value.Department}");
                    Console.WriteLine($"Location: {employ.Value.LocationName}");
                    Console.WriteLine($"Joining Date: {employ.Value.JoiningDate}");
                    Console.WriteLine($"Manager Name: {employ.Value.Manager}");
                    Console.WriteLine($"Project Name: {employ.Value.ProjectName}");
                    Console.WriteLine("======================================");
                }
            }
        }

        public void AddEmployee()
        {
            Models.Employee employee = new Models.Employee();

            string employeeId = GenerateEmployeeId();
            employee.EmployeeId = employeeId; 

            string firstName = ValidateText("First Name");
            employee.FirstName= firstName;

            string lastName = ValidateText("Last Name");
            employee.LastName = lastName;

            DateTime dateOfBirth = ValidateDate("Birth");
            employee.DateOfBirth = dateOfBirth.ToShortDateString();

            string email = ValidateEmail();
            employee.Email = email;

            string phoneNumber = ValidatePhoneNumber();
            employee.PhoneNumber = phoneNumber;

            DateTime joinDate = ValidateDate("Joining");
            employee.JoiningDate = joinDate.ToShortDateString();

            string jobTitle = DisplayJobTitles();
            employee.JobTitle = jobTitle;

            string department = DisplayDepartmentsForRole(jobTitle);
            employee.Department = department;

            string location = DisplayLocationsForRoleAndDepartment(jobTitle, department);
            employee.LocationName = location;

            string manager = ValidateText("Manager");
            employee.Manager = manager;

            string project = ValidateText("Project");
            employee.ProjectName = project;

            _employeeService.AddEmployeeToDb(employeeId, firstName, lastName, dateOfBirth, email, phoneNumber, joinDate, jobTitle, department, location, manager, project);
        }

        public string GenerateEmployeeId()
        {
            Dictionary<string, Employee> employees = _employeeService.DisplayEmployees();
            int maximumId = 0;
            foreach (var employee in employees.Values)
            {
                int idNumber;
                if (int.TryParse(employee.EmployeeId.Substring(2), out idNumber))
                {
                    if (idNumber > maximumId)
                    {
                        maximumId = idNumber;
                    }
                }
            }
            string nextEmployeeId = "TZ" + (maximumId + 1).ToString().PadLeft(4, '0');
            return nextEmployeeId;
        }

        public string DisplayJobTitles()
        {
            var jobTitles = _roleService.DisplayAll()
                .Select(role => role.RoleName)
                .Distinct()
                .ToList();

            if (jobTitles.Any())
            {
                Console.WriteLine("Select a Job Title:");
                int index = 1;
                foreach (var title in jobTitles)
                {
                    Console.WriteLine(index + ". " + title);
                    index++;
                }
                int jobRole;
                if (!int.TryParse(Console.ReadLine(), out jobRole))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid option. Please enter a number.");
                    Console.ResetColor();
                }
                if (jobRole==0 || jobRole>=index)
                {
                    Console.WriteLine("Enter the correct option");
                    return DisplayJobTitles();
                }
                else
                {
                    return jobTitles[jobRole - 1];
                }
            }
            else
            {
                Console.WriteLine("No job titles found.");
            }
            return "";
        }

        public string DisplayDepartmentsForRole(string selectedRole)
        {
            var roles = _roleService.DisplayAll()
                .Where(role => role.RoleName.Equals(selectedRole, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (roles.Any())
            {
                var departments = roles
                    .Select(role => role.Department)
                    .Distinct()
                    .ToList();

                Console.WriteLine("Select a Department:");
                int index = 1;
                foreach (var depart in departments)
                {
                    Console.WriteLine(index + ". " + depart);
                    index++;
                }
                int department;
                if (!int.TryParse(Console.ReadLine(), out department))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid option. Please enter a number.");
                    Console.ResetColor();
                }
                if (department == 0 || department >= index)
                {
                    Console.WriteLine("Enter the correct option:");
                    return DisplayDepartmentsForRole(selectedRole);
                }
                else
                {
                    return departments[department - 1];
                }
            }
            else
            {
                Console.WriteLine("No Departments found with the role.");
                return "";
            }
        }


        public string DisplayLocationsForRoleAndDepartment(string selectedRole, string selectedDepartment)
        {
            var roles = _roleService.DisplayAll()
                .Where(role => role.RoleName.Equals(selectedRole, StringComparison.OrdinalIgnoreCase))
                .Where(role => role.Department.Equals(selectedDepartment, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (roles.Any())
            {
                var locations = roles
                    .Select(role => role.LocationName)
                    .Distinct()
                    .ToList();
                Console.WriteLine("Select a Location:");
                int index = 1;
                foreach (var loc in locations)
                {
                    Console.WriteLine(index + ". " + loc);
                    index++;
                }
                int location;
                if (!int.TryParse(Console.ReadLine(), out location))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid option. Please enter a number.");
                    Console.ResetColor();
                }
                if (location == 0 || location >= index)
                {
                    Console.WriteLine("Enter the correct option:");
                    return DisplayLocationsForRoleAndDepartment(selectedRole, selectedDepartment);
                }
                else
                {
                    return locations[location - 1];
                }
            }
            else
            {
                Console.WriteLine("No locations found with the role and department.");
                return "";
            }
        }


        public void UpdateEmployee()
        {
            string employeeId = ValidateEmployeeId();
            Models.Employee employee = _employeeService.DisplayEmpDetails(employeeId);
            if(employee==null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Employee id does not exists");
                Console.ResetColor();
                UpdateEmployee();
            }
            bool toUpdate = true;
            while (toUpdate)
            {
                Console.WriteLine("Enter the option of the field to be updated:");
                Console.WriteLine("1. FirstName");
                Console.WriteLine("2. LastName");
                Console.WriteLine("3. Email");
                Console.WriteLine("4. Phone number");
                Console.WriteLine("5. Job Tile");
                Console.WriteLine("6. Department");
                Console.WriteLine("7. Location");
                Console.WriteLine("8. Manager");
                Console.WriteLine("9. Project");
                Console.WriteLine("10. Go Back");
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
                        employee.FirstName = ValidateText("FirstName");
                        break;
                    case 2:
                        employee.LastName = ValidateText("LastName");
                        break;
                    case 3:
                        employee.Email = ValidateEmail();
                        break;
                    case 4:
                        employee.PhoneNumber = ValidatePhoneNumber();
                        break;
                    case 5:
                        employee.JobTitle = DisplayJobTitles();
                        break;
                    case 6:
                        employee.Department = DisplayDepartmentsForRole(employee.JobTitle);
                        break;
                    case 7:
                        employee.LocationName = DisplayLocationsForRoleAndDepartment(employee.JobTitle,employee.Department);
                        break;
                    case 8:
                        employee.Manager = ValidateText("Manager");
                        break;
                    case 9:
                        employee.ProjectName = ValidateText("Project");
                        break;
                    case 10:
                        toUpdate = false;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Enter the correct option");
                        Console.ResetColor();
                        break;
                }
                if (option!=0 && option<=9)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Employee details updated successfully");
                    Console.ResetColor();
                }
                _employeeService.UpdateEmployeeDetails(employee);
            }
        }

        public void DisplayEmpDetails()
        {
            string employeeId = ValidateEmployeeId();
            Models.Employee employee = _employeeService.DisplayEmpDetails(employeeId);
            if (employee==null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Employee Id does not exist");
                Console.ResetColor();
                DisplayEmpDetails();
                return;
            }
            Console.WriteLine("Employee Details:");
            PropertyInfo[] properties = typeof(Models.Employee).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                Console.WriteLine($"{property.Name}: {property.GetValue(employee)}");
            }
        }

        public void DeleteEmployee()
        {
            string employeeId = ValidateEmployeeId();
            if(_employeeService.DeleteEmployee(employeeId)== true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Employee deleted successfully");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Employee Id does not exist");
                Console.ResetColor();
                DeleteEmployee();
            }
        }

        public string ValidateEmployeeId()
        {
            string employeeId = "";
            bool isValidEmployeeId = false;
            while (!isValidEmployeeId)
            {
                Console.WriteLine("Enter Employee Id in the format(TZ0001):");
                employeeId = Console.ReadLine() ?? "";
                string pattern = @"TZ\d{4}$";
                if (employeeId == "TZ0000")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Employee ID is not valid");
                    Console.ResetColor();
                }
                else if (Regex.IsMatch(employeeId, pattern))
                {
                    isValidEmployeeId = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Enter a valid Employee id in the format TZ0001:");
                    Console.ResetColor();
                }
            }
            return employeeId;
        }

        public string ValidateText(string type)
        {
            string text = "";
            bool isValidText = false;
            while (!isValidText)
            {
                Console.WriteLine($"Enter Employee {type}:");
                text = Console.ReadLine() ?? "";
                string pattern = @"^[A-Za-z\s]+$";
                if (type == "Manager" && string.IsNullOrEmpty(text) || type == "Project" && string.IsNullOrEmpty(text))
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

        public DateTime ValidateDate(string type)
        {
            DateTime date;
            while (true)
            {
                Console.WriteLine($"Enter {type} Date in the format DD-MM-YYYY:");
                string input = Console.ReadLine() ?? "";
                if (type == "Birth" && string.IsNullOrWhiteSpace(input))
                {
                    return DateTime.MinValue;
                }
                else if (DateTime.TryParseExact(input, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                {
                    try
                    {
                        if (type == "Birth" && CalculateAge(date) < 18)
                        {
                            throw new InvalidAgeException("Age must be greater than 18");
                        }
                        else if(type == "Birth" && CalculateAge(date) > 60)
                        {
                            throw new InvalidAgeException("Age must be less than 60");
                        }
                        return date;
                    }
                    catch (InvalidAgeException ex) 
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please enter date in the format DD-MM-YYYY.");
                    Console.ResetColor();
                }
            }
        }

        private int CalculateAge(DateTime birthDate)
        {
            int age = DateTime.Now.Year - birthDate.Year;
            if(DateTime.Now.DayOfYear<birthDate.DayOfYear)
            {
                age--;
            }
            return age;
        }

        public string ValidateEmail()
        {
            string email;
            while (true)
            {
                Console.WriteLine("Enter Email:");
                email = Console.ReadLine() ?? "";
                string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                if (!string.IsNullOrEmpty(email) && Regex.IsMatch(email, pattern))
                {
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Enter valid email id:");
                    Console.ResetColor();
                }
            }
            return email;
        }

        public string ValidatePhoneNumber()
        {
            string phoneNumber = "";
            bool isValidPhoneNumber = false;
            while (!isValidPhoneNumber)
            {
                Console.WriteLine("Enter PhoneNumber:");
                phoneNumber = Console.ReadLine() ?? "";
                if (string.IsNullOrEmpty(phoneNumber))
                {
                    isValidPhoneNumber = true;
                }
                else if (phoneNumber.Length != 10 || !IsNumeric(phoneNumber))
                { 
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Enter a valid 10-digit PhoneNumber containing only digits.");
                    Console.ResetColor();
                }
                else if (phoneNumber.Substring(0, 1) == "0")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Phone Number cannot start with 0");
                    Console.ResetColor();
                }
                else
                {
                    isValidPhoneNumber = true;
                }
            }
            return phoneNumber;
        }

        public static bool IsNumeric(string phoneNumber)
        {
            foreach (char character in phoneNumber)
            {
                if (!char.IsDigit(character))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
