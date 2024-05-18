using OOP_ICT.Second.Models;

namespace OOP_ICT.Fifth.FirstTurnUI;

public record CallBlindsOrFoldCommand
{
    public Player Player { get; set; }
    public string CallOrFold { get; set; }
    public int SmallBlind { get; set; }
}