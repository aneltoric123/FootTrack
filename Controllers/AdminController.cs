using System.Xml.Schema;
using FootTrack.Data;
using FootTrack.Models;
using FootTrack.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using Microsoft.AspNetCore.Authorization;

public class AdminController : Controller
{
    private readonly FootTrackContext _context;
    private readonly UserManager<Uporabnik> _userManager;

    public AdminController(FootTrackContext context,UserManager<Uporabnik> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var adminUser = await _userManager.GetUserAsync(User);
        var adminEmail = Environment.GetEnvironmentVariable("ADMIN_EMAIL");
        if(adminUser == null || adminUser.Email != adminEmail )
        {
            return Unauthorized();
        }
        var najEkipa = await _context.Ekipe
                .Include(e => e.Stadion)
                .ThenInclude(s => s.Mesto)
                .ThenInclude(m => m.Drzava)
                .FirstOrDefaultAsync(e => e.EkipaId == adminUser.NajljubsaEkipaId.Value);
        var tekme = await _context.Tekme
    .Include(t => t.DomacaEkipa)
    .Include(t => t.GostujocaEkipa)
    .Include(t => t.Stadion)
    .Include(t => t.Krog)
        .ThenInclude(k => k.Sezona)
            .ThenInclude(s => s.Tekmovanje)
    .OrderByDescending(t => t.Datum)
    .ToListAsync();
        List<Tekmovanje>? tekmovanja = await _context.Tekmovanja.ToListAsync();
        var vm = new AdminViewModel
        
        {
            Admin = adminUser,
            najEkipa = najEkipa,
            Tekme = tekme,
            Tekmovanja = tekmovanja

        };
        return View(vm);
    }
}