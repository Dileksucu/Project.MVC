using System;
using System.Collections.Generic;

namespace DataAccess.ModelFirstNorthwind;

public partial class CustomersWork
{
    public string CustomerId { get; set; } = null!;

    public string CompanyName { get; set; } = null!;

    public string? ContactName { get; set; }
}
