using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FootTrack.Models;
using FootTrack.Data;
using Microsoft.EntityFrameworkCore;

namespace FootTrack.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<Uporabnik> _userManager;
        private readonly SignInManager<Uporabnik> _signInManager;
        private readonly FootTrackContext _context;

        public RegisterModel(
            UserManager<Uporabnik> userManager,
            SignInManager<Uporabnik> signInManager,
            FootTrackContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public SelectList Teams { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Username")]
            public string UporabniskoIme { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Confirm Password")]
            [Compare("Password", ErrorMessage = "Passwords do not match.")]
            public string ConfirmPassword { get; set; }

            [Display(Name = "Favorite Team")]
            public int? NajljubsaEkipaId { get; set; }
        }

        public async Task OnGetAsync()
        {
            var teams = await _context.Ekipe.ToListAsync();
            Teams = new SelectList(teams, "EkipaId", "Ime");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var teams = await _context.Ekipe.ToListAsync();
            Teams = new SelectList(teams, "EkipaId", "Ime");

            if (ModelState.IsValid)
            {
                var user = new Uporabnik
                {
                    UserName = Input.UporabniskoIme,
                    Email = Input.Email,
                    UporabniskoIme = Input.UporabniskoIme,
                    NajljubsaEkipaId = Input.NajljubsaEkipaId
                };

                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect("~/");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Page();
        }
    }
}
