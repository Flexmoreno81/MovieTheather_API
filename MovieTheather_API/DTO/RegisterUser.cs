namespace MovieTheather_API.DTO
{
    public class RegisterUser
    {
       public required string username { get; set; }
        public required string password { get; set; }

        public required  string FullName { get; set; }
    }
}
