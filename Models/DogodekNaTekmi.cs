namespace FootTrack.Models
{
    public class DogodekNaTekmi
    {
        public int St_Dogodka { get; set; }
        public string TipDogodka { get; set; } = string.Empty;
        public int Minuta { get; set; }

        public int TekmaId { get; set; }
        public Tekma Tekma { get; set; } = null!;

        public int? IgralecId { get; set; }
        public Igralec? Igralec { get; set; }
    }
}
