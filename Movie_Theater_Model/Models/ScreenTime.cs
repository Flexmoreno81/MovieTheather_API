using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Movie_Theater_Model.Models;

public partial class ScreenTime
{
    public int ScreeningId { get; set; }

    public int MovieId { get; set; }

    public int TheatherId { get; set; }


    [Column(TypeName = "VARCHAR")]
    [StringLength(50)]
    public string ScreenTime1 { get; set; }

    public virtual Movie Movie { get; set; } = null!;

    public virtual Theather Theather { get; set; } = null!;
}
