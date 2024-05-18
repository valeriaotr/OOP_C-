using OOP_ICT.Second.Models;

namespace OOP_ICT.Fifth.SecondTurnUI;

public record GetBetCommand
{
    public string Choice { get; set; }
    public Player Player { get; set; }
    public int BetAmount { get; set; }
}