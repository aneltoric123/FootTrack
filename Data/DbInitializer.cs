using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FootTrack.Models;

namespace FootTrack.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(FootTrackContext context, UserManager<Uporabnik> userManager)
        {
            context.Database.Migrate();

           
            if (!context.Drzave.Any())
            {
                var drzave = new List<Drzava>
                {
                    new Drzava { Ime = "Slovenija" },
                    new Drzava { Ime = "Anglija" },
                    new Drzava { Ime = "Nemčija" }
                };
                context.Drzave.AddRange(drzave);
                await context.SaveChangesAsync();
            }

            // --- Seed Cities ---
            if (!context.Mesta.Any())
            {
                var slovenija = context.Drzave.First(d => d.Ime == "Slovenija");
                var ang = context.Drzave.First(d => d.Ime == "Anglija");
                var nem = context.Drzave.First(d => d.Ime == "Nemčija");

                var mesta = new List<Mesto>
                {
                    new Mesto { Ime = "Ljubljana", DrzavaId = slovenija.DrzavaId },
                    new Mesto { Ime = "London", DrzavaId = ang.DrzavaId },
                    new Mesto { Ime = "Berlin", DrzavaId = nem.DrzavaId }
                };
                context.Mesta.AddRange(mesta);
                await context.SaveChangesAsync();
            }

            // --- Seed Stadions ---
            if (!context.Stadioni.Any())
            {
                var lj = context.Mesta.First(m => m.Ime == "Ljubljana");
                var lon = context.Mesta.First(m => m.Ime == "London");
                var ber = context.Mesta.First(m => m.Ime == "Berlin");

                var stadioni = new List<Stadion>
                {
                    new Stadion { Ime = "Stadion Stožice",Datum_Otvoritve = new DateTime(1970, 7, 15),Kapaciteta = 15000,MestoId = lj.MestoId },
                    new Stadion { Ime = "Wembley", Datum_Otvoritve = new DateTime(1930, 1, 1),Kapaciteta = 80000,MestoId = lon.MestoId },
                    new Stadion { Ime = "Olympiastadion",Datum_Otvoritve= new DateTime(2001, 3, 12) ,Kapaciteta = 100000,MestoId = ber.MestoId }
                };
                context.Stadioni.AddRange(stadioni);
                await context.SaveChangesAsync();
            }

            // --- Seed Teams ---
            if (!context.Ekipe.Any())
        {
                var st1 = context.Stadioni.First(s => s.Ime == "Stadion Stožice");
                var st2 = context.Stadioni.First(s => s.Ime == "Wembley");
                var st3 = context.Stadioni.First(s => s.Ime == "Olympiastadion");

                var ekipe = new List<Ekipa>
    {
        new Ekipa { Ime = "NK Olimpija Ljubljana", StadionId = st1.StadionId },
        new Ekipa { Ime = "Manchester United", StadionId = st2.StadionId },
        new Ekipa { Ime = "Bayern Munich", StadionId = st3.StadionId }
    };
    context.Ekipe.AddRange(ekipe);
    await context.SaveChangesAsync();
}

            // --- Seed Players ---
            if (!context.Igralci.Any())
            {
                var slovenija = context.Drzave.First(d => d.Ime == "Slovenija");
                var ang = context.Drzave.First(d => d.Ime == "Anglija");
                var nem = context.Drzave.First(d => d.Ime == "Nemčija");

                var olimpija = context.Ekipe.First(e => e.Ime == "NK Olimpija Ljubljana");
                var manu = context.Ekipe.First(e => e.Ime == "Manchester United");
                var bayern = context.Ekipe.First(e => e.Ime == "Bayern Munich");

                var igralci = new List<Igralec>
                {
                    new Igralec { Ime = "Jan Oblak", EkipaId = olimpija.EkipaId, DrzavaId = slovenija.DrzavaId, Pozicija = "Goalkeeper" },
                    new Igralec { Ime = "Benjamin Šeško", EkipaId = olimpija.EkipaId, DrzavaId = slovenija.DrzavaId, Pozicija = "Forward" },
                    new Igralec { Ime = "Cristiano Ronaldo", EkipaId = manu.EkipaId, DrzavaId = ang.DrzavaId, Pozicija = "Forward" },
                    new Igralec { Ime = "Bruno Fernandes", EkipaId = manu.EkipaId, DrzavaId = ang.DrzavaId, Pozicija = "Midfielder" },
                    new Igralec { Ime = "Harry Kane", EkipaId = bayern.EkipaId, DrzavaId = nem.DrzavaId, Pozicija = "Forward" },
                    new Igralec { Ime = "Joshua Kimmich", EkipaId = bayern.EkipaId, DrzavaId = nem.DrzavaId, Pozicija = "Midfielder" }
                };
                context.Igralci.AddRange(igralci);
                await context.SaveChangesAsync();
            }

            // --- Seed Referees ---
            if (!context.Sodniki.Any())
            {
                var sodniki = new List<Sodnik>
                {
                    new Sodnik { Ime = "Damir", Priimek = "Skomina" },
                    new Sodnik { Ime = "Howard",Priimek = "Webb",}
                };
                context.Sodniki.AddRange(sodniki);
                await context.SaveChangesAsync();
            }

            // --- Seed Competitions and Seasons ---
            if (!context.Tekmovanja.Any())
            {
                var slo = context.Drzave.FirstOrDefault(d => d.Ime == "Slovenija");
                var eng = context.Drzave.FirstOrDefault(d => d.Ime == "Anglija");
                var ger = context.Drzave.FirstOrDefault(d => d.Ime == "Nemčija");
                var tekmovanja = new List<Tekmovanje>
                {
                    new Tekmovanje { Ime = "Premier League",DrzavaId=eng.DrzavaId },
                    new Tekmovanje { Ime = "Bundesliga",DrzavaId=ger.DrzavaId },
                    new Tekmovanje { Ime = "Slovenian Prva Liga",DrzavaId=slo.DrzavaId }
                };
                context.Tekmovanja.AddRange(tekmovanja);
                await context.SaveChangesAsync();

                foreach (var t in tekmovanja)
                {
                    var sezona = new Sezona { Leto = $"{t.Ime} 2025/26", TekmovanjeId = t.TekmovanjeId };
                    context.Sezone.Add(sezona);
                }
                await context.SaveChangesAsync();
            }

            // --- Seed Rounds ---
            if (!context.Krogi.Any())
            {
                foreach (var sez in context.Sezone)
                {
                    context.Krogi.AddRange(
                        new Krog { Stevilka = 1, SezonaId = sez.Id },
                        new Krog { Stevilka = 2, SezonaId = sez.Id },
                        new Krog { Stevilka = 3, SezonaId = sez.Id }
                    );
                }
                await context.SaveChangesAsync();
            }

            // --- Seed Matches and Events ---
            if (!context.Tekme.Any())
            {
                var stadion = context.Stadioni.First();
                var ekipe = context.Ekipe.ToList();
                var krogi = context.Krogi.ToList();

                var tekme = new List<Tekma>
                {
                    new Tekma
                    {
                        Datum = DateTime.Now.AddDays(-1),
                        StadionId = stadion.StadionId,
                        DomacaEkipaId = ekipe[0].EkipaId,
                        GostujocaEkipaId = ekipe[1].EkipaId,
                        KrogId = krogi[0].KrogId,
                        GoliDomaci = 2,
                        GoliGosti = 1
                    },
                    new Tekma
                    {
                        Datum = DateTime.Now,
                        StadionId = stadion.StadionId,
                        DomacaEkipaId = ekipe[1].EkipaId,
                        GostujocaEkipaId = ekipe[2].EkipaId,
                        KrogId = krogi[1].KrogId,
                        GoliDomaci = 3,
                        GoliGosti = 2
                    },
                    new Tekma
                    {
                        Datum = DateTime.Now.AddDays(2),
                        StadionId = stadion.StadionId,
                        DomacaEkipaId = ekipe[2].EkipaId,
                        GostujocaEkipaId = ekipe[0].EkipaId,
                        KrogId = krogi[2].KrogId},
                };

                context.Tekme.AddRange(tekme);
                await context.SaveChangesAsync();

                // --- Events on matches ---
                var igralci = context.Igralci.ToList();
                var dogodki = new List<DogodekNaTekmi>
                {
                    new DogodekNaTekmi { IgralecId = igralci[1].IgralecId, TekmaId = tekme[0].TekmaId, St_Dogodka = 1, TipDogodka = "Goal" },
                    new DogodekNaTekmi { IgralecId = igralci[2].IgralecId, TekmaId = tekme[0].TekmaId, St_Dogodka = 2, TipDogodka = "Goal" },
                    new DogodekNaTekmi { IgralecId = igralci[4].IgralecId, TekmaId = tekme[1].TekmaId, St_Dogodka = 1, TipDogodka = "Goal" },
                    new DogodekNaTekmi { IgralecId = igralci[5].IgralecId, TekmaId = tekme[1].TekmaId, St_Dogodka = 2, TipDogodka = "Goal" },
                };
                context.Dogodek_Na_Tekmi.AddRange(dogodki);
                await context.SaveChangesAsync();
            }
        }
    }
}
