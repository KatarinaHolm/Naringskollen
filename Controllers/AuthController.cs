using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Naringskollen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<IdentityUser<int>> signInManager;

        public AuthController(SignInManager<IdentityUser<int>> _signInManager)
        {
            signInManager = _signInManager;
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] object empty)
        {
            if (empty != null)
            {
                await signInManager.SignOutAsync();
                return Ok();
            }
            return Unauthorized();
        }
    }
}
