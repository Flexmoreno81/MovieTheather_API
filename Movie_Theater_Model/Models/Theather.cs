using System;
using System.Collections.Generic;

namespace Movie_Theater_Model.Models;

public partial class Theather
{
    public int TheatherId { get; set; }

    public string Name { get; set; } = null!;

    public string State { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Zipcode { get; set; } = null!;

    public int SeatCapacity { get; set; }
}
