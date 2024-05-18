using OOP_ICT.Second.Models;

namespace OOP_ICT.Fifth.SecondTurnUI;

public record CallBetOrFoldCommand
{
    public Player Player { get; init; }
    public string CallOrFold { get; set; }
    public int Bet { get; init; }
}