namespace FootTrack.Models
{
    public class Mesto
    {
        public int MestoId { get; set; }
        public string Ime { get; set; } = string.Empty;

        public int DrzavaId { get; set; }

        public Drzava Drzava { get; set; } = null!;

        public ICollection<Stadion> Stadioni { get; set; } = new List<Stadion>();
    }
}