namespace FootTrack.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using FootTrack.Models;

public class TekmaCreateViewModel
{
    public Tekma Tekma { get; set; }
        public IEnumerable<SelectListItem>? Ekipe { get; set; }
        public IEnumerable<SelectListItem>? Stadioni { get; set; }
        public IEnumerable<SelectListItem>? Krogi { get; set; }

}
