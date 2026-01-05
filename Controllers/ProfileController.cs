using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using FootTrack.Models;
using FootTrack.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

namespace FootTrack.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<Uporabnik> _userManager;
        private readonly FootTrackContext _context;

        public ProfileController(UserManager<Uporabnik> userManager,FootTrackContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("Login", "Account");
            user = await _context.Uporabniki.Include(u => u.NajljubsaEkipa).FirstOrDefaultAsync(u => u.Id == user.Id);
            return View(user);
        }
        
[HttpPost]
[ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete()
        {
            var user = await _userManager.GetUserAsync(User);
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
                return RedirectToAction("Index","Home");
            }
            else
            {
                return View("Index",user);
            }
        }

}
    }
