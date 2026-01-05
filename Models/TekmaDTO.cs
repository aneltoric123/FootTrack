public class TekmaDTO
{
    public int TekmaId { get; set; }
    public string DomacaEkipaIme { get; set; } = null!;
    public string GostujocaEkipaIme { get; set; } = null!;
    public string StadionIme { get; set; } = null!;
    public DateTime Datum { get; set; }
    public int DomacaGol { get; set; }
    public int GostujocaGol { get; set; }
}
