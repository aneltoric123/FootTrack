namespace FootTrack.Models
{
    public class Sodnik
    {
        public int SodnikId { get; set; }
        public string Ime { get; set; } = string.Empty;
        public string Priimek { get; set; } = string.Empty;

        public ICollection<SodnikTekma> SodnikTekme { get; set; } = new List<SodnikTekma>();
    }
}
