using OOP_ICT.Second.Models;

namespace OOP_ICT.Fifth.BankMenuUI;

public record TopUpBankAccountCommand
{
    public Player Player { get; init; }
    public PayUnit Money { get; set; }
    public string Choice { get; init; }
}