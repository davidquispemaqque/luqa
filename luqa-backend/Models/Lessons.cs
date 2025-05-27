using System;
using System.Collections.Generic;

namespace luqa_backend.Models;

public partial class Lessons
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? VideoUrl { get; set; }

    public int? Order { get; set; }

    public string? TypeLesson { get; set; }

    public int? CourseId { get; set; }

    public virtual Course? Course { get; set; }

    public virtual ICollection<Questions> Questions { get; set; } = new List<Questions>();

    public virtual ICollection<Userprogress> Userprogress { get; set; } = new List<Userprogress>();
}
