namespace FootTrack.Models
{
    public class Stadion
    {
        public int StadionId { get; set; }
        public string Ime { get; set; } = string.Empty;
        public DateTime Datum_Otvoritve { get; set; }
        public int Kapaciteta { get; set; }
        public int MestoId { get; set; }
        public Mesto Mesto { get; set; } = null!;

        public ICollection<Tekma> Tekme { get; set; } = new List<Tekma>();
    }
}