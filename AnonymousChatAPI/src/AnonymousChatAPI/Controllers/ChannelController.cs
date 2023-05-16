using Application.Interfaces.Services;
using Domain.DTOs.Channel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnonymousChatAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ChannelController : ControllerBase {

        private readonly IStreamIOService _streamIOService;
        public ChannelController(IStreamIOService streamIOService) {
            _streamIOService = streamIOService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateChannel([FromBody] CreateChannelDTO Channel) {
            var request = await _streamIOService.CreateChannel(Channel);
            return Ok(request);
        }

        [HttpDelete]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteChannel([FromQuery] string ChannelID) {
            var request = await _streamIOService.DeleteChannel(ChannelID);
            return Ok(request);
        }

        [HttpPost("add-member-to-channel")]
        [AllowAnonymous]
        public async Task<IActionResult> AddMemberToChannel([FromBody] AddMemberToChannelDTO Member) {
            var request = await _streamIOService.AddMemberToChannel(Member);
            return Ok(request);
        }
    }
}
