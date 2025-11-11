using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using FootTrack.Models;

public class ProfileModel : PageModel
{
    private readonly UserManager<Uporabnik> _userManager;

    public ProfileModel(UserManager<Uporabnik> userManager)
    {
        _userManager = userManager;
    }

    public Uporabnik? User { get; set; }

    public async Task OnGetAsync()
    {
        User = await _userManager.GetUserAsync(HttpContext.User);
    }
}
