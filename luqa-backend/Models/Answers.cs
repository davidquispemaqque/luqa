using System;
using System.Collections.Generic;

namespace luqa_backend.Models;

public partial class Answers
{
    public int Id { get; set; }

    public bool? IsCorrect { get; set; }

    public string? Text { get; set; }

    public int? QuestionId { get; set; }

    public virtual Questions? Question { get; set; }
}
