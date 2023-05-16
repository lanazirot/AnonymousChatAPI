using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnonymousChatAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase {

        private readonly IStreamIOService _streamIOService;

        public UserController(IStreamIOService streamIOService) {
            _streamIOService = streamIOService;
        }

        [HttpGet("connect-user")]
        [AllowAnonymous]
        public async Task<IActionResult> GetToken([FromQuery] string Email) {
            var token = await _streamIOService.CreateToken(Email);
            return Ok(token);
        }

        [HttpPost("create-user")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser([FromQuery] string Email) {
            try {
                await _streamIOService.CreateUser(Email);
                return Ok();
            }catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

    }
}
