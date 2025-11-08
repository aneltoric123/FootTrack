namespace FootTrack.Models
{
    public class Sodnik
    {
        public int Id { get; set; }
        public string Ime { get; set; } = string.Empty;
        public string Priimek { get; set; } = string.Empty;

        public ICollection<Sodnik_Tekma> Tekme { get; set; } = new List<Sodnik_Tekma>();
    }
}
