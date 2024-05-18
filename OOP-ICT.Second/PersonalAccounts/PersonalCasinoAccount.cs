using OOP_ICT.Second.Exceptions;

namespace OOP_ICT.Second.PersonalAccounts;

public class PersonalCasinoAccount : IAccount<double>
{
    private int _playerId;
    private double _amountOfChips;

    public PersonalCasinoAccount(int playerId, int balance)
    {
        _playerId = playerId;
        _amountOfChips = balance;
    }

    public double GetBalance()
    {
        return _amountOfChips;
    }

    public int GetAccountId()
    {
        return _playerId;
    }

    public void ReplenishBalance(double chips)
    {
        if (chips <= 0)
        {
            throw new NegativeAmountOfChipsException("Amount of chips is negative or equal to zero");
        }
        _amountOfChips += chips;   
    }

    public void DeductFromBalance(double chips)
    {
        if (_amountOfChips < chips)
        {
            throw new NotEnoughChipsException("Not enough chips to deduct");
        }
        _amountOfChips -= chips;
    }

    public bool PossibleTransfer(double chips)
    {
        return chips <= _amountOfChips;
    }
}