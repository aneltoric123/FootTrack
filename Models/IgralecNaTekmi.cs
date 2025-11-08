namespace FootTrack.Models
{
    public class IgralecNaTekmi
    {
        public int IgralecId { get; set; }
        public Igralec Igralec { get; set; } = null!;

        public int TekmaId { get; set; }
        public Tekma Tekma { get; set; } = null!;

        public bool ZacetnaPostava { get; set; }
        public bool Kapetan { get; set; }
    }
}
