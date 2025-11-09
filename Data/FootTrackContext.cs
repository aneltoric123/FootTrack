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
        public DbSet<SodnikTekma> Sodniki_Tekma { get; set; }
        public DbSet<Trener> Trenerji { get; set; }
        public DbSet<TrenerObdobje> Trenerji_Obdobje { get; set; }
        public DbSet<Krog> Krogi { get; set; }
        public DbSet<Uporabnik> Uporabniki { get; set; }
        public DbSet<Sezona> Sezone { get; set; }
        public DbSet<Tekmovanje> Tekmovanja { get; set; }
        public DbSet<DogodekNaTekmi> Dogodek_Na_Tekmi { get; set; }
        public DbSet<IgralecNaTekmi> Igralec_Na_Tekmi { get; set; }
        public DbSet<EkipaVSezoni> Ekipa_V_Sezoni { get; set; }
protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IgralecNaTekmi>().HasKey(x => new { x.IgralecId, x.TekmaId });
            modelBuilder.Entity<SodnikTekma>().HasKey(x => new { x.TekmaId, x.SodnikId });
            modelBuilder.Entity<DogodekNaTekmi>().HasKey(x => new { x.IgralecId, x.TekmaId, x.St_Dogodka });

            modelBuilder.Entity<SodnikTekma>().HasOne(x => x.Sodnik).WithMany(e => e.SodnikTekme).HasForeignKey(x => x.SodnikId);
            modelBuilder.Entity<SodnikTekma>().HasOne(x => x.Tekma).WithMany(e => e.Sodniki).HasForeignKey(x => x.TekmaId);

            modelBuilder.Entity<DogodekNaTekmi>().HasOne(x => x.Igralec).WithMany(e => e.Dogodki).HasForeignKey(x => x.IgralecId);
            modelBuilder.Entity<DogodekNaTekmi>().HasOne(x => x.Tekma).WithMany(e => e.Dogodki).HasForeignKey(x => x.TekmaId);

            modelBuilder.Entity<Mesto>().HasOne(m => m.Drzava).WithMany(d => d.Mesta).HasForeignKey(m => m.DrzavaId);
            modelBuilder.Entity<Stadion>().HasOne(m => m.Mesto).WithMany(s => s.Stadioni).HasForeignKey(m => m.MestoId);
            modelBuilder.Entity<Tekma>().HasOne(s => s.Stadion).WithMany(t => t.Tekme).HasForeignKey(s => s.StadionId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<TrenerObdobje>().HasKey(x => new { x.TrenerId, x.EkipaId, x.Zacetek });

            modelBuilder.Entity<TrenerObdobje>().HasOne(x => x.Trener).WithMany(t => t.Obdobja).HasForeignKey(x => x.TrenerId);

            modelBuilder.Entity<TrenerObdobje>().HasOne(x => x.Ekipa).WithMany(e => e.TrenerjiObdobja).HasForeignKey(x => x.EkipaId);
            modelBuilder.Entity<Tekma>().HasOne(t => t.Krog).WithMany(k => k.Tekme).HasForeignKey(t => t.KrogId);
            modelBuilder.Entity<Sezona>().HasOne(s => s.Tekmovanje).WithMany(t => t.Sezone).HasForeignKey(s => s.TekmovanjeId);
            modelBuilder.Entity<Krog>().HasOne(k => k.Sezona).WithMany(s => s.Krogi).HasForeignKey(k => k.SezonaId);
            modelBuilder.Entity<EkipaVSezoni>().HasKey(x => new { x.EkipaId, x.SezonaId });

            modelBuilder.Entity<EkipaVSezoni>().HasOne(es => es.Ekipa).WithMany(e => e.Sezone).HasForeignKey(es => es.EkipaId).OnDelete(DeleteBehavior.Restrict);;

            modelBuilder.Entity<EkipaVSezoni>().HasOne(es => es.Sezona).WithMany(s => s.Ekipe).HasForeignKey(es => es.SezonaId).OnDelete(DeleteBehavior.Cascade);;

            modelBuilder.Entity<IgralecNaTekmi>().HasOne(x => x.Igralec).WithMany(i => i.Tekme).HasForeignKey(x => x.IgralecId);

            modelBuilder.Entity<IgralecNaTekmi>().HasOne(x => x.Tekma).WithMany(t => t.Igralci).HasForeignKey(x => x.TekmaId);

            modelBuilder.Entity<Tekma>().HasOne(t => t.DomacaEkipa).WithMany(e => e.DomaceTekme).HasForeignKey(t => t.DomacaEkipaId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Tekma>().HasOne(t => t.GostujocaEkipa).WithMany(e => e.GostTekme).HasForeignKey(t => t.GostujocaEkipaId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Igralec>().HasOne(i => i.Ekipa).WithMany(e => e.Igralci).HasForeignKey(i => i.EkipaId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Igralec>().HasOne(i => i.Drzava).WithMany(d => d.Igralci).HasForeignKey(i => i.DrzavaId).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
