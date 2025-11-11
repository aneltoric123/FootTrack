using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using FootTrack.Models;
using Microsoft.AspNetCore.Authorization;

namespace FootTrack.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<Uporabnik> _userManager;

        public ProfileController(UserManager<Uporabnik> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("Login", "Account");

            return View(user);
        }
    }
}
