namespace FootTrack.Models
{
    public class Tekma
    {
        public int TekmaId { get; set; }
        public DateTime Datum { get; set; }
        public int GoliDomaci { get; set; }
        public int GoliGosti { get; set; }

        public int DomacaEkipaId { get; set; }
        public Ekipa DomacaEkipa { get; set; } = null!;

        public int GostujocaEkipaId { get; set; }
        public Ekipa GostujocaEkipa { get; set; } = null!;

        public int StadionId { get; set; }
        public Stadion Stadion { get; set; } = null!;

        public int KrogId { get; set; }
        public Krog Krog { get; set; } = null!;

        public ICollection<IgralecNaTekmi> Igralci { get; set; } = new List<IgralecNaTekmi>();
        public ICollection<DogodekNaTekmi> Dogodki { get; set; } = new List<DogodekNaTekmi>();
    }
}
