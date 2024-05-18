using OOP_ICT.Second.Exceptions;
using OOP_ICT.Second.Models;

namespace OOP_ICT.Second.PersonalAccounts;

public class PersonalBankAccount : IAccount<PayUnit>
{
    private int _playerId;
    private PayUnit _balance;

    public PersonalBankAccount(int playerId, PayUnit startingBalance)
    {
        _playerId = playerId;
        _balance = startingBalance;
    }

    public PayUnit GetBalance()
    {
        return _balance;
    }
    
    public int GetAccountId()
    {
        return _playerId;
    }
    
    public void ReplenishBalance(PayUnit money)
    {
        if (money.Integer <= 0 && money.Fractional == 0)
        {
            throw new NegativeAmountOfMoneyException("Amount of money is negative or equal to zero");
        }

        _balance.Integer += money.Integer;
        _balance.Fractional += money.Fractional;
        
        if (_balance.Fractional >= 100)
        {
            _balance.Integer += (int)money.Fractional / 100;
            _balance.Fractional = money.Fractional % 100;
        }
    }

    public void DeductFromBalance(PayUnit money)
    {
        if (_balance.Integer < money.Integer || (_balance.Integer == money.Integer && _balance.Fractional < money.Fractional))
        {
            throw new NotEnoughMoneyException("Not enough money to deduct");
        }
        _balance.Integer -= money.Integer;

        if (_balance.Fractional < money.Fractional)
        {
            _balance.Integer--;
            _balance.Fractional += 100 - money.Fractional;
        }
        else
        {
            _balance.Fractional -= money.Fractional;
        }
    }

    public bool PossibleBet(PayUnit bet)
    {
        return bet <= _balance;
    }
}