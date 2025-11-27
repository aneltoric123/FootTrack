using FootTrack.Data;
using Microsoft.EntityFrameworkCore;
using FootTrack.Models;
using FootTrack.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

public class TekmovanjeController: Controller
{
    private readonly FootTrackContext _context;
    public TekmovanjeController(FootTrackContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> SeasonDetails(int seasonId,int? krogIndex)
    {
        var season = await _context.Sezone.Include(s => s.Krogi).ThenInclude(k => k.Tekme).ThenInclude(f => f.DomacaEkipa)
        .Include(s => s.Krogi).ThenInclude(k => k.Tekme).ThenInclude(f => f.GostujocaEkipa)
        .Include(s => s.Krogi).ThenInclude(k => k.Tekme).ThenInclude(f => f.Stadion).FirstOrDefaultAsync(s => s.Id == seasonId);

        if (season == null)
        return NotFound();

        var krogi = season.Krogi.OrderBy(k => k.KrogId).ToList();

        if (!krogi.Any())
        {
        ViewBag.Message = "No rounds available for this season.";
        return View(new SeasonDetailsViewModel
        {
            Sezona = season,
            Krogi = new List<Krog>(),
            CurrentKrogIndex = 0,
            CurrentKrog = null
        });
        }
        int index = krogIndex ?? 0;
        if (index < 0) index = 0;
        if (index >= krogi.Count) index = krogi.Count - 1;
        var viewModel = new SeasonDetailsViewModel
        {
            Sezona = season,
            Krogi = krogi,
            CurrentKrogIndex = index,
            CurrentKrog = krogi[index]
        };
        return View(viewModel);
    }

    public async Task<IActionResult> Leaderboard(int seasonId)
    {
        var season = await _context.Sezone.Include(s => s.Ekipe).ThenInclude(ev => ev.Ekipa).FirstOrDefaultAsync(s => s.Id == seasonId);

        var leadeerboard = season.Ekipe.OrderByDescending(e => e.Tocke).ThenByDescending(e => e.Goli - e.Prejeti_Goli)
        .ThenByDescending(k => k.Ekipa.Ime).ToList();

        return View(leadeerboard);
    }
}