using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MovieTheather_API.DTO
{
    public class MovieDTO
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MovieId { get; set; }
        public string Title { get; set; } = null!;

        public string Director { get; set; } = null!;

        public string Genre { get; set; } = null!;

        public int Runtime { get; set; }

        public string Rating { get; set; } = null!;

        public DateOnly ReleaseYear { get; set; }
    }
}
