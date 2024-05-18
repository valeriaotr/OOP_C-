using OOP_ICT.Second.Models;

namespace OOP_ICT.Fifth.BankMenuUI;

public record BankMenuCommand
{
    public Player Player { get; init; }
    public string Choice { get; init; }
}