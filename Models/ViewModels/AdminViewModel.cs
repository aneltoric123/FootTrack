namespace FootTrack.Models.ViewModels;

public class AdminViewModel
{
    public Uporabnik? Admin {get;set;} 

    public List<Tekma>? Tekme {get;set;}

    public List<Tekmovanje>? Tekmovanja {get;set;}

}