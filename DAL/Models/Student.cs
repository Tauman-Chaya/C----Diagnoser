using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Student
{
    public int Code { get; set; }

    public string Name { get; set; } = null!;

    public string Mail { get; set; } = null!;

    public int Password { get; set; }

    public string? Phone { get; set; }

    public string? Adress { get; set; }

    public DateOnly? BirthDate { get; set; }

    public int? CodeCourses { get; set; }

    public virtual Course? CodeCoursesNavigation { get; set; }
}
