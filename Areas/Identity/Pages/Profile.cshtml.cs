using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FootTrack.Models;
using FootTrack.Data;

public class ProfileModel : PageModel
{
    private readonly UserManager<Uporabnik> _userManager;

    public ProfileModel(UserManager<Uporabnik> userManager)
    {
        _userManager = userManager;
    }

    public Uporabnik? Uporabnik { get; set; }

    public async Task OnGetAsync()
    {
        var Uporabnik = await _userManager.GetUserAsync(HttpContext.User);
        
        
    }
}
