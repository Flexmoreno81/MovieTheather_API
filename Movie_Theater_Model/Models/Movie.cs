using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Movie_Theater_Model.Models;

public partial class Movie
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int MovieId { get; set; }

    public string Title { get; set; } = null!;

    public string Director { get; set; } = null!;

    public string Genre { get; set; } = null!;
    public string Rating { get; set; } = null!; 


    public int Runtime { get; set; }

    public DateOnly ReleaseYear { get; set; }
  
}
