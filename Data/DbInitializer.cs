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
                    new Drzava { Ime = "Slovenia" },
                    new Drzava { Ime = "England" },
                    new Drzava { Ime = "Germany" }
                };
                context.Drzave.AddRange(drzave);
                await context.SaveChangesAsync();
            }

            // --- Seed Cities ---
            if (!context.Mesta.Any())
            {
                var slovenija = context.Drzave.First(d => d.Ime == "Slovenia");
                var ang = context.Drzave.First(d => d.Ime == "England");
                var nem = context.Drzave.First(d => d.Ime == "Germany");

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
                var slovenija = context.Drzave.First(d => d.Ime == "Slovenia");
                var ang = context.Drzave.First(d => d.Ime == "England");
                var nem = context.Drzave.First(d => d.Ime == "Germany");

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
                var slo = context.Drzave.FirstOrDefault(d => d.Ime == "Slovenia");
                var eng = context.Drzave.FirstOrDefault(d => d.Ime == "England");
                var ger = context.Drzave.FirstOrDefault(d => d.Ime == "Germany");
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
    new DogodekNaTekmi { IgralecId = igralci[1].IgralecId, TekmaId = tekme[0].TekmaId, St_Dogodka = 1, Minuta = 30, TipDogodka = "Goal" },
    new DogodekNaTekmi { IgralecId = igralci[2].IgralecId, TekmaId = tekme[0].TekmaId, St_Dogodka = 2, Minuta = 54, TipDogodka = "Goal" },
    new DogodekNaTekmi { IgralecId = igralci[4].IgralecId, TekmaId = tekme[1].TekmaId, St_Dogodka = 1, Minuta = 2, TipDogodka = "Goal" },
    new DogodekNaTekmi { IgralecId = igralci[5].IgralecId, TekmaId = tekme[1].TekmaId, St_Dogodka = 2, Minuta = 78, TipDogodka = "Goal" },
};

foreach (var d in dogodki)
{
    // check if event already exists
    if (!context.Dogodek_Na_Tekmi.Any(x => x.IgralecId == d.IgralecId && x.TekmaId == d.TekmaId && x.St_Dogodka == d.St_Dogodka))
    {
        context.Dogodek_Na_Tekmi.Add(d);
    }
}
                await context.SaveChangesAsync();
            }
            // --- Seed More Teams ---
if (!context.Ekipe.Any(e => e.Ime.Contains("Real") || e.Ime.Contains("Barcelona")))
{
    var lj = context.Stadioni.First(s => s.Ime == "Stadion Stožice");
    var wembley = context.Stadioni.First(s => s.Ime == "Wembley");
    var olympia = context.Stadioni.First(s => s.Ime == "Olympiastadion");

    var additionalTeams = new List<Ekipa>
    {
        new Ekipa { Ime = "Real Madrid", StadionId = olympia.StadionId },
        new Ekipa { Ime = "FC Barcelona", StadionId = olympia.StadionId },
        new Ekipa { Ime = "Liverpool", StadionId = wembley.StadionId },
        new Ekipa { Ime = "Chelsea", StadionId = wembley.StadionId },
        new Ekipa { Ime = "Maribor", StadionId = lj.StadionId }
    };
    context.Ekipe.AddRange(additionalTeams);
    await context.SaveChangesAsync();
}

