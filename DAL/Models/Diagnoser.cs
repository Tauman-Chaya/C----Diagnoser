using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Diagnoser
{
    public int Code { get; set; }

    public string Name { get; set; } = null!;

    public string Mail { get; set; } = null!;

    public int Password { get; set; }

    public string? Phone { get; set; }

    public bool Morphology { get; set; }

    public bool Graphology { get; set; }

    public bool Chirology { get; set; }

    public bool Available { get; set; }

    public int? PrecentagePayment { get; set; }

    public virtual ICollection<OrderWorkShop> OrderWorkShops { get; set; } = new List<OrderWorkShop>();

    public virtual ICollection<Workshop> Workshops { get; set; } = new List<Workshop>();
}
