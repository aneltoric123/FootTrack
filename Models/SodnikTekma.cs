namespace FootTrack.Models
{
    public class SodnikTekma
    {
        public int TekmaId { get; set; }
        public Tekma Tekma { get; set; } = null!;

        public int SodnikId { get; set; }
        public Sodnik Sodnik { get; set; } = null;

        public string Vloga { get; set; } = string.Empty;
    }
}