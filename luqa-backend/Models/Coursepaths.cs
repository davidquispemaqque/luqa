using System;
using System.Collections.Generic;

namespace luqa_backend.Models;

public partial class Coursepaths
{
    public int Id { get; set; }

    public int? OrderInPath { get; set; }

    public int? RouteId { get; set; }

    public int? CourseId { get; set; }

    public virtual Course? Course { get; set; }

    public virtual Route? Route { get; set; }
}
