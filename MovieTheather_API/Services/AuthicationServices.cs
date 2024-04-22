using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Movie_Theater_Model;
using Movie_Theater_Model.Models;

using MovieTheather_API.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MovieTheather_API.Services
{
    public class AuthicationServices : IAuthicationServicess
    {
        private readonly IConfiguration _configuration;
        private readonly MovieTheatherContext _context;

        private readonly UserManager<UserLogins> _userManager;


        public AuthicationServices(IConfiguration config, UserManager<UserLogins> usermanager, MovieTheatherContext context)
        {
            _userManager = usermanager;
            _configuration = config;
            _context = context;
        }

         private string GenerateToken(LoginUser registerd_user)
        {
            var security_key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var sigingcredit = new SigningCredentials(security_key, SecurityAlgorithms.HmacSha256);


            var claims = new[] {
                 new Claim (ClaimTypes.Email, registerd_user.username),
                 new Claim (ClaimTypes.Name, registerd_user.username),
                 new Claim (ClaimTypes.Role, "admin")
            };


            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                   _configuration["Jwt:Audience"], claims,
                   expires: DateTime.UtcNow.AddMinutes(30),
                   signingCredentials: sigingcredit
               );


            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public async Task<LoginResult> Login(LoginUser registerUser)
        {
            UserLogins? username = await _userManager.FindByNameAsync(registerUser.username);

            if (username == null) {
                return new LoginResult { 
                    Success = false,
                    Message = "Incorrect username or password, please try again", 
                    Token = null
                };

            }

            bool password = await _userManager.CheckPasswordAsync(username, registerUser.password);


            if (!password) {
                return new LoginResult
                {
                    Success = false,
                    Message = "Incorrect username or password, please try again",
                    Token = null
                };

            }


            string token = GenerateToken(registerUser); 

            return new LoginResult {
                Success = true,
                Message = "Login Sucessful", 
                Token = token
            };  
        }

        public async Task<IdentityResult> RegisterUser(RegisterUser user)
        {

            var created_user = new UserLogins {
                UserName = user.username,
                Email = user.username,
                FullName = user.FullName
                  
            };  

           
           

            var  result = await _userManager.CreateAsync(created_user, user.password );


            return result; 


        }


        



    }
}
