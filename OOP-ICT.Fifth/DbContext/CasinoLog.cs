using System.ComponentModel.DataAnnotations;

namespace OOP_ICT.Fifth.DbContext;

public class CasinoLog
{
    [Key]
    public string PlayerName { get; set; }
    public string FirstChoice { get; set; }
    public string SecondChoice { get; set; }
    public string ThirdChoice { get; set; }
    
    public int FirstBet { get; set; }
    public int SecondBet { get; set; }
    public int ThirdBet { get; set; }
    public int GameId { get; set; }
}