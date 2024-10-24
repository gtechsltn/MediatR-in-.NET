using MediatR;
using MediatRPriorityNotifications.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediatRPriorityNotifications.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IPublisher _publisher;

        public TestController(IPublisher publisher)
        {
            _publisher = publisher;
        }

        [HttpPost("low")]
        public async Task<IActionResult> PostLowPriority([FromBody] string message)
        {
            await _publisher.Publish(new LowPriorityNotification(message));
            return Ok("Low priority notification sent.");
        }

        [HttpPost("high")]
        public async Task<IActionResult> PostHighPriority([FromBody] string message)
        {
            await _publisher.Publish(new HighPriorityNotification(message));
            return Ok("High priority notification sent.");
        }
    }
}
