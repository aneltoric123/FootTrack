namespace FootTrack.Models
{
    public class Igralec
    {
        public int IgralecID { get; set; }
        public string Ime { get; set; }
        public string Priimek { get; set; }
        public DateOnly Datum_Rojstva { get; set; }
        public int PozicijaId { get; set; }
        public Pozicija Pozicija { get; set; } = null!;

        public int EkipaId { get; set; }
        public Ekipa Ekipa { get; set; } = null!;

        public ICollection<IgralecNaTekmi> Tekme { get; set; } = new List<IgralecNaTekmi>();
    }
}