using Xunit;
using OOP_ICT.Second.AccountFactories;
using OOP_ICT.Second.Models;
using OOP_ICT.Second.PersonalAccounts;
using Xunit.Abstractions;

namespace OOP_ICT.Second.Tests;

public class Testing
{
    private readonly ITestOutputHelper _output;

    public Testing(ITestOutputHelper output)
    {
        _output = output;
    }
    
    [Fact]
    public void BankAccountFactoryTesting()
    {
        PersonalBankAccountFactory bankFactory = new PersonalBankAccountFactory();
        IAccount<PayUnit> newBankAccount = bankFactory.CreateAccount();
        PersonalBankAccount account = (PersonalBankAccount)newBankAccount;
        PayUnit zero = new PayUnit(0);
        
        Assert.Equal(1, bankFactory.GetId());
        Assert.Equal(zero, account.GetBalance());
        IAccount<PayUnit> secondBankAccount = bankFactory.CreateAccount();
        Assert.Equal(2, bankFactory.GetId());
    }

    [Fact]
    public void CasinoAccountFactoryTesting()
    {
        PersonalCasinoAccountFactory casinoFactory = new PersonalCasinoAccountFactory();
        IAccount <double> newCasinoAccount = casinoFactory.CreateAccount();
        Assert.Equal(1, casinoFactory.GetId());
        IAccount<double> secondCasinoAccount = casinoFactory.CreateAccount();
        Assert.Equal(2, casinoFactory.GetId());
    }

    [Fact]
    public void GeneralCasinoTesting()
    {
        GeneralCasinoAccount casino = new GeneralCasinoAccount(100);
        Assert.Equal(100, casino.GetBalance());
        casino.DeductFromBalance(10);
        Assert.Equal(90, casino.GetBalance());
        casino.ReplenishBalance(20);
        Assert.Equal(110, casino.GetBalance());
    }

    [Fact]
    public void PersonalBankAccountTesting()
    {
        PayUnit hundred = new PayUnit(100);
        PayUnit ten = new PayUnit(10);
        PayUnit ninety = new PayUnit(90);
        PayUnit zero = new PayUnit(0);
        
        PersonalBankAccount account = new PersonalBankAccount(1, zero);
        account.ReplenishBalance(hundred);
        Assert.Equal(hundred, account.GetBalance());
        
        account.DeductFromBalance(ten);
        Assert.Equal(ninety, account.GetBalance());
        Assert.True(account.PossibleBet(ten));
    }

    [Fact]
    public void PersonalCasinoAccountTesting()
    {
        PersonalCasinoAccount account = new PersonalCasinoAccount(1, 0);
        Assert.Equal(0, account.GetBalance());
        
        account.ReplenishBalance(10);
        Assert.Equal(10, account.GetBalance());
    }

    [Fact]
    public void PlayerTesting()
    {
        Player player = new Player("Ivan");
        PersonalBankAccountFactory bankAccountFactory = new PersonalBankAccountFactory();
        IAccount<PayUnit> bankAccount = bankAccountFactory.CreateAccount();
        PersonalBankAccount newPersonalBankAccount = (PersonalBankAccount)bankAccount;
        
        player.SetBankAccount(newPersonalBankAccount);
        string name = player.GetPlayerName();
        int id = player.GetPlayerId();
        
        Assert.Equal("Ivan", name);
        Assert.Equal(1, id);
    }

    [Fact]
    public void BankTesting()
    {
        Player player = new Player("Eva");
        Bank bank = new Bank(player);
        PersonalBankAccount bankAccount = bank.CreateNewAccount(player);
        
        Assert.True(bank.BankAccountExists("Eva", 1));

        PayUnit money = new PayUnit(100);
        bank.AddMoney(player, money);
        Assert.Equal(money, bankAccount.GetBalance());

        PayUnit moneyToDeduct = new PayUnit(10);
        PayUnit finalBalance = new PayUnit(90);
        bank.WriteOffMoney(player, moneyToDeduct);
        Assert.Equal(finalBalance, bankAccount.GetBalance());
        
        Assert.True(bank.IfPossibleToBet(player, finalBalance));
    }

    [Fact]
    public void BlackjackCasinoTesting()
    {
        Player player = new Player("Veronika");
        GeneralCasinoAccount generalCasinoAccount = new GeneralCasinoAccount(1000);
        BlackjackCasino blackjackCasino = new BlackjackCasino(generalCasinoAccount);
        Bank bank = new Bank(player);
        BankCasinoFacade facade = new BankCasinoFacade(bank, generalCasinoAccount, blackjackCasino);
        PersonalCasinoAccount casinoAccount = blackjackCasino.CreateNewCasinoAccount(player);
        PersonalBankAccount bankAccount = bank.CreateNewAccount(player);
        PayUnit money = new PayUnit(100);
        bank.AddMoney(player, money);
        
        Assert.True(blackjackCasino.CasinoAccountExists("Veronika", 1));
        PayUnit moneyToTransfer = new PayUnit(10.57);
        blackjackCasino.AddChips(player, facade, moneyToTransfer);
        Assert.Equal(10, casinoAccount.GetBalance());
        
        blackjackCasino.ChargeLoss(player, facade, 10);
        Assert.Equal(0, casinoAccount.GetBalance());
        
        blackjackCasino.HandleBlackjack(player, facade, 2);
        Assert.Equal(4, casinoAccount.GetBalance());
    }
}