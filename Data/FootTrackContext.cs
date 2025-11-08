using Microsoft.EntityFrameworkCore;
using FootTrack.Models;

namespace FootTrack.Data
{
    public class FootTrackContext : DbContext
    {
        public FootTrackContext(DbContextOptions<FootTrackContext> options) : base(options) {}

        public DbSet<Ekipa> Ekipe { get; set; }
        public DbSet<Igralec> Igralci { get; set; }
        public DbSet<Tekma> Tekme { get; set; }
        public DbSet<Drzava> Drzave { get; set; }
        public DbSet<Stadion> Stadioni { get; set; }
        public DbSet<Mesto> Mesta { get; set; }
        public DbSet<Sodnik> Sodniki { get; set; }
        public DbSet<Pozicija> Pozicije { get; set; }
        public DbSet<SodnikTekma> Sodniki_Tekma { get; set; }
        public DbSet<Trener> Trenerji { get; set; }
        public DbSet<TrenerObdobje> Trenerji_Obdobje { get; set; }
        public DbSet<Krog> Krogi { get; set; }
        public DbSet<Uporabnik> Uporabniki { get; set; }
        public DbSet<Sezona> Sezone { get; set; }
        public DbSet<Tekmovanje> Tekmovanja { get; set; }
        public DbSet<DogodekNaTekmi> Dogodek_Na_Tekmi { get; set; }
        public DbSet<IgralecNaTekmi> Igralec_Na_Tekmi { get; set; }
        public DBSet<EkipaVSezoni> Ekipa_V_Sezoni { get; set; }

    }
}
