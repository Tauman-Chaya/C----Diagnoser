using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Workshop
{
    public int Code { get; set; }

    public int CodeDiagnoser { get; set; }

    public int TypeGroup { get; set; }

    public string? Description { get; set; }

    public bool Morfology { get; set; }

    public bool Grafology { get; set; }

    public bool Chirology { get; set; }

    public int Price { get; set; }

    public int? AccontOfPeople { get; set; }

    public virtual Diagnoser CodeDiagnoserNavigation { get; set; } = null!;

    public virtual ICollection<OrderWorkShop> OrderWorkShops { get; set; } = new List<OrderWorkShop>();

    public virtual ICollection<Reference> References { get; set; } = new List<Reference>();

    public virtual TypeGroup TypeGroupNavigation { get; set; } = null!;
}
