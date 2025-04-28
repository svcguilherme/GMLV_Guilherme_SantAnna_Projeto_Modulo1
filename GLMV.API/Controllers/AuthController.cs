using GLMV.Application.Services;
using GLMV.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace GLMV.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser>? _userManager;
        private readonly SalesPersonService _salesPersonService;

        public AuthController(SignInManager<IdentityUser> signInManager,
                                UserManager<IdentityUser>? userManager,
                                SalesPersonService salesPersonService,
                                IOptions<JwtSettings> jwtSettings)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _salesPersonService = salesPersonService;
            _jwtSettings = jwtSettings.Value;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar(RegisterUserViewModel registerUser)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var user = new IdentityUser
            {
                UserName = registerUser.Email,
                Email = registerUser.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, registerUser.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "SALESPERSON");

                await _signInManager.SignInAsync(user, false);

                SalesPerson salesPerson = new SalesPerson 
                { 
                  Id = user.Id, 
                  Email = user.Email, 
                  FirstName = registerUser.FirstName, 
                  LastName = registerUser.LastName };

                await _salesPersonService.AddAsync(salesPerson);

                await _salesPersonService.SaveASync();

                return Ok(await GerarJwt());
            }

            return Problem("Falha ao registrar o usuário");
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginViewModel userLoginViewModel)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var result = await _signInManager.PasswordSignInAsync(userLoginViewModel.Email, userLoginViewModel.Password, false, true);

            if (result.Succeeded)
            {
                return Ok(await GerarJwt());
            }

            return Ok();
        }

        private async Task<object> GerarJwt()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Segredo);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _jwtSettings.Emissor,
                Audience = _jwtSettings.Audiencia,
                Expires = DateTime.UtcNow.AddHours(_jwtSettings.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var encodedToken = tokenHandler.WriteToken(token);

            return new
            {
                access_token = encodedToken,
                expiration = tokenDescriptor.Expires
            };
        }


    }
}
