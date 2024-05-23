using System;
using System.Collections.Generic;

namespace EmployeeConsole.Models;

public partial class Role
{
    public int Id { get; set; }

    public string RoleName { get; set; } = null!;

    public string Department { get; set; } = null!;

    public string? Description { get; set; }

    public string LocationName { get; set; } = null!;
}
