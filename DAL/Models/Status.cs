using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Status
{
    public int Code { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Reference> References { get; set; } = new List<Reference>();
}
