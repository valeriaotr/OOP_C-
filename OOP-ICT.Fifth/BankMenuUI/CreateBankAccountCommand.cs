using OOP_ICT.Second.Models;

namespace OOP_ICT.Fifth.BankMenuUI;

public record CreateBankAccountCommand
{
    public Player Player { get; init; }
}