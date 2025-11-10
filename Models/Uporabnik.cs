using Microsoft.AspNetCore.Identity;
namespace FootTrack.Models
{
    public class Uporabnik : IdentityUser
    {
        public string UporabniskoIme { get; set; } = string.Empty;

        public int? NajljubsaEkipaId { get; set; }
        public Ekipa? NajljubsaEkipa { get; set; }

        public ICollection<Tekmovanje> Naj_Tekmovanja { get; set; } = new List<Tekmovanje>();
    }
}
