namespace FootTrack.Models.ViewModels;

public class TekmaDetailsViewModel
{
    public Tekma Tekma {get;set;}
    public List<DogodekNaTekmi> Dogodki {get;set;}
    public List<IgralecNaTekmi> Igralci {get;set;}
}