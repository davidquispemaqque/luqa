using System;
using System.Collections.Generic;

namespace luqa_backend.Models;

public sealed partial class Userprogress
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? LessonId { get; set; }

    public string? Status { get; set; }

    public DateTime? ProgressDate { get; set; }

    public Lessons? Lesson { get; set; }

    public Usuarios? User { get; set; }
}
