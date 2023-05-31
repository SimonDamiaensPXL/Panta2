namespace Panta2.Core.Models.Ticket
{
    public class TicketModel
    {
        public int TicketNum { get; set; }
        public string Subject { get; set; }
        public string Priority { get; set; }
        public string State { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModificationDate { get; set; }
    }
}
