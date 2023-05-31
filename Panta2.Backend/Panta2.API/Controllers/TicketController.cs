using Microsoft.AspNetCore.Mvc;
using Panta2.Core.Models.Ticket;

namespace Panta2.API.Controllers
{
    [Route("api/tickets")]
    [ApiController]

    public class TicketController : ControllerBase
    {
        private IEnumerable<TicketModel> _ticketList;

        public TicketController()
        {
            _ticketList = new TicketModel[]
            {
                new TicketModel
                {
                    TicketNum = 123456,
                    Subject = "PULIZIA POS DIMISSIONE",
                    Priority = "Normal",
                    State = "Open",
                    CreationDate = new DateTime(2023, 5, 1, 12, 35, 0),
                    LastModificationDate = new DateTime(2023, 5, 11, 9, 12, 0)
                },
                new TicketModel
                {
                    TicketNum = 123456443,
                    Subject = "PULIZIA POS DIMISSIONE URGENTE",
                    Priority = "Emergency",
                    State = "Closed",
                    CreationDate = new DateTime(2023, 5, 5, 14, 44, 0),
                    LastModificationDate = new DateTime(2023, 5, 6, 9, 18, 0)
                },
                new TicketModel
                {
                    TicketNum = 443456443,
                    Subject = "SANIFICAZIONE",
                    Priority = "High",
                    State = "In Progress",
                    CreationDate = new DateTime(2023, 5, 18, 8, 12, 0),
                    LastModificationDate = new DateTime(2023, 5, 18, 19, 18, 0)
                },
                 new TicketModel
                {
                    TicketNum = 654321,
                    Subject = "Ticket 1",
                    Priority = "Normal",
                    State = "Open",
                    CreationDate = new DateTime(2023, 6, 1, 10, 0, 0),
                    LastModificationDate = new DateTime(2023, 6, 1, 10, 0, 0)
                },
                new TicketModel
                {
                    TicketNum = 654322,
                    Subject = "Ticket 2",
                    Priority = "Low",
                    State = "Open",
                    CreationDate = new DateTime(2023, 6, 2, 12, 0, 0),
                    LastModificationDate = new DateTime(2023, 6, 2, 12, 0, 0)
                }
            };
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
