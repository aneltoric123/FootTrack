using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FootTrack.Models;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using DotNetEnv;

namespace FootTrack.Data
{
    public static class DbInitializer
    {
        public static async Task SeedRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            if(!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
        }
    

        public static async Task Initialize(FootTrackContext context, UserManager<Uporabnik> userManager)
        {
            context.Database.Migrate();
            var rnd = new Random();
            if (!context.Drzave.Any())
            {
                var drzave = new List<Drzava>
                {
                    new Drzava { Ime = "Slovenia" },
                    new Drzava { Ime = "England" },
                    new Drzava { Ime = "Germany" },
                    new Drzava { Ime = "Spain" },
                    new Drzava { Ime = "Italy" }
                };
                context.Drzave.AddRange(drzave);
                await context.SaveChangesAsync();
            }

            if (!context.Mesta.Any())
            {
                var drzave = context.Drzave.AsNoTracking().ToList();
                var mesta = new List<Mesto>
                {
                    new Mesto { Ime = "Ljubljana", DrzavaId = drzave.First(d => d.Ime=="Slovenia").DrzavaId },
                    new Mesto { Ime = "London", DrzavaId = drzave.First(d => d.Ime=="England").DrzavaId },
                    new Mesto { Ime = "Berlin", DrzavaId = drzave.First(d => d.Ime=="Germany").DrzavaId },
                    new Mesto { Ime = "Madrid", DrzavaId = drzave.First(d => d.Ime=="Spain").DrzavaId },
                    new Mesto { Ime = "Rome", DrzavaId = drzave.First(d => d.Ime=="Italy").DrzavaId }
                };
                context.Mesta.AddRange(mesta);
                await context.SaveChangesAsync();
            }

            
            if (!context.Stadioni.Any())
            {
                var mesta = context.Mesta.AsNoTracking().ToList();
                var stadioni = new List<Stadion>
                {
                    new Stadion { Ime = "Stadion Stožice", Datum_Otvoritve = new DateTime(1970, 7, 15), Kapaciteta = 15000, MestoId = mesta.First(m=>m.Ime=="Ljubljana").MestoId },
                    new Stadion { Ime = "Wembley", Datum_Otvoritve = new DateTime(1930,1,1), Kapaciteta = 80000, MestoId = mesta.First(m=>m.Ime=="London").MestoId },
                    new Stadion { Ime = "Olympiastadion", Datum_Otvoritve = new DateTime(2001,3,12), Kapaciteta = 100000, MestoId = mesta.First(m=>m.Ime=="Berlin").MestoId },
                    new Stadion { Ime = "Santiago Bernabeu", Datum_Otvoritve = new DateTime(1947, 12, 14), Kapaciteta = 81000, MestoId = mesta.First(m=>m.Ime=="Madrid").MestoId },
                    new Stadion { Ime = "Stadio Olimpico", Datum_Otvoritve = new DateTime(1937, 5, 1), Kapaciteta = 72000, MestoId = mesta.First(m=>m.Ime=="Rome").MestoId }
                };
                context.Stadioni.AddRange(stadioni);
                await context.SaveChangesAsync();
            }

            if (!context.Tekmovanja.Any())
            {
                var drzave = context.Drzave.AsNoTracking().ToList();
                var tekmovanja = new List<Tekmovanje>
                {
                    new Tekmovanje { Ime="Slovenian Prva Liga", DrzavaId = drzave.First(d=>d.Ime=="Slovenia").DrzavaId },
                    new Tekmovanje { Ime="Premier League", DrzavaId = drzave.First(d=>d.Ime=="England").DrzavaId },
                    new Tekmovanje { Ime="Bundesliga", DrzavaId = drzave.First(d=>d.Ime=="Germany").DrzavaId },
                    new Tekmovanje { Ime="La Liga", DrzavaId = drzave.First(d=>d.Ime=="Spain").DrzavaId },
                    new Tekmovanje { Ime="Serie A", DrzavaId = drzave.First(d=>d.Ime=="Italy").DrzavaId }
                };
                context.Tekmovanja.AddRange(tekmovanja);
                await context.SaveChangesAsync();


                foreach(var t in tekmovanja)
                {
                    for(int year = 2023; year <= 2025; year++)
                    {
                        context.Sezone.Add(new Sezona
                        {
                            Leto = $"{year}/{year+1}",
                            TekmovanjeId = t.TekmovanjeId
                        });
                    }
                }
                await context.SaveChangesAsync();
            }

            if (!context.Ekipe.Any())
            {
                var stadioni = context.Stadioni.AsNoTracking().ToList();
                var ekipe = new List<Ekipa>
                {
                    new Ekipa { Ime="NK Olimpija Ljubljana", StadionId = stadioni.First(s=>s.Ime=="Stadion Stožice").StadionId },
                    new Ekipa { Ime="Maribor", StadionId = stadioni.First(s=>s.Ime=="Stadion Stožice").StadionId },
                    new Ekipa { Ime="Manchester United", StadionId = stadioni.First(s=>s.Ime=="Wembley").StadionId },
                    new Ekipa { Ime="Chelsea", StadionId = stadioni.First(s=>s.Ime=="Wembley").StadionId },
                    new Ekipa { Ime="Bayern Munich", StadionId = stadioni.First(s=>s.Ime=="Olympiastadion").StadionId },
                    new Ekipa { Ime="Real Madrid", StadionId = stadioni.First(s=>s.Ime=="Santiago Bernabeu").StadionId },
                    new Ekipa { Ime="FC Barcelona", StadionId = stadioni.First(s=>s.Ime=="Santiago Bernabeu").StadionId },
                    new Ekipa { Ime="Juventus", StadionId = stadioni.First(s=>s.Ime=="Stadio Olimpico").StadionId },
                    new Ekipa { Ime="AC Milan", StadionId = stadioni.First(s=>s.Ime=="Stadio Olimpico").StadionId }
                };
                context.Ekipe.AddRange(ekipe);
                await context.SaveChangesAsync();
            }

            if (!context.Krogi.Any())
            {
                var allSeasons = context.Sezone.AsNoTracking().ToList();
                foreach (var season in allSeasons)
                {
                    for(int i=1;i<=5;i++)
                    {
                        context.Krogi.Add(new Krog { Stevilka = i, SezonaId = season.Id });
                    }
                }
                await context.SaveChangesAsync();
            }

            if (!context.Tekme.Any())
            {
                var allKrogi = await context.Krogi.Include(k=>k.Sezona).ToListAsync();
                var allEkipe = context.Ekipe.AsNoTracking().ToList();
                var allStadioni = context.Stadioni.AsNoTracking().ToList();

                foreach(var krog in allKrogi)
                {
                    var teams = allEkipe.OrderBy(x=>rnd.Next()).Take(4).ToList();
                    for(int i=0;i<teams.Count-1;i+=2)
                    {
                        context.Tekme.Add(new Tekma
                        {
                            KrogId = krog.KrogId,
                            DomacaEkipaId = teams[i].EkipaId,
                            GostujocaEkipaId = teams[i+1].EkipaId,
                            StadionId = allStadioni[rnd.Next(allStadioni.Count)].StadionId,
                            Datum = DateTime.Now.AddDays(rnd.Next(-10,10)),
                            GoliDomaci = rnd.Next(0,5),
                            GoliGosti = rnd.Next(0,5)
                        });
                    }
                }
                await context.SaveChangesAsync();
            }

            if (!context.Igralci.Any())
            {
                var allEkipe = context.Ekipe.AsNoTracking().ToList();
                var allDrzave = context.Drzave.AsNoTracking().ToList();
                var positions = new[] {"Goalkeeper","Defender","Midfielder","Forward"};

                foreach(var team in allEkipe)
                {
                    for(int i=1;i<=5;i++)
                    {
                        context.Igralci.Add(new Igralec
                        {
                            Ime = $"Player{i}_{team.Ime.Split(' ')[0]}",
                            Pozicija = positions[rnd.Next(positions.Length)],
                            EkipaId = team.EkipaId,
                            DrzavaId = allDrzave[rnd.Next(allDrzave.Count)].DrzavaId
                        });
                    }
                }
                await context.SaveChangesAsync();
            }

            if (!context.Dogodek_Na_Tekmi.Any())
            {
                var allTekme = await context.Tekme.ToListAsync();
                var allPlayers = await context.Igralci.ToListAsync();
                foreach(var tekma in allTekme)
                {
                    int eventsCount = rnd.Next(2,6);
                    for(int i=0;i<eventsCount;i++)
                    {
                        var player = allPlayers[rnd.Next(allPlayers.Count)];
                        context.Dogodek_Na_Tekmi.Add(new DogodekNaTekmi
                        {
                            TekmaId = tekma.TekmaId,
                            IgralecId = player.IgralecId,
                            Minuta = rnd.Next(1,91),
                            St_Dogodka = i+1,
                            TipDogodka = rnd.Next(0,2)==0?"Goal":(rnd.Next(0,2)==0?"Yellow Card":"Red Card")
                        });
                    }
                }
var adminEmail = Environment.GetEnvironmentVariable("ADMIN_EMAIL");
var user = await context.Uporabniki.FirstOrDefaultAsync(u => u.Email == adminEmail);
if(user != null)
                {
            await userManager.AddToRoleAsync(user, "Admin");
                }
var allSeasons = context.Sezone.AsNoTracking().ToList();
var allTeams = context.Ekipe.AsNoTracking().ToList();
var allTeamInSeason = context.Ekipa_V_Sezoni.AsNoTracking().ToList();


foreach (var season in allSeasons)
{
    foreach (var team in allTeams)
    {
        if (!allTeamInSeason.Any(e => e.EkipaId == team.EkipaId && e.SezonaId == season.Id))
        {
            context.Ekipa_V_Sezoni.Add(new EkipaVSezoni
            {
                EkipaId = team.EkipaId,
                SezonaId = season.Id,
                Tocke = 0,
                Zmage = 0,
                Remi = 0,
                Porazi = 0,
                Goli = 0,
                Prejeti_Goli = 0
            });
        }
    }
                await context.SaveChangesAsync();    
                var allSeason = context.Sezone.AsNoTracking().ToList();
                var leaderboardService = new LeaderboardService(context);

                foreach(var seasons in allSeasons)
            {
                    await leaderboardService.UpdateLeaderboardAsync(seasons.Id);
            }
    
}
            }
        }
    }
}
