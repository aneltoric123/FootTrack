using System.Xml.Schema;
using FootTrack.Data;
using FootTrack.Models;
using FootTrack.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class HomeController : Controller
{
    private readonly FootTrackContext _context;
    private readonly UserManager<Uporabnik> _userManager;

    public HomeController(FootTrackContext context, UserManager<Uporabnik> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var appUser = await _userManager.GetUserAsync(User);
        List<Tekma>? tekme = new();
        Ekipa? najEkipa = null;

        if (appUser != null && appUser.NajljubsaEkipaId.HasValue)
        {
            najEkipa = await _context.Ekipe
                .Include(e => e.Stadion)
                .ThenInclude(s => s.Mesto)
                .ThenInclude(m => m.Drzava)
                .FirstOrDefaultAsync(e => e.EkipaId == appUser.NajljubsaEkipaId.Value);

            var query = _context.Tekme.Include(t => t.DomacaEkipa).Include(t => t.GostujocaEkipa)
            .Where(t => t.DomacaEkipaId == najEkipa.EkipaId || t.GostujocaEkipaId == najEkipa.EkipaId);

            tekme = await query.ToListAsync();
        }

        var viewModel = new HomeViewModel
        {
            AppUser = appUser,
            NajEkipa = najEkipa,
            Tekme = tekme,


        };

        return View(viewModel);
    }
}
