using OOP_ICT.Second.Models;

namespace OOP_ICT.Fifth.CasinoMenuUI;

public record CasinoMenuCommand
{
    public Player Player { get; init; }
    public string Choice { get; init; }
}