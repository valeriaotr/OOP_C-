using OOP_ICT.Second.Models;

namespace OOP_ICT.Fifth.CasinoMenuUI;

public record BuyChipsCommand
{
    public Player Player { get; init; }
    public PayUnit Chips { get; set; }
    public string Choice { get; init; }
}