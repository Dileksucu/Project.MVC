﻿using System;
using System.Collections.Generic;

namespace DataAccess.ModelFirstNorthwind;

public partial class SummaryOfSalesByYear
{
    public DateTime? ShippedDate { get; set; }

    public int OrderId { get; set; }

    public decimal? Subtotal { get; set; }
}
