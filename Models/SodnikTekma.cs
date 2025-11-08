namespace FootTrack.Models
{
    public class Sodnik_Tekma
    {
        public int TekmaID { get; set; }
        public Tekma Tekma { get; set; } = null!;

        public int SodnikID { get; set; }
        public Sodnik Sodnik { get; set; } = null;

        public string Vloga { get; set; } = string.Empty;
    }
}