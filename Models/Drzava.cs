namespace FootTrack.Models
{
    public class Drzava
    {
        public int DrzavaId { get; set; }
        public string Ime { get; set; } = string.Empty;

        public ICollection<Mesto> Mesta { get; set; } = new List<Mesto>();
        public ICollection<Tekmovanje> Tekmovanja { get; set; } = new List<Tekmovanje>();

        public ICollection<Igralec> Igralci { get; set; } = new List<Igralec>();
            }
}