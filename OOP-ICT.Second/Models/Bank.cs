using OOP_ICT.Second.AccountFactories;
using OOP_ICT.Second.Exceptions;
using OOP_ICT.Second.PersonalAccounts;

namespace OOP_ICT.Second.Models;

public class Bank
{
    private Player _player;
    private PersonalBankAccount _bankAccount;
    private Dictionary<string, int> _idByPlayerNameStoreInBank;

    public Bank()
    {
        _idByPlayerNameStoreInBank = new Dictionary<string, int>();
    }
    public PersonalBankAccount CreateNewAccount(Player player)
    {
        PersonalBankAccountFactory bankAccountFactory = new PersonalBankAccountFactory();
        IAccount<PayUnit> bankAccount = bankAccountFactory.CreateAccount();
        PersonalBankAccount newPersonalBankAccount = (PersonalBankAccount)bankAccount;

        string name = player.GetPlayerName();
        int id = newPersonalBankAccount.GetAccountId();
        
        _idByPlayerNameStoreInBank.Add(name, id);
        player.SetBankAccount(newPersonalBankAccount);
        return newPersonalBankAccount;
    }
    public bool BankAccountExists(string name, int playerId)
    {
        if (_idByPlayerNameStoreInBank.TryGetValue(name, out int storedPlayerId))
        {
            return storedPlayerId == playerId;
        }
        return false;
    }
    
    public void AddMoney(Player player, PayUnit money)
    {
        if (money.Integer < 0)
        {
            throw new NegativeAmountOfMoneyException("You are trying to add negative amount of money");
        }
        string name = player.GetPlayerName();
        int playerId = player.GetPlayerId();
        
        if (!BankAccountExists(name, playerId))
        {
            throw new AccountDoesntExistException("Account doesn't exist");
        }
        PersonalBankAccount account = player.GetBankAccount(); 
        account.ReplenishBalance(money);
    }
    
    public void GetReward(Player player, PayUnit money)
    {
        if (money.Integer < 0)
        {
            throw new NegativeAmountOfMoneyException("You are trying to add negative amount of money");
        }
        string name = player.GetPlayerName();
        int playerId = player.GetPlayerId();
        
        if (!BankAccountExists(name, playerId))
        {
            throw new AccountDoesntExistException("Account doesn't exist");
        }
        PersonalBankAccount account = player.GetBankAccount();
        account.ReplenishBalance(money);
    }

    public void WriteOffMoney(Player player, PayUnit money)
    {
        if (money.Integer < 0)
        {
            throw new NegativeAmountOfMoneyException("You are trying to withdraw negative amount of money");
        }
        string name = player.GetPlayerName();
        int playerId = player.GetPlayerId();
        
        if (!BankAccountExists(name, playerId))
        {
            throw new AccountDoesntExistException("Account doesn't exist");
        }
        PersonalBankAccount account = player.GetBankAccount();
        account.DeductFromBalance(money);
    }

    public bool IfPossibleToBet(Player player, PayUnit bet)
    {
        string name = player.GetPlayerName();
        int playerId = player.GetPlayerId();
        
        if (!BankAccountExists(name, playerId))
        {
            throw new AccountDoesntExistException("Account doesn't exist");
        }
        PersonalBankAccount account = player.GetBankAccount();
        return account.PossibleBet(bet);
    }
}