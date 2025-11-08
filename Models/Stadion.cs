namespace FootTrack.Models
{
    public class Stadion
    {
        public int StadionID { get; set; }
        public string Ime { get; set; }
        public DateTime Datum_Otvoritve { get; set; }
        public int Kapaciteta { get; set; }
        public int MestoId { get; set; }
        public Mesto Mesto { get; set; } = null!;

        public ICollection<Tekma> tekme { get; set; } = new List<Tekma>();
    }
}