namespace OOP_ICT.Fifth.FirstTurnUI;

public record GetBlindsCommand
{
    public string PlayOrExit { get; init; }
    public int SmallBlindAmount { get; set; }
}