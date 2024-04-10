
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Movie_Theater_Model;
using Movie_Theater_Model.Models;
using MovieTheather_API.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MovieTheather_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        private readonly MovieTheatherContext _context;
        private readonly IConfiguration _configuration;

        public LoginsController(MovieTheatherContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/Logins
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Logins>>> GetLogin()
        {
            return await _context.Logins.ToListAsync();
        }

        // GET: api/Logins/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Logins>> GetLogin(int id)
        {
            var login = await _context.Logins.FindAsync(id);

            if (login == null)
            {
                return NotFound();
            }

            return login;
        }

        // PUT: api/Logins/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLogin(int id, Logins login)
        {
            if (id != login.loginID)
            {
                return BadRequest();
            }

            _context.Entry(login).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoginExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Logins
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Logins>> PostLogin(Logins login)
        {
            _context.Logins.Add(login);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLogin", new { id = login.loginID }, login);
        }

        // DELETE: api/Logins/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLogin(int id)
        {
            var login = await _context.Logins.FindAsync(id);
            if (login == null)
            {
                return NotFound();
            }

            _context.Logins.Remove(login);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpPost("AUTHICATION/")]

        public async Task<ActionResult<string>> authicate_user(loginDTO user) {
            var login_user = await Authication(user);

            if (login_user is null) { 
                return NotFound("Unable to locate the person or the person is not an authurizde user");
            }

            var login_db = login_user.Adapt<Logins>();

            var token = generate_token(login_db);

            return token; 
        } 

        private bool LoginExists(int id)
        {
            return _context.Logins.Any(e => e.loginID == id);
        }



        private async Task<loginDTO?> Authication(loginDTO user) {
            var current_user = await _context.Logins.FirstOrDefaultAsync(x=> x.username == user.username && x.password ==user.password);

            if (current_user is null) {
                return null; 
            }

            var DTO=  current_user.Adapt<loginDTO>();

            return DTO; 
        }


        private string generate_token(Logins Authicaticated_user) {
            var security_key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credit = new SigningCredentials(security_key, SecurityAlgorithms.HmacSha256);


            var claims = new [] {
                 new Claim(ClaimTypes.NameIdentifier, Authicaticated_user.username), 
                 new Claim("ClaimPassword", Authicaticated_user.password)
            };



            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                   _configuration["Jwt:Audience"], claims,
                   expires: DateTime.UtcNow.AddMinutes(30),
                   signingCredentials: credit
               );


            return new JwtSecurityTokenHandler().WriteToken(token);
        } 


    }
}
