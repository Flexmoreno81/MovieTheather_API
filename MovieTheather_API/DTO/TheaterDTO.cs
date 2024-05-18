namespace MovieTheather_API.DTO
{
    public class TheaterDTO
    {

        public int TheatherId { get; set; }
        public string Name { get; set; } = null!;

        public string State { get; set; } = null!;

        public string City { get; set; } = null!;

        public string Zipcode { get; set; } = null!;

        public int SeatCapacity { get; set; }
    }
}
