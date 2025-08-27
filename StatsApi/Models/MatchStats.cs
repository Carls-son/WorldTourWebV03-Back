namespace StatsApi.Models
{
    public class MatchStats
    {
        public int Id { get; set; }  // Maybe Add this later so other people can track their own stats?
        public string Stadium { get; set; } = string.Empty;
        public string Class { get; set; } = string.Empty;
        public string Placement { get; set; } = string.Empty;
        public int Elims { get; set; }
        public int Assists { get; set; }
        public int Deaths { get; set; }
        public int Revives { get; set; }
    }
}
