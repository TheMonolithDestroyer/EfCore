using Tests.Chapter07.Enums;

namespace Tests.Chapter07.EfClasses
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public TicketTypesEnum TicketType { get; set; }

        public Attendee Attendee { get; set; }
    }
}