using Entities.Entities;
using Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using WebAPI.Models;
using WebAPI.Token;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UsersController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/CreateTokenIdentity")]
        public async Task<IActionResult> CreateTokenIdentity([FromBody] Login login)
        {
            if (string.IsNullOrWhiteSpace(login.email) || string.IsNullOrWhiteSpace(login.password))
                return Unauthorized();

            var resultado = await
                _signInManager.PasswordSignInAsync(login.email, login.password, false, lockoutOnFailure: false);

            if (resultado.Succeeded)
            {
                // Get the logged user
                var userCurrent = await _userManager.FindByEmailAsync(login.email);
                var idUser = userCurrent.Id;

                var token = new TokenJWTBuilder()
                    .AddSecurityKey(JwtSecurityKey.Create("Secret_Key-12345678"))
                .AddSubject("API Dot NET Core")
                .AddIssuer("Test.Securiry.Bearer")
                .AddAudience("Test.Securiry.Bearer")
                .AddClaims("idUser", idUser)
                .AddExpiry(5)
                .Builder();

                return Ok(token.value);
            }
            else
                return Unauthorized();
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/AddUserIdentity")]
        public async Task<IActionResult> AddUserIdentity([FromBody] Login login)
        {
            if (string.IsNullOrWhiteSpace(login.email) || string.IsNullOrWhiteSpace(login.password))
                return Ok("There are data missing");

            var user = new ApplicationUser
            {
                UserName = login.email,
                Email = login.email,
                CPF = login.document,
                Tipo = UserType.Regular,
            };

            var resultCreate = await _userManager.CreateAsync(user, login.password);

            if (resultCreate.Errors.Any())
                return Ok(resultCreate.Errors);

            // Simulate to send an email with the confirmation.
            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            // Retrive false email to confirm it.
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var resultConfirmation = await _userManager.ConfirmEmailAsync(user, code);

            if (resultConfirmation.Succeeded)
                return Ok("User added successfully.");
            else
                return Ok("Error at the confirmation.");

        }
    }
}
