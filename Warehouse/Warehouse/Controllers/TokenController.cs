using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WarehouseProject.dto;
using WarehouseProject.Dto;
using WarehouseProject.Model;

namespace WarehouseProject.Controllers
{
    [ApiController]
    [Route("authorize")]
    public class TokenController : ControllerBase
    {
        byte[] salt = RandomNumberGenerator.GetBytes(128 / 8); // divide by 8 to convert bits to bytes
        private readonly IConfiguration _config;
        private readonly IPasswordHasher<User> passwordHasher;
        private readonly WarehouseContext _context;
        private readonly ILogger<WeatherForecastController> _logger;

        public TokenController(ILogger<WeatherForecastController> logger, WarehouseContext context, IConfiguration config, IPasswordHasher<User> passwordHasher)
        {
            _logger = logger;
            _context = context;
            _config = config;
            this.passwordHasher = passwordHasher;
        }

        [HttpPost]
        [Route("login")]
        public ActionResult Login([FromBody] UserDto userDto)
        {
            var user = _context.User.Where(user => user.Mail == userDto.Email).FirstOrDefault();
            if(user == null)
            {
                return BadRequest("nie ma takiego uzytkownika");
            }
            var result = passwordHasher.VerifyHashedPassword(user, user.Password, userDto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                return BadRequest("zjebales");
            }




            return Ok();




            
        }

        public string GenerateJwt(User user)
        {

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Role, $"{user.Role}"),

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_config.GetSection("JwtIssuer").Value,
                claims,
                expires: expires,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);

        }
        [HttpPost]
        [Route("register")]
        public void Register([FromBody] UserDto userDto)
        {


            var user = new User()
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Mail = userDto.Email,
            };
            var hashedPassword = passwordHasher.HashPassword(user, userDto.Password);
            user.Password = hashedPassword;

            _context.Add(user);
            _context.SaveChanges();
        }
    }
}