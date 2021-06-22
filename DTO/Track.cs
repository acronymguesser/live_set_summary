namespace LiveSetSummary.DTO
{
    public class Track
    {
        public TrackType Type { get; set; }
        public string Name { get; set; }
        public TrackDevice[] Devices { get; set; }
    }
}
