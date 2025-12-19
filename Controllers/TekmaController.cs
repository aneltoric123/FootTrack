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
    public async Task<IActionResult> Create()
    {
        var ekipe = await _context.Ekipe.ToListAsync();
        var Ekipe = new SelectList(ekipe, "EkipaId", "Ime");
        var tekma = new Tekma();
        var vm = new TekmaCreateViewModel
        {
          Ekipe = Ekipe,
          Tekma = tekma  
        };
        return View(vm);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Tekma tekma)
    {
        if (ModelState.IsValid)
        {
            _context.Add(tekma);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Admin");
        }
        return View(tekma);
    }

    
}