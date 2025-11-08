namespace FootTrack.Models
{
    public class TrenerObdobje
    {
        public int Id { get; set; }
        public int TrenerId { get; set; }
        public Trener Trener { get; set; } = null!;

        public int EkipaId { get; set; }
        public Ekipa Ekipa { get; set; } = null!;

        public DateTime Od { get; set; }
        public DateTime? Do { get; set; }
    }
}
