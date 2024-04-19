namespace EmployeeConsole.PAL.Interfaces
{
    public interface IEmployeeUi
    {
        void DisplayEmployees();
        void AddEmployee();
        void UpdateEmployee();
        void DisplayEmpDetails();
        void DeleteEmployee();
        void EmployeeManager();
        string ValidateEmployeeId();
        string ValidateText(string type);
        DateTime ValidateDate(string type);
        string ValidateEmail();
        string ValidatePhoneNumber();
    }

}
