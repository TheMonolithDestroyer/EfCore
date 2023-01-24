using Tests.Chapter07.Enums;

namespace Tests.Chapter07.EfClasses
{
    public class OptionalTrack
    {
        public int OptionalTrackId { get; set; }
        public TrackNamesEnum Track { get; set; }
        public Ticket Ticket { get; set; }
    }
}