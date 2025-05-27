using System;
using System.Collections.Generic;

namespace luqa_backend.Models;

public partial class Course
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Coursepaths> Coursepaths { get; set; } = new List<Coursepaths>();

    public virtual ICollection<Lessons> Lessons { get; set; } = new List<Lessons>();
}
