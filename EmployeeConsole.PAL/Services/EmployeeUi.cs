using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;
using EmployeeConsole.BLL.Interfaces;
using EmployeeConsole.PAL.Interfaces;

namespace EmployeeConsole.PAL.Services
{
    public class EmployeeUi : IEmployeeUi
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeUi( IEmployeeService employeeService)
        {
            _employeeService = employeeService;
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
                        Console.WriteLine("Enter a Valid Option");
                        break;
                }
            }
        }

        public void DisplayEmployees()
        {
            Dictionary<string, Models.Employee> employee = _employeeService.DisplayEmployees();
            foreach (var emp in employee)
            {
                Console.WriteLine("Employee Details:");
                Console.WriteLine($"Employee Id: {emp.Key}");
                Console.WriteLine($"Full Name: {emp.Value.Name}");
                Console.WriteLine($"Role: {emp.Value.JobTitle}");
                Console.WriteLine($"Department: {emp.Value.Department}");
                Console.WriteLine($"Location: {emp.Value.Location}");
                Console.WriteLine($"Joining Date: {emp.Value.JoiningDate}");
                Console.WriteLine($"Manager Name: {emp.Value.Manager}");
                Console.WriteLine($"Project Name: {emp.Value.Project}");
                Console.WriteLine("======================================");
            }
        }
        public void AddEmployee()
        {
            Models.Employee emp = new Models.Employee();
            string empId = ValidateEmployeeId();
            emp.EmpId = empId;

            if (_employeeService.employeeIdExists(empId) == false)
            {
                return;
            }

            string firstName = ValidateText("First Name");
            string lastName = ValidateText("Last Name");
            emp.Name = firstName + " " + lastName;

            DateTime dob = ValidateDate("Birth");
            emp.DateOfBirth = dob.ToShortDateString();

            string email = ValidateEmail();
            emp.Email = email;

            string phoneNumber = ValidatePhoneNumber();
            emp.PhoneNumber = phoneNumber;

            DateTime joinDate = ValidateDate("Joining");
            emp.JoiningDate = joinDate.ToShortDateString();

            string location = ValidateText("Location");
            emp.Location = location;

            string jobTitle = ValidateText("Job Title");
            emp.JobTitle = jobTitle;

            string department = ValidateText("Department");
            emp.Department = department;

            string manager = ValidateText("Manager");
            emp.Manager = manager;

            string project = ValidateText("Project");
            emp.Project = project;

            _employeeService.AddEmployee(emp);
        }

        public void UpdateEmployee()
        {
            Console.WriteLine("Enter the employee id to be updated:");
            string empid = Console.ReadLine() ?? "";
            //Dictionary<string, Models.Employee> employee = _employeeJsonOperation.LoadExistingJsonFile("C:\\Users\\lekyasri.c\\Downloads\\TASKS\\Task 5 - EmployeeConsole ClassLibrary\\EmployeeConsole.Data\\Employees.json", "Employees.json");
            //if (!employee.ContainsKey(empid))
            //{
            //    Console.ForegroundColor = ConsoleColor.Red;
            //    Console.WriteLine("Employee id does not exists");
            //    Console.ResetColor();
            //    return;
            //}
            //Models.Employee emp = employee[empid];
            Models.Employee emp = _employeeService.UpdateEmployee(empid);
            if(emp==null)
            {
                return;
            }
            bool toUpdate = true;
            while (toUpdate)
            {
                Console.WriteLine("Enter the option of the field to be updated:");
                Console.WriteLine("1. Name");
                Console.WriteLine("2. Email");
                Console.WriteLine("3. Phone number");
                Console.WriteLine("4. Location");
                Console.WriteLine("5. Job Title");
                Console.WriteLine("6. Department");
                Console.WriteLine("7. Manager");
                Console.WriteLine("8. Project");
                Console.WriteLine("9. Go Back");
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
                        emp.Name = ValidateText("Name");
                        break;
                    case 2:
                        emp.Email = ValidateEmail();
                        break;
                    case 3:
                        emp.PhoneNumber = ValidatePhoneNumber();
                        break;
                    case 4:
                        emp.Location = ValidateText("Location");
                        break;
                    case 5:
                        emp.JobTitle = ValidateText("Job Title");
                        break;
                    case 6:
                        emp.Department = ValidateText("Department");
                        break;
                    case 7:
                        emp.Manager = ValidateText("Manager");
                        break;
                    case 8:
                        emp.Project = ValidateText("Project");
                        break;
                    case 9:
                        toUpdate = false;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Enter the correct option");
                        Console.ResetColor();
                        break;
                }
                if (option != 9)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Employee details updated successfully");
                    Console.ResetColor();
                }
                _employeeService.UpdateEmployee(emp);
            }
        }

        public void DisplayEmpDetails()
        {
            Console.WriteLine("Enter Employee ID to view details:");
            string empId = Console.ReadLine() ?? "";
            Models.Employee emp = _employeeService.DisplayEmpDetails(empId);
            if(emp==null)
            {
                return;
            }
            Console.WriteLine("Employee Details:");
            PropertyInfo[] properties = typeof(Models.Employee).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                Console.WriteLine($"{property.Name}: {property.GetValue(emp)}");
            }
        }

        public void DeleteEmployee()
        {
            Console.WriteLine("Enter Employee ID to delete:");
            string empId = Console.ReadLine() ?? "";
            _employeeService.DeleteEmployee(empId);
        }

        public string ValidateEmployeeId()
        {
            string empId = "";
            bool isValidEmpId = false;
            while (!isValidEmpId)
            {
                Console.WriteLine("Enter Employee Id in the format(TZ0001):");
                empId = Console.ReadLine() ?? "";
                string pattern = @"TZ\d{4}$";
                if (empId == "TZ0000")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Employee ID is not valid");
                    Console.ResetColor();
                }
                else if (Regex.IsMatch(empId, pattern))
                {
                    isValidEmpId = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Enter a valid Employee id in the format TZ0001:");
                    Console.ResetColor();
                }
            }
            return empId;
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
                    return date;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please enter date in the format DD-MM-YYYY.");
                    Console.ResetColor();
                }
            }
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
                else if (string.IsNullOrEmpty(phoneNumber) || phoneNumber.Length != 10 || !IsNumeric(phoneNumber))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Enter a valid 10-digit PhoneNumber containing only digits.");
                    Console.ResetColor();
                }
                else
                {
                    isValidPhoneNumber = true;
                }
            }
            return phoneNumber;
        }

        public static bool IsNumeric(string str)
        {
            foreach (char c in str)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
