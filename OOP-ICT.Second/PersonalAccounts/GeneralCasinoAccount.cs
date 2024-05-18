using OOP_ICT.Second.Exceptions;
using OOP_ICT.Second.Models;

namespace OOP_ICT.Second.PersonalAccounts;

public class GeneralCasinoAccount : IAccount<int>
{
    private int _balanceOfCasino;

    public GeneralCasinoAccount(int startiBalanceOfCasino)
    {
        if (startiBalanceOfCasino < 0)
        {
            throw new NegativeBalanceException("Balance should be >= 0");
        }
        _balanceOfCasino = startiBalanceOfCasino;
    }

    public int GetBalance()
    {
        return _balanceOfCasino;
    }

    public void DeductFromBalance(int chips)
    {
        if (_balanceOfCasino < chips)
        {
            throw new NotEnoughChipsException("Not enough chips to deduct");
        }
        _balanceOfCasino -= chips;
    }

    public void ReplenishBalance(int chips)
    {
        if (chips < 0)
        {
            throw new NegativeAmountOfChipsException("Amount of chips is negative or equal to zero");
        }
        _balanceOfCasino += chips;
    }
}