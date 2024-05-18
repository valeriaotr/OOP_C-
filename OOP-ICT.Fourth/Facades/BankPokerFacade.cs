using OOP_ICT.Fourth.Poker;
using OOP_ICT.Second.Exceptions;
using OOP_ICT.Second.Models;
using OOP_ICT.Second.PersonalAccounts;

namespace OOP_ICT.Fourth.Facades;

public class BankPokerFacade
{
    private Bank _bank;
    private PokerCasino _casino;
    private GeneralCasinoAccount _generalCasino;

    public BankPokerFacade(Bank bank, GeneralCasinoAccount generalCasino, PokerCasino casino)
    {
        _bank = bank;
        _casino = casino;
        _generalCasino = generalCasino;
    }

    public void TopUpCasinoAccount(Player player, PayUnit money)
    {
        PayUnit zero = new PayUnit(0, 00);
        if (money <= zero)
        {
            throw new NegativeAmountOfMoneyException("You are trying to transfer negative amount of money");
        }
        
        string name = player.GetPlayerName();
        int playerId = player.GetPlayerId();
        
        if (!_casino.CasinoAccountExists(name, playerId))
        {
            _casino.CreateNewCasinoAccount(player);
        }
        
        PersonalCasinoAccount casinoAccount = player.GetCasinoAccount();
        PersonalBankAccount bankAccount = player.GetBankAccount();
        
        if (!_bank.IfPossibleToBet(player, money))
        {
            throw new NotEnoughMoneyException("Not enough money on bank account balance");
        }

        int moneyToChips = money.Integer;
        double extraCents = money.Fractional;
        PayUnit backToBank = new PayUnit(extraCents);
       
        _bank.WriteOffMoney(player, money);
        if (extraCents != 0)
        {
            _bank.AddMoney(player, backToBank);
        }

        casinoAccount.ReplenishBalance(moneyToChips);
    }

    public void PayToPlayer(Player player, int winning)
    {
        if (winning < 0)
        {
            throw new NegativeAmountOfChipsException("Amount of winning is negative");
        }
        string name = player.GetPlayerName();
        int playerId = player.GetPlayerId();
        
        if (!_casino.CasinoAccountExists(name, playerId))
        {
            _casino.CreateNewCasinoAccount(player);
        }
        
        PersonalCasinoAccount casinoAccount = player.GetCasinoAccount();
        _generalCasino.DeductFromBalance(winning);
        casinoAccount.ReplenishBalance(winning);
    }

    public void ChargePlayerLoss(Player player, int loss)
    {
        if (loss < 0)
        {
            throw new NegativeAmountOfChipsException("Amount of chips is negative");
        }
        
        PersonalCasinoAccount casinoAccount = player.GetCasinoAccount();
        casinoAccount.DeductFromBalance(loss);
        _generalCasino.ReplenishBalance(loss);
    }

    public void TransferChipsToMoney(Player player, double chips)
    {
        if (chips <= 0)
        {
            throw new NegativeAmountOfChipsException("You are trying to transfer negative amount of chips");
        }
        
        PersonalCasinoAccount casinoAccount = player.GetCasinoAccount();
        
        if (!_casino.IsPossibleToTransferChips(player, chips))
        {
            throw new NotEnoughChipsException("Not enough chips on casino account balance");
        }
        casinoAccount.DeductFromBalance(chips);
        
        PayUnit transferingChpis = new PayUnit(chips);
        _bank.AddMoney(player, transferingChpis);
    }

    public void MakeBet(Player player, double chips)
    {
        if (chips <= 0)
        {
            throw new NegativeAmountOfChipsException("Your bet should be > 0");
        }
        PersonalCasinoAccount personalCasinoAccount = player.GetCasinoAccount();
        
        if (!personalCasinoAccount.PossibleTransfer(chips))
        {
            throw new NotEnoughChipsException("Not enough chips on your balance to bet");
        }
        personalCasinoAccount.DeductFromBalance(chips);
    }
}