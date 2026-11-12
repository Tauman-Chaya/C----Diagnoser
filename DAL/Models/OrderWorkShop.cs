using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class OrderWorkShop
{
    public int Code { get; set; }

    public int CodeDiagnoser { get; set; }

    public int CodeCustomer { get; set; }

    public int TypeWorkShop { get; set; }

    public DateOnly Date { get; set; }

    public string? Adress { get; set; }

    public int Price { get; set; }

    public bool? Paied { get; set; }

    public bool Parsent { get; set; }

    public virtual Customer CodeCustomerNavigation { get; set; } = null!;

    public virtual Diagnoser CodeDiagnoserNavigation { get; set; } = null!;

    public virtual Workshop TypeWorkShopNavigation { get; set; } = null!;
}
