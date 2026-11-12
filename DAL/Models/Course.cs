using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Course
{
    public int Code { get; set; }

    public string Name { get; set; } = null!;

    public int AccountOfStudent { get; set; }

    public string? Desciption { get; set; }

    public int AccountOfMeetings { get; set; }

    public string Day { get; set; } = null!;

    public DateOnly FirstMeeting { get; set; }

    public string? Adress { get; set; }

    public int Price { get; set; }
}
