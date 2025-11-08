namespace FootTrack.Models
{
    public class EkipaVSezoni
    {
        public int EkipaID { get; set; }
        public Ekipa Ekipa { get; set; } = null!;

        public int SezonaID { get; set; }
        public Sezona Sezona { get; set; } = null!;


        public int Tocke { get; set; }
        public int Zmage { get; set; }
        public int Remi { get; set; }
        public int Porazi { get; set; }
        public int Goli { get; set; }
        public int Prejeti_Goli { get; set; }

        
    }
}