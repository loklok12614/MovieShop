using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IConfiguration _configuration;

        public AccountController(IAccountService accountService, IConfiguration configuration)
        {
            _accountService = accountService;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register( [FromBody] UserRegisterModel model)
        {
            var userId = await _accountService.RegisterUser(model);
            if (userId == 0)
            {
                return BadRequest("User with this email already exist");
            }

            return Ok(userId);
        }
        
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginModel model)
        {
            var user = await _accountService.ValidateUser(model);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Incorrect Login Credentials");
                // return Unauthorized(new { errorMessage = "Incorrect Login Credentials" });
            }
            // Create Token
            var jwtToken = CreateJwtToken(user);
            return Ok(new { token = jwtToken });
        }

        private string CreateJwtToken(UserLoginSuccessModel user)
        {
            // create the claims
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                new Claim("Country", "USA"),
                new Claim("Language", "English")
            };
            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);
            
            // Specify a secret key
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["secretKey"]));
            
            // Specify the algorithm
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            
            // Specify expiration of the token
            var tokenExpiration = DateTime.UtcNow.AddDays(7);
            
            // create an object with all the above information to create the token
            var tokenDetails = new SecurityTokenDescriptor
            {
                Subject = identityClaims,
                Expires = tokenExpiration,
                SigningCredentials = credentials,
                Issuer = "Lok's MovieShop, Inc",
                Audience = "MovieShop Clients",
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var encodedJwt = tokenHandler.CreateToken(tokenDetails);
            return tokenHandler.WriteToken(encodedJwt);
        }
    }
}
