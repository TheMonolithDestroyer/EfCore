namespace Tests.Chapter07.EfClasses
{
    public class Attendee
    {
        public int AttendeeId { get; set; }
        public string Name { get; set; }


        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }

        public OptionalTrack Optional { get; set; }
        public RequiredTrack Required { get; set; }
    }
}