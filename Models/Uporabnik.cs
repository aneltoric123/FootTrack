namespace FootTrack.Models
{
    public class Uporabnik
    {
        public int UporabnikId { get; set; }
        public string UporabniskoIme { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string GesloHash { get; set; } = string.Empty;

        public int? NajljubsaEkipaId { get; set; }
        public Ekipa? NajljubsaEkipa { get; set; }

        public ICollection<Tekmovanje> Naj_Tekmovanja { get; set; } = new List<Tekmovanje>();
    }
}
