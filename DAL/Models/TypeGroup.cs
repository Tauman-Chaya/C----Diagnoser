using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class TypeGroup
{
    public int Code { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual ICollection<Lead> Leads { get; set; } = new List<Lead>();

    public virtual ICollection<Workshop> Workshops { get; set; } = new List<Workshop>();
}
