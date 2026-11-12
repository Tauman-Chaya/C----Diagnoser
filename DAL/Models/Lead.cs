using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Lead
{
    public int Code { get; set; }

    public string Name { get; set; } = null!;

    public string Mail { get; set; } = null!;

    public int Password { get; set; }

    public string? Phone { get; set; }

    public int CodeType { get; set; }

    public virtual TypeGroup CodeTypeNavigation { get; set; } = null!;
}
