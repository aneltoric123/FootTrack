namespace FootTrack.Models
{
    public class Uporabnik
    {
        public int UserId { get; set; }
        public string UporabniskoIme { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string GesloHash { get; set; } = string.Empty;

        public int? NajljubsaEkipaId { get; set; }
        public Ekipa? NajljubsaEkipa { get; set; }

        public int? NajljubseTekmovanjeId { get; set; }
        public Tekmovanje? NajljubseTekmovanje { get; set; }
    }
}
