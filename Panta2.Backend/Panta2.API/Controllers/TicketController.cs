using Microsoft.AspNetCore.Mvc;
using Panta2.Core.Models.Ticket;
using System.Text.Json;

namespace Panta2.API.Controllers
{
    [Route("api/tickets")]
    [ApiController]

    public class TicketController : ControllerBase
    {
        private IEnumerable<TicketModel>? _ticketList;

        public TicketController(IWebHostEnvironment webHostEnvironment)
        {
            string jsonString = System.IO.File.ReadAllText(Path.Combine(webHostEnvironment.WebRootPath, "tickets.json"));

            _ticketList = JsonSerializer.Deserialize<TicketModel[]>(jsonString);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketModel>>> GetAll()
        {
            return Ok(_ticketList);
        }

        [HttpGet("state-counted")]
        public async Task<ActionResult> GetTicketCountedPerState()
        {
            int openTicketCount = _ticketList.Count(t => t.State == "Open");
            int inProgressTicketCount = _ticketList.Count(t => t.State == "In Progress");
            int closedTicketCount = _ticketList.Count(t => t.State == "Closed");

            return Ok(new {openTicketCount, inProgressTicketCount, closedTicketCount});
        }

        [HttpGet("priority-counted")]
        public async Task<ActionResult> GetTicketCountedPerPriority()
        {
            int normalTicketCount = _ticketList.Count(t => t.Priority == "Normal");
            int highTicketCount = _ticketList.Count(t => t.Priority == "High");
            int emergencyTicketCount = _ticketList.Count(t => t.Priority == "Emergency");

            return Ok(new { normalTicketCount, highTicketCount, emergencyTicketCount });
        }
    }
}
