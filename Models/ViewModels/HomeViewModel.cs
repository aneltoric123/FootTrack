namespace FootTrack.Models.ViewModels
{
    public class HomeViewModel
    {
        public Uporabnik? AppUser { get; set; }
        public Ekipa? NajEkipa { get; set; }
        public List<Tekma>? Tekme { get; set; }
        public string? IzbranoTekmovanje { get; set; }
        public List<Tekmovanje>? Tekmovanja { get; set; }
    }
}
