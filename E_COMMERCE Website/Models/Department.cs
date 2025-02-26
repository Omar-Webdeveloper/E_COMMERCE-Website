using System;
using System.Collections.Generic;

namespace E_COMMERCE_Website.Models;

public partial class Department
{
    public int Id { get; set; }

    public string? DepartmentName { get; set; }

    public string? DepartmentManger { get; set; }

    public string? Location { get; set; }

    public decimal? Budget { get; set; }
}
