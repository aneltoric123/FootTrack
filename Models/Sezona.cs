namespace FootTrack.Models
{
    public class Sezona
    {
        public int Id { get; set; }
        public string Leto { get; set; } = string.Empty;

        public int TekmovanjeId { get; set; }
        public Tekmovanje Tekmovanje { get; set; } = null!;

        public ICollection<Krog> Krogi { get; set; } = new List<Krog>();
    }
}
