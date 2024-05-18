using Movie_Theater_Model.Models;

namespace MovieTheather_API.DTO
{
    public class ScreenTimeDTO
    {
        public int ScreeningId { get; set; }

      

        public string ScreenTime1 { get; set; }

        public virtual Movie Movie { get; set; } = null!;

        public virtual Theather Theather { get; set; } = null!;
    }
}
