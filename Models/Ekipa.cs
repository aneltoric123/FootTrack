namespace FootTrack.Models
{
    public class Ekipa
    {
        public int EkipaID { get; set; }
        public string Ime { get; set; }
        public string Kratica { get; set; }
        public DateOnly Datum_Ustanovitve { get; set; }
        
        public int StadionId { get; set; }
        public Stadion Stadion { get; set; } = null!;
        public ICollection<Igralec> Igralci { get; set; } = new List<Igralec>();
        public ICollection<Tekma> DomaceTekme { get; set; } = new List<Tekma>();
        public ICollection<Tekma> GostTekme { get; set; } = new List<Tekma>();
        public ICollection<TrenerObdobje> Trenerji { get; set; } = new List<TrenerObdobje>();
            
            }
} 