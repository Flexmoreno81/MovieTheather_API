using Microsoft.AspNetCore.Identity;
using MovieTheather_API.DTO;

namespace MovieTheather_API.Services
{
    public interface IAuthicationServicess
    {
        public Task<IdentityResult> RegisterUser(RegisterUser user);

        

        public  Task<LoginResult> Login(LoginUser registerUser);
    }
}