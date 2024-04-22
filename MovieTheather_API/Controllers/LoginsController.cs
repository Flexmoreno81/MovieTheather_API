
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


using Movie_Theater_Model;

using MovieTheather_API.DTO;
using MovieTheather_API.Services;



namespace MovieTheather_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        private readonly MovieTheatherContext _context;
        private readonly IConfiguration _configuration;

        private readonly IAuthicationServicess _auth;
       

        public LoginsController(MovieTheatherContext context, IConfiguration configuration, IAuthicationServicess auth)
        {
            _context = context;
            _configuration = configuration;
            _auth = auth;
        }


        [HttpPost("login")]
        public async Task<ActionResult<LoginResult>> Login(LoginUser login)
        {
            LoginResult? logins = await _auth.Login(login);

            if (logins is null)
            {
                return Unauthorized();
            }

            return Ok(logins);
        }

        [HttpPost("register")]
        public async Task<ActionResult<IdentityResult>> RegisterUser(RegisterUser user)
        {
            var registerUserResult = await _auth.RegisterUser(user);

            if (registerUserResult is null)
            {
                return BadRequest("THERE IS AN ISSUE");
            }

            return Ok(registerUserResult);
        }



    }
}
