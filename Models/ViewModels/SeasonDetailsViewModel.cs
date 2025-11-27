using FootTrack.Models;
namespace FootTrack.Models.ViewModels;

public class SeasonDetailsViewModel
{
    public Sezona Sezona {get;set;} = null!;
    public List<Krog> Krogi {get;set;} = new();

    public Krog CurrentKrog {get;set;} = null!;

    public int CurrentKrogIndex {get;set;}
}