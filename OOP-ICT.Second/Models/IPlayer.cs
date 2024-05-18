using OOP_ICT.Second.PersonalAccounts;

namespace OOP_ICT.Second.Models;

public interface IPlayer
{
    string GetPlayerName();
    int GetPlayerId();
    void SetBankAccount(PersonalBankAccount bankAccount);
    void SetCasinoAccount(PersonalCasinoAccount casinoAccount);
    PersonalBankAccount GetBankAccount();
    PersonalCasinoAccount GetCasinoAccount();
}