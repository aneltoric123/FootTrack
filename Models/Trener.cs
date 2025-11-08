namespace FootTrack.Models
{
    public class Trener
    {
        public int Id { get; set; }
        public string Ime { get; set; } = string.Empty;
        public string Priimek { get; set; } = string.Empty;
        public string Formacija { get; set; } = string.Empty;

        public ICollection<TrenerObdobje> Obdobja { get; set; } = new List<TrenerObdobje>();
    }
}
