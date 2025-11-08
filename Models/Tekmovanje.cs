namespace FootTrack.Models
{
    public class Tekmovanje
    {
        public int TekmvovanjeId { get; set; }
        public string Ime { get; set; } = string.Empty;

        public int DrzavaId { get; set; }
        public Drzava Drzava { get; set; } = null!;

        public ICollection<Sezona> Sezone { get; set; } = new List<Sezona>();
    }
}
