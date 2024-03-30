using System;
using System.Collections.Generic;

namespace Movie_Theater_Model.Models;

public partial class ScreenTime
{
    public int ScreeningId { get; set; }

    public int MovieId { get; set; }

    public int TheatherId { get; set; }

    public TimeOnly ScreenTime1 { get; set; }

    public virtual Movie Movie { get; set; } = null!;

    public virtual Theather Theather { get; set; } = null!;
}
