namespace FootTrack.Models
{
    public class EkipaVSezoni
    {
        public int EkipaId { get; set; }
        public Ekipa Ekipa { get; set; } = null!;

        public int SezonaId { get; set; }
        public Sezona Sezona { get; set; } = null!;


        public int Tocke { get; set; }
        public int Zmage { get; set; }
        public int Remi { get; set; }
        public int Porazi { get; set; }
        public int Goli { get; set; }
        public int Prejeti_Goli { get; set; }

        
    }
}