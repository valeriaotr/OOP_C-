using OOP_ICT.Models;
using OOP_ICT.Second.PersonalAccounts;

namespace OOP_ICT.Second.Models;

public class Player : IPlayer
{
    private string _name;
    private int _playerId;
    private PlayerHand _hand;
    private int _bestHand;
    public int BetAmount { get; set; }
    
    public string FirstChoice { get; set; }
    public string SecondChoice { get; set; }
    public string ThirdChoice { get; set; }
    
    public int FirstBet { get; set; }
    public int SecondBet { get; set; }
    public int ThirdBet { get; set; }
    public double Winning { get; set; }
    public int PlayedGameId { get; set; }
    
    private PersonalBankAccount _bankAccount;
    private PersonalCasinoAccount _casinoAccount;

    public Player(string name)
    {
        _name = name;
        _hand = new PlayerHand();
    }

    public string GetPlayerName()
    {
        return _name;
    }
    
    public void SetBankAccount(PersonalBankAccount bankAccount)
    {
        _bankAccount = bankAccount;
        _playerId = bankAccount.GetAccountId();
    }

    public void SetCasinoAccount(PersonalCasinoAccount casinoAccount)
    {
        _casinoAccount = casinoAccount;
        _playerId = casinoAccount.GetAccountId();
    }

    public PersonalBankAccount GetBankAccount()
    {
        return _bankAccount; 
    }

    public PersonalCasinoAccount GetCasinoAccount()
    {
        return _casinoAccount;
    }

    public int GetPlayerId()
    {
        return _playerId;
    }

    public PlayerHand GetPlayerHand()
    {
        return _hand;
    }

    public void ReceiveCard(Card card)
    {
        _hand.ReceiveCard(card);
    }
    
    public override bool Equals(object obj)
    {
        if (obj is Player other)
        {
            return _name == other.GetPlayerName();
        }
        return false;
    }

    public override int GetHashCode()
    {
        return _name.GetHashCode();
    }

    public void SetBestHand(int bestHand)
    {
        _bestHand = bestHand;
    }

    public int GetBestHand()
    {
        return _bestHand;
    }
}