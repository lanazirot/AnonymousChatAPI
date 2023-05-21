using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnonymousChatAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase {
        
        private readonly IRandomUserService _randomUserService;
        public AuthController(IRandomUserService randomUserService) {
            _randomUserService = randomUserService;
        }

        /// <summary>
        /// Login with a random user given a UserIdentifier
        /// </summary>
        /// <param name="UserIdentifier">User email</param>
        /// <returns>Ok and a random user</returns>
        [HttpGet("anonymous-login")]
        [AllowAnonymous]
        public async Task<IActionResult> GetRandomUser([FromQuery] string UserIdentifier) {
            var randomUser = await _randomUserService.AssignRandomUser(UserIdentifier);
            return Ok(randomUser);
        }

    }
}
