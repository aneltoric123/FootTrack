namespace FootTrack.Models
{
    public class Drzava
    {
        public int DrzavaID { get; set; }
        public string Ime { get; set; }

        public ICollection<Mesto> Mesta { get; set; } = new List<Mesto>();
        public ICollection<Tekmovanje> Tekmovanja { get; set; } = new List<Tekmovanje>();
            }
}