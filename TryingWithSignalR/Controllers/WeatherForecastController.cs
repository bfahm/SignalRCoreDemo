using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using TryingWithSignalR.Hubs;

namespace TryingWithSignalR.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private IHubContext<MessageHub> _hubContext;

        public WeatherForecastController(IHubContext<MessageHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { value = "Hello World" });
        }

        [HttpPost("/announcment")]
        public async void Post([FromForm] string message)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", message);
            //await _hubContext.Clients.All.SendAsync("ReceiveMessage", message);
        }

        [HttpPost("/sendToUser")]
        public async void SendToUser([FromForm] string connectionId)
        {
            await _hubContext.Clients.Client(connectionId).SendAsync("ReceiveMessage", "Automated Message");
        }
    }
}
