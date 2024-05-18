using OOP_ICT.Models;
using OOP_ICT.Second.Models;
using OOP_ICT.Second.PersonalAccounts;
using Xunit;
using OOP_ICT.Third.ValuesOfCards;
using OOP_ICT.Third.GameFactory;
using OOP_ICT.Third.PlayerFacade;
using Xunit.Abstractions;

namespace OOP_ICT.Third.Tests;

public class Testing
{
    private readonly ITestOutputHelper _output;

    public Testing(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void ValuesOfCardsTesting()
    {
        ValuesOfCards.ValuesOfCards valuesOfCards = new ValuesOfCards.ValuesOfCards();
        Card cardKing = new Card("Hearts", "King", true);
        string denomination = cardKing.GetDenomination();
        int value = valuesOfCards.GetValueForCard(denomination,11);
        Assert.Equal(10, value);

        Card cardAce = new Card("Hearts", "Ace", true);
        string newDenomination = cardAce.GetDenomination();
        int newValue = valuesOfCards.SetValueForAce(1);
        Assert.Equal(1, newValue);
    }

    [Fact]
    public void ListOfPlayersTesting()
    {
        Game game = new Game();
        Player player1 = new Player("Ivan");
        Player player2 = new Player("Dima");

        game.AddPlayerToGame(player1);
        game.AddPlayerToGame(player2);

        List<Player> expectedList = new List<Player>() { player1, player2 };
        List<Player> listPlayers = game.SeePlayersPlaying();
        Assert.Equal(expectedList, listPlayers);
    }

    [Fact]
    public void DealCardsTesting()
    {
        Game game = new Game();
        Player player1 = new Player("Dima");
        game.AddPlayerToGame(player1);
        game.DealCards();
        Assert.Equal(49,game.GetCards().Count);
        Assert.Equal(2,player1.GetPlayerHand().GetPlayerCards().Count);
        Assert.Equal(1,game.GetGameDealer().GetDealerHand().GetDealerCards().Count);
        game.GiveMoreCardsToPlayer(player1);
        Assert.Equal(3,player1.GetPlayerHand().GetPlayerCards().Count);
        game.GiveMoreCardsToDealer();
        Assert.Equal(2,game.GetGameDealer().GetDealerHand().GetDealerCards().Count);
    }

    [Fact]
    public void BetTesting()
    {
        Player player1 = new Player("Dima");
        Game game = new Game();
        GeneralCasinoAccount generalCasinoAccount = new GeneralCasinoAccount(1000);
        BlackjackCasino blackjackCasino = game.InitializeBlackjackCasino(generalCasinoAccount);
        Bank bank = new Bank(player1);
        BankCasinoFacade bankCasinoFacade = new BankCasinoFacade(bank, generalCasinoAccount, blackjackCasino);
        GameFacade gameFacade = new GameFacade(player1, game.GetGameDealer(), blackjackCasino, bankCasinoFacade);
        
        PayUnit twenty = new PayUnit(20);
        
        bank.CreateNewAccount(player1);
        bank.AddMoney(player1, twenty);
        
        Assert.Equal(twenty,player1.GetBankAccount().GetBalance());
        
        blackjackCasino.AddChips(player1, bankCasinoFacade, twenty);
        Assert.Equal(20,player1.GetCasinoAccount().GetBalance());
        game.MakeBet(player1, gameFacade, 10);
        Assert.Equal(10, player1.GetCasinoAccount().GetBalance());
    }

    [Fact]
    public void DoesPlayerWin()
    {
        Player player1 = new Player("Dima");
        Game game = new Game();
        GeneralCasinoAccount generalCasinoAccount = new GeneralCasinoAccount(1000);
        BlackjackCasino blackjackCasino = game.InitializeBlackjackCasino(generalCasinoAccount);
        Bank bank = new Bank(player1);
        BankCasinoFacade bankCasinoFacade = new BankCasinoFacade(bank, generalCasinoAccount, blackjackCasino);
        GameFacade gameFacade = new GameFacade(player1, game.GetGameDealer(), blackjackCasino, bankCasinoFacade);
        
        game.AddPlayerToGame(player1);
        bank.CreateNewAccount(player1);
        
        PayUnit twenty = new PayUnit(20);
        bank.AddMoney(player1, twenty);
        blackjackCasino.AddChips(player1, bankCasinoFacade, twenty);
        game.MakeBet(player1, gameFacade, 10);
        game.GetGameDealer().ShuffleDeck();
        game.DealCards();
        player1.GetPlayerHand().ReceiveCard(new Card("Clubs","Ace",false));
        Assert.Equal(21,game.CalculatePointsOfPlayer(gameFacade, 11));
        game.FindOutWinner(gameFacade, game.CalculatePointsOfPlayer(gameFacade, 11), game.CalculatePointsOfDealer(gameFacade, 1), 10);
        Assert.Equal(30,player1.GetCasinoAccount().GetBalance());
    }
    
    
    [Fact]
    public void DoesPlayerLose()
    {
        Player player1 = new Player("Dima");
        Game game = new Game();
        GeneralCasinoAccount generalCasinoAccount = new GeneralCasinoAccount(1000);
        BlackjackCasino blackjackCasino = game.InitializeBlackjackCasino(generalCasinoAccount);
        Bank bank = new Bank(player1);
        BankCasinoFacade bankCasinoFacade = new BankCasinoFacade(bank, generalCasinoAccount, blackjackCasino);
        GameFacade gameFacade = new GameFacade(player1, game.GetGameDealer(), blackjackCasino, bankCasinoFacade);
        
        game.AddPlayerToGame(player1);
        bank.CreateNewAccount(player1);
        
        PayUnit twenty = new PayUnit(20);
        bank.AddMoney(player1, twenty);
        blackjackCasino.AddChips(player1, bankCasinoFacade, twenty);
        game.MakeBet(player1, gameFacade, 10);
        game.GetGameDealer().ShuffleDeck();
        game.DealCards();
        player1.GetPlayerHand().ReceiveCard(new Card("Clubs","Two",false));
        game.GetGameDealer().GetDealerHand().ReceiveCard(new Card("Clubs","King",false));
        game.GetGameDealer().GetDealerHand().ReceiveCard(new Card("Clubs","Two",false));
        Assert.Equal(12,game.CalculatePointsOfPlayer(gameFacade, 11));
        game.FindOutWinner(gameFacade, game.CalculatePointsOfPlayer(gameFacade, 11), game.CalculatePointsOfDealer(gameFacade, 1), 10);
        
        Assert.Equal(10,player1.GetCasinoAccount().GetBalance());
    }
    
    [Fact]
    public void IsItDraw()
    {
        Player player1 = new Player("Dima");
        Game game = new Game();
        GeneralCasinoAccount generalCasinoAccount = new GeneralCasinoAccount(1000);
        BlackjackCasino blackjackCasino = game.InitializeBlackjackCasino(generalCasinoAccount);
        Bank bank = new Bank(player1);
        BankCasinoFacade bankCasinoFacade = new BankCasinoFacade(bank, generalCasinoAccount, blackjackCasino);
        GameFacade gameFacade = new GameFacade(player1, game.GetGameDealer(), blackjackCasino, bankCasinoFacade);
        
        game.AddPlayerToGame(player1);
        bank.CreateNewAccount(player1);
        
        PayUnit twenty = new PayUnit(20);
        bank.AddMoney(player1, twenty);
        blackjackCasino.AddChips(player1, bankCasinoFacade, twenty);
        game.MakeBet(player1, gameFacade, 10);
        game.GetGameDealer().ShuffleDeck();
        game.DealCards();
        player1.GetPlayerHand().ReceiveCard(new Card("Clubs","Two",false));
        game.GetGameDealer().GetDealerHand().ReceiveCard(new Card("Clubs","King",false));
        Assert.Equal(12,game.CalculatePointsOfPlayer(gameFacade, 11));
        game.FindOutWinner(gameFacade, game.CalculatePointsOfPlayer(gameFacade, 11), game.CalculatePointsOfDealer(gameFacade, 1), 10);
        
        Assert.Equal(20,player1.GetCasinoAccount().GetBalance());
    }
} 