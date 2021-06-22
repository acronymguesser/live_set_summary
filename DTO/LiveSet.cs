namespace LiveSetSummary.DTO
{
    public class LiveSet
    {
        public Track[] Tracks { get; set; }
        public Track MasterTrack { get; set; }
        public decimal? Tempo { get; set; }
    }
}
