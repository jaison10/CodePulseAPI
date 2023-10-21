using CodePulseAPI.Models.DTO;
using CodePulseAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodePulseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUser request)
        {
            var user = new IdentityUser
            {
                UserName = request.Email?.Trim(),
                Email = request.Email?.Trim()
            };
            //creating user
            var result = await this.userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                //assigning roles to user.
                result = await this.userManager.AddToRoleAsync(user, "Reader");
                if (result.Succeeded)
                {
                    return Ok();
                }
                if (result.Errors.Any())
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            else
            {
                if (result.Errors.Any())
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return ValidationProblem(ModelState);
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] RegisterUser request)
        {
            var user = await this.userManager.FindByEmailAsync(request.Email.Trim());
            if (user != null)
            {
                var pswdMatch = await this.userManager.CheckPasswordAsync(user, request.Password);
                if (pswdMatch == true)
                {
                    var roles = await this.userManager.GetRolesAsync(user);

                    var returnUser = new LoggedUser
                    {
                        Email = request.Email,
                        Token = tokenRepository.CreateJwnToken(user, roles.ToList()),
                        Roles = roles.ToList()
                    };
                    return Ok(returnUser);
                }
                else
                {
                    ModelState.AddModelError("", "Login Failed!");
                }
            }
            ModelState.AddModelError("", "Login Failed!");
            return ValidationProblem(ModelState);
        }
    }
}