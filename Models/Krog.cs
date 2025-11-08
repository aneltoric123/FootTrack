namespace FootTrack.Models
{
    public class Krog
    {
        public int KrogId { get; set; }
        public int Stevilka { get; set; }

        public int SezonaId { get; set; }
        public Sezona Sezona { get; set; } = null!;

        public ICollection<Tekma> Tekme { get; set; } = new List<Tekma>();
    }
}
