using System;
using System.Collections.Generic;

namespace DataAccess.ModelFirstNorthwind;

public partial class GetProduct
{
    public string CategoryName { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public decimal? UnitPrice { get; set; }
}
