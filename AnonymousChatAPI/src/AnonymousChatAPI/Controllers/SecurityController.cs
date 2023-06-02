using Application.Interfaces.Email;
using Microsoft.AspNetCore.Mvc;

namespace AnonymousChatAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SecurityController : ControllerBase {

    private readonly IMailService _mailsService;

    public SecurityController(IMailService mailsService) {
        _mailsService = mailsService;
    }

    [HttpPost]
    [Route("report-room")]
    public Task<IActionResult> ReportRoom([FromQuery] string RoomId) {
        try {
            var subject = $"Room {RoomId} was reported";
            var body = $"Room {RoomId} was reported at {DateTime.Now}";
            _mailsService.SendSupportMail(subject, body);
        } catch (Exception e) {
            return Task.FromResult<IActionResult>(Ok(e.Message));
        }
        return Task.FromResult<IActionResult>(Ok());
    }

}
