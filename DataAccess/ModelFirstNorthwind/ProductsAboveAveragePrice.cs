using System;
using System.Collections.Generic;

namespace DataAccess.ModelFirstNorthwind;

public partial class ProductsAboveAveragePrice
{
    public string ProductName { get; set; } = null!;

    public decimal? UnitPrice { get; set; }
}