// --- Seed More Players ---
if (!context.Igralci.Any(i => i.Ime.Contains("Messi") || i.Ime.Contains("Modric")))
{
    var ekipe = context.Ekipe.ToList();
    var drzave = context.Drzave.ToList();

    var additionalPlayers = new List<Igralec>
    {
        new Igralec { Ime = "Lionel Messi", Priimek = "Messi", EkipaId = ekipe.First(e => e.Ime=="FC Barcelona").EkipaId, DrzavaId = drzave.First(d=>d.Ime=="Germany").DrzavaId, Pozicija="Forward" },
        new Igralec { Ime = "Luka Modric", Priimek = "Modric", EkipaId = ekipe.First(e => e.Ime=="Real Madrid").EkipaId, DrzavaId = drzave.First(d=>d.Ime=="Slovenia").DrzavaId, Pozicija="Midfielder" },
        new Igralec { Ime = "Mohamed Salah", Priimek = "Salah", EkipaId = ekipe.First(e => e.Ime=="Liverpool").EkipaId, DrzavaId = drzave.First(d=>d.Ime=="England").DrzavaId, Pozicija="Forward" },
        new Igralec { Ime = "N'Golo", Priimek = "Kante", EkipaId = ekipe.First(e => e.Ime=="Chelsea").EkipaId, DrzavaId = drzave.First(d=>d.Ime=="England").DrzavaId, Pozicija="Midfielder" },
        new Igralec { Ime = "Nejc", Priimek = "Skubic", EkipaId = ekipe.First(e => e.Ime=="Maribor").EkipaId, DrzavaId = drzave.First(d=>d.Ime=="Slovenia").DrzavaId, Pozicija="Defender" }
    };
    context.Igralci.AddRange(additionalPlayers);
    await context.SaveChangesAsync();
}

// --- Seed More Matches ---
if (!context.Tekme.Any(t => t.Datum.Year == 2025))
{
    var ekipe = context.Ekipe.ToList();
    var krogi = context.Krogi.ToList();
    var stadioni = context.Stadioni.ToList();
    var matches = new List<Tekma>();

    foreach (var doma in ekipe)
    {
        foreach (var gost in ekipe.Where(e => e.EkipaId != doma.EkipaId))
        {
            matches.Add(new Tekma
            {
                Datum = DateTime.Now.AddDays(new Random().Next(-10, 30)),
                DomacaEkipaId = doma.EkipaId,
                GostujocaEkipaId = gost.EkipaId,
                KrogId = krogi[new Random().Next(krogi.Count)].KrogId,
                StadionId = stadioni[new Random().Next(stadioni.Count)].StadionId,
                GoliDomaci = new Random().Next(0, 5),
                GoliGosti = new Random().Next(0, 5)
            });
        }
    }
    context.Tekme.AddRange(matches);
    await context.SaveChangesAsync();
}

// --- Seed Match Events (Goals, Cards) ---
// --- Seed Match Events (Goals, Cards) ---
var igralciAll = context.Igralci.ToList();
var tekmeAll = context.Tekme.ToList();
var events = new List<DogodekNaTekmi>();
var rnd = new Random();

foreach (var tekma in tekmeAll)
{
    var domaciIgralci = igralciAll.Where(i => i.EkipaId == tekma.DomacaEkipaId).ToList();
        var gostIgralci = igralciAll.Where(i => i.EkipaId == tekma.GostujocaEkipaId).ToList();

    // Random 3-5 events per match
    int eventCount = rnd.Next(3, 6);
    for (int i = 0; i < eventCount; i++)
    {
        var isGoal = rnd.Next(0, 2) == 0;
        var igralec = (i % 2 == 0) ? domaciIgralci[rnd.Next(domaciIgralci.Count)] : gostIgralci[rnd.Next(gostIgralci.Count)];

        var stDogodka = i + 1;

        // Only add if this event does not already exist
        if (!context.Dogodek_Na_Tekmi.Any(x =>
                x.TekmaId == tekma.TekmaId &&
                x.IgralecId == igralec.IgralecId &&
                x.St_Dogodka == stDogodka))
        {
            events.Add(new DogodekNaTekmi
            {
                TekmaId = tekma.TekmaId,
                IgralecId = igralec.IgralecId,
                St_Dogodka = stDogodka,
                Minuta = rnd.Next(1, 91),
                TipDogodka = isGoal ? "Goal" : (rnd.Next(0, 2) == 0 ? "Yellow Card" : "Red Card")
            });
        }
    }
}

context.Dogodek_Na_Tekmi.AddRange(events);
await context.SaveChangesAsync();


        }
    }
}
