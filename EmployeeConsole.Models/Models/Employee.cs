using System;
using System.Collections.Generic;

namespace EmployeeConsole.Models;

public partial class Employee
{
    public string EmployeeId { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? DateOfBirth { get; set; }

    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string JoiningDate { get; set; } = null!;

    public string JobTitle { get; set; } = null!;

    public string Department { get; set; } = null!;

    public string LocationName { get; set; } = null!;

    public string? Manager { get; set; }

    public string? ProjectName { get; set; }
}
