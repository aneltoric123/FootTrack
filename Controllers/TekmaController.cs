using System.Threading.Tasks;
using System.Xml.Schema;
using FootTrack.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

using FootTrack.Models;
using FootTrack.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;

public class TekmaController : Controller
{
    private readonly FootTrackContext _context;
    private readonly UserManager<Uporabnik> _usermanager;

    public TekmaController(FootTrackContext context,UserManager<Uporabnik> usermanager)
    {
        _context = context;
        _usermanager = usermanager;

    }
    public async Task<IActionResult> Details(int id)
    {
        var tekma = await _context.Tekme.Include(t => t.DomacaEkipa).Include(t => t.GostujocaEkipa).Include(t => t.Stadion).ThenInclude(s => s.Mesto)
        .ThenInclude(m => m.Drzava).Include(t => t.Krog).ThenInclude(k => k.Sezona).ThenInclude(f => f.Tekmovanje).Include(t => t.Dogodki).ThenInclude(i => i.Igralec).Include(t => t.Igralci).ThenInclude(g => g.Igralec)
        .FirstOrDefaultAsync(t => t.TekmaId == id);

        if (tekma == null){
            return null;
        }
        var vm = new TekmaDetailsViewModel
        {
            Tekma = tekma,
            Dogodki = tekma.Dogodki.OrderBy(d => d.Minuta).ToList(),
            Igralci = tekma.Igralci.ToList()
        };
        return View(vm);
    }
    [HttpGet]
    public async Task<IActionResult> Create()
    {
    
    var model = new TekmaCreateViewModel
    {
        Tekma = new Tekma(),

        Ekipe = _context.Ekipe
            .Select(e => new SelectListItem
            {
                Value = e.EkipaId.ToString(),
                Text = e.Ime
            }).ToList(),

        Stadioni = _context.Stadioni
            .Select(s => new SelectListItem
            {
                Value = s.StadionId.ToString(),
                Text = s.Ime
            }).ToList(),
        Krogi = _context.Krogi
        .Include(k => k.Sezona)
            .ThenInclude(s => s.Tekmovanje)
        .Select(k => new SelectListItem
        {
            Value = k.KrogId.ToString(),
            Text = $"Krog {k.Stevilka} – {k.Sezona.Leto} {k.Sezona.Tekmovanje.Ime}"
        }).ToList()
    };

    return View(model);
}
[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Create(TekmaCreateViewModel model)
{
    Console.WriteLine($"ADDED GAME ID: {model.Tekma.TekmaId}");
    if (!ModelState.IsValid)
{
    Console.WriteLine("MODELSTATE INVALID");

    foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
    {
        Console.WriteLine(error.ErrorMessage);
    }

    return View(model);
}

    _context.Tekme.Add(model.Tekma);
    _context.SaveChanges();


    _context.SaveChanges();
    return RedirectToAction("Index","Admin");
}


    
}