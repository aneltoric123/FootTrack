using FootTrack.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using FootTrack.Filters;
using FootTrack.Data;

[ApiController]
[ApiKeyAuth]
[Route("api/tekme")]
public class TekmeAPIController : ControllerBase
{
    private readonly FootTrackContext _context;

    public TekmeAPIController(FootTrackContext context)
    {
        _context = context;
    }
[HttpGet("test")]
public IActionResult Test()
{
    return Ok(User.Identity!.Name);
}

   
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tekme = await _context.Tekme.Include(t => t.DomacaEkipa).Include(t => t.GostujocaEkipa).Include(t => t.Stadion).ThenInclude(s => s.Mesto).ThenInclude(m => m.Drzava)
        .Include(t => t.Krog).ThenInclude(k => k.Sezona).ThenInclude(f => f.Tekmovanje).ToListAsync();
        return Ok(tekme);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var tekma = await _context.Tekme.Include(t => t.DomacaEkipa).Include(t => t.GostujocaEkipa).Include(t => t.Stadion).ThenInclude(s => s.Mesto).ThenInclude(m => m.Drzava)
        .Include(t => t.Krog).ThenInclude(k => k.Sezona).ThenInclude(f => f.Tekmovanje).FirstOrDefaultAsync(t=> t.TekmaId ==id);
        if (tekma == null) return NotFound();
        return Ok(tekma);
    }


    [HttpPost]
    public async Task<IActionResult> Create(Tekma tekma)
    {
        _context.Tekme.Add(tekma);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = tekma.TekmaId }, tekma);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Tekma tekma)
    {
        if (id != tekma.TekmaId) return BadRequest();
        _context.Entry(tekma).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var tekma = await _context.Tekme.FindAsync(id);
        if (tekma == null) return NotFound();

        _context.Tekme.Remove(tekma);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
