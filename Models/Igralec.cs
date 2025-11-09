namespace FootTrack.Models
{
    public class Igralec
    {
        public int IgralecId { get; set; }
        public string Ime { get; set; } = string.Empty;
        public string Priimek { get; set; } = string.Empty;
        public DateOnly Datum_Rojstva { get; set; }
        public string Pozicija { get; set; } = string.Empty;
        public int EkipaId { get; set; }
        public Ekipa Ekipa { get; set; } = null!;

        public int DrzavaId { get; set; }
        public Drzava Drzava { get; set; } = null!;

        public ICollection<IgralecNaTekmi> Tekme { get; set; } = new List<IgralecNaTekmi>();
        public ICollection<DogodekNaTekmi> Dogodki { get; set; } = new List<DogodekNaTekmi>();
    }
}