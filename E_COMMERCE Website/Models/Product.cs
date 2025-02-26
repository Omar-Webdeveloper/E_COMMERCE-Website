using System;
using System.Collections.Generic;

namespace E_COMMERCE_Website.Models;

public partial class Product
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Price { get; set; }

    public string? Description { get; set; }

    public string? Image { get; set; }
}
