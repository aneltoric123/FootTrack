namespace FootTrack.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using FootTrack.Models;

public class TekmaCreateViewModel
{
    public Tekma Tekma { get; set; }
    public SelectList Ekipe { get; set; }
}
