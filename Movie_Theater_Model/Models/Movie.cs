using System;
using System.Collections.Generic;

namespace Movie_Theater_Model.Models;

public partial class Movie
{
    public int MovieId { get; set; }

    public string Title { get; set; } = null!;

    public string Director { get; set; } = null!;

    public string Genre { get; set; } = null!;

    public int Runtime { get; set; }

    public DateOnly ReleaseYear { get; set; }
  
}
