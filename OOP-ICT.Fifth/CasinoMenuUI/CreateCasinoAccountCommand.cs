using OOP_ICT.Second.Models;

namespace OOP_ICT.Fifth.CasinoMenuUI;

public record CreateCasinoAccountCommand
{
    public Player Player { get; init; }
}