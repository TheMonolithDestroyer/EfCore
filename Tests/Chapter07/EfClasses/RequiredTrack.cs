using Tests.Chapter07.Enums;

namespace Tests.Chapter07.EfClasses
{
    public class RequiredTrack
    {
        public int RequiredTrackId { get; set; }
        public TrackNamesEnum Track { get; set; }
        public Attendee Attend { get; set; }
    }
}