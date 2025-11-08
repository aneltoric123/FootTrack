namespace FootTrack.Models
{
    public class Mesto
    {
        public int MestoID { get; set; }
        public string Ime { get; set; }

        public int DrzavaID { get; set; }

        public Drzava Drzava { get; set; } = null!;
    }
}