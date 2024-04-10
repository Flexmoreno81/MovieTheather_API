namespace MovieTheather_API.DTO
{
    public class MovieDTO
    {
        public string Title { get; set; } = null!;

        public string Director { get; set; } = null!;

        public string Genre { get; set; } = null!;

        public int Runtime { get; set; }

        public string Rating { get; set; } = null!;

        public DateOnly ReleaseYear { get; set; }
    }
}
