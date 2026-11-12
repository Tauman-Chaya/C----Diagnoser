using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Reference
{
    public int Code { get; set; }

    public int CodeWorkShop { get; set; }

    public int CodeCustomer { get; set; }

    public DateOnly Date { get; set; }

    public double Time { get; set; }

    public string Adress { get; set; } = null!;

    public string? Comments { get; set; }

    public int Status { get; set; }

    public virtual Customer CodeCustomerNavigation { get; set; } = null!;

    public virtual Workshop CodeWorkShopNavigation { get; set; } = null!;

    public virtual Status StatusNavigation { get; set; } = null!;
}
