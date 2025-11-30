using FootTrack.Data;
using FootTrack.Models;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;

public class LeaderboardService
{
    private readonly FootTrackContext _context;

    public LeaderboardService(FootTrackContext context)
    {
        _context = context;
    }
    public async Task UpdateLeaderboardAsync(int seasonID)
    {
        var matches = await _context.Tekme.Where(f =>f.Krog.SezonaId == seasonID).ToListAsync();

        var seasonTeams = await _context.Ekipa_V_Sezoni.Where(e => e.SezonaId == seasonID).ToListAsync();

        foreach(var st in seasonTeams)
        {
            st.Tocke = 0;
            st.Goli = 0;
            st.Prejeti_Goli = 0;
            st.Zmage = 0;
            st.Remi = 0;
            st.Porazi = 0;
        }
        foreach(var match in matches)
        {
            var home = seasonTeams.FirstOrDefault(e => e.EkipaId == match.DomacaEkipaId);
            var away = seasonTeams.FirstOrDefault(e => e.EkipaId == match.GostujocaEkipaId);
            if (home != null && away != null){
                
            home.Goli += match.GoliDomaci;
            away.Goli += match.GoliGosti;
            home.Prejeti_Goli += match.GoliGosti;
            away.Prejeti_Goli += match.GoliDomaci;
            if(match.GoliGosti > match.GoliDomaci)
            {
                away.Tocke += 3;
                away.Zmage += 1;
                home.Porazi += 1;
            }
            else if(match.GoliDomaci > match.GoliGosti)
            {
                home.Tocke += 3;
                away.Porazi += 1;
                home.Zmage += 1;
            }
            else
            {
                home.Tocke += 1;
                away.Tocke += 1;
                home.Remi += 1;
                away.Remi += 1;
            }
            await _context.SaveChangesAsync();
        }
        }

    }
    public async Task UpdateAllLeaderboards()
    {
        var seasons = await _context.Sezone.ToListAsync();
        foreach(var season in seasons)
        {
            await UpdateLeaderboardAsync(season.Id);
        }
    }
}