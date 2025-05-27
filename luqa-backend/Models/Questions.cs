using System;
using System.Collections.Generic;

namespace luqa_backend.Models;

public partial class Questions
{
    public int Id { get; set; }

    public string Text { get; set; } = null!;

    public int? LessonId { get; set; }

    public virtual ICollection<Answers> Answers { get; set; } = new List<Answers>();

    public virtual Lessons? Lesson { get; set; }
}
