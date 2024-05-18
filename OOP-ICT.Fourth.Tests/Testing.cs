using OOP_ICT.Fourth.Combinations;
using OOP_ICT.Fourth.Facades;
using OOP_ICT.Fourth.Poker;
using OOP_ICT.Models;
using OOP_ICT.Second.Models;
using OOP_ICT.Second.PersonalAccounts;
using Xunit;

namespace OOP_ICT.Fourth.Tests;

public class Testing
{
    [Fact]
    public void CardConverterTesting()
    {
        CardConverter cardConverter = new CardConverter();
        string denomination1 = new Card("Hearts", "Ace", false).GetDenomination();
        Assert.Equal(14, cardConverter.GetCardValue(denomination1));
        string denomination2 = new Card("Hearts", "Two", false).GetDenomination();
        Assert.Equal(2, cardConverter.GetCardValue(denomination2));
    }

    [Fact]
    public void CombinationValuesTesting()
    {
        Combinations.Combinations combinations = new Combinations.Combinations();
        string combination1 = "FourOfAKind";
        Assert.Equal(3, combinations.GetCombinationValue(combination1));
        string combination2 = "FullHouse";
        Assert.Equal(4, combinations.GetCombinationValue(combination2));
    }

    [Fact]
    public void CallBlindTesting()
    {
        var generalCasino = new GeneralCasinoAccount(100);
        var player = new Player("Dima");
        var dealer = new Dealer();
        var bank = new Bank(player);
        var pokerCasino = new PokerCasino(generalCasino);
        var bankPokerFacade = new BankPokerFacade(bank, generalCasino, pokerCasino);
        var pokerFacade = new PokerFacade(dealer, pokerCasino, bankPokerFacade);

        PayUnit fifty = new PayUnit(50);
        bank.CreateNewAccount(player);
        bank.AddMoney(player, fifty);
        pokerCasino.CreateNewCasinoAccount(player);
        bankPokerFacade.TopUpCasinoAccount(player, fifty);
        pokerFacade.CallBlindInFirstTurn(player, 10);
        
        Assert.Equal(30, player.GetCasinoAccount().GetBalance());
    }

    [Fact]
    public void PutBlindTesting()
    {
        var generalCasino = new GeneralCasinoAccount(100);
        var player = new Player("Dima");
        var dealer = new Dealer();
        var bank = new Bank(player);
        var pokerCasino = new PokerCasino(generalCasino);
        var bankPokerFacade = new BankPokerFacade(bank, generalCasino, pokerCasino);
        var pokerFacade = new PokerFacade(dealer, pokerCasino, bankPokerFacade);
        
        PayUnit fifty = new PayUnit(50);
        bank.CreateNewAccount(player);
        bank.AddMoney(player, fifty);
        pokerCasino.CreateNewCasinoAccount(player);
        bankPokerFacade.TopUpCasinoAccount(player, fifty);
        
        pokerFacade.PutBlind(player, 5);
        Assert.Equal(45, player.GetCasinoAccount().GetBalance());
    }

    [Fact]
    public void DetermineCombinationTesting()
    {
        var generalCasino = new GeneralCasinoAccount(100);
        var player1 = new Player("Dima");
        var dealer = new Dealer();
        var bank1 = new Bank(player1);
        var pokerCasino = new PokerCasino(generalCasino);
        var bankPokerFacade = new BankPokerFacade(bank1, generalCasino, pokerCasino);
        var pokerFacade = new PokerFacade(dealer, pokerCasino, bankPokerFacade);

        player1.ReceiveCard(new Card("Heart", "Ace", false));
        player1.ReceiveCard(new Card("Heart", "King", false));
        player1.ReceiveCard(new Card("Heart", "Queen", false));
        player1.ReceiveCard(new Card("Heart", "Jack", false));
        player1.ReceiveCard(new Card("Heart", "Ten", false));
        player1.ReceiveCard(new Card("Clubs", "Ace", false));
        player1.ReceiveCard(new Card("Clubs", "Two", false));
        Assert.Equal(1, pokerFacade.DeterminePlayerCombination(player1));

        var player2 = new Player("Lisa");
        var bank2 = new Bank(player2);
        var bankPokerFacade2 = new BankPokerFacade(bank2, generalCasino, pokerCasino);
        var pokerFacade2 = new PokerFacade(dealer, pokerCasino, bankPokerFacade2);
        
        player2.ReceiveCard(new Card("Heart", "King", false));
        player2.ReceiveCard(new Card("Clubs", "King", false));
        player2.ReceiveCard(new Card("Heart", "Two", false));
        player2.ReceiveCard(new Card("Clubs", "Two", false));
        player2.ReceiveCard(new Card("Diamonds", "Two", false));
        player2.ReceiveCard(new Card("Heart", "Queen", false));
        player2.ReceiveCard(new Card("Heart", "Jack", false));
        
        Assert.Equal(4, pokerFacade2.DeterminePlayerCombination(player2));
    }

    [Fact]
    public void DetermineWinnerTesting()
    {
        var generalCasino = new GeneralCasinoAccount(100);
        var player1 = new Player("Dima");
        var dealer = new Dealer();
        var bank1 = new Bank(player1);
        var pokerCasino = new PokerCasino(generalCasino);
        var bankPokerFacade = new BankPokerFacade(bank1, generalCasino, pokerCasino);
        var pokerFacade = new PokerFacade(dealer, pokerCasino, bankPokerFacade);

        player1.ReceiveCard(new Card("Heart", "Ace", false)); // RoyalFlush
        player1.ReceiveCard(new Card("Heart", "King", false));
        player1.ReceiveCard(new Card("Heart", "Queen", false));
        player1.ReceiveCard(new Card("Heart", "Jack", false));
        player1.ReceiveCard(new Card("Heart", "Ten", false));
        player1.ReceiveCard(new Card("Clubs", "Ace", false));
        player1.ReceiveCard(new Card("Clubs", "Two", false));

        var player2 = new Player("Lisa");
        List<Player> players = new List<Player>();
        players.Add(player1);
        players.Add(player2);
        
        player2.ReceiveCard(new Card("Heart", "King", false)); // FullHouse
        player2.ReceiveCard(new Card("Clubs", "King", false));
        player2.ReceiveCard(new Card("Heart", "Two", false));
        player2.ReceiveCard(new Card("Clubs", "Two", false));
        player2.ReceiveCard(new Card("Diamonds", "Two", false));
        player2.ReceiveCard(new Card("Heart", "Queen", false));
        player2.ReceiveCard(new Card("Heart", "Jack", false));
        
        Assert.Equal(player1, pokerFacade.DetermineWinner(players));
    }

    [Fact]
    public void PokerCasinoTesting()
    {
        var generalCasino = new GeneralCasinoAccount(100);
        var player = new Player("Dima");
        var dealer = new Dealer();
        var bank = new Bank(player);
        var pokerCasino = new PokerCasino(generalCasino);
        var bankPokerFacade = new BankPokerFacade(bank, generalCasino, pokerCasino);
        
        PayUnit fifty = new PayUnit(50);
        bank.CreateNewAccount(player);
        bank.AddMoney(player, fifty);
        pokerCasino.CreateNewCasinoAccount(player);
        bankPokerFacade.TopUpCasinoAccount(player, fifty);
        
        pokerCasino.MakeBet(player, bankPokerFacade, 10);
        Assert.Equal(40, player.GetCasinoAccount().GetBalance());
        pokerCasino.AllInBet(player);
        Assert.Equal(0, player.GetCasinoAccount().GetBalance());
    }

    [Fact]
    public void PokerGameTesting()
    {
        var generalCasino = new GeneralCasinoAccount(100);
        
        var pokerCasino = new PokerCasino(generalCasino);
        PokerGame game = new PokerGame();
        Dealer dealer = game.GetGameDealer();
        Player player1 = new Player("Andrey");
        Player player2 = new Player("Artem");
        var bank = new Bank(player1);
        var bankPokerFacade = new BankPokerFacade(bank, generalCasino, pokerCasino);
        var pokerFacade = new PokerFacade(dealer, pokerCasino, bankPokerFacade);

        PayUnit fifty = new PayUnit(50);
        bank.CreateNewAccount(player1);
        bank.CreateNewAccount(player2);
        bank.AddMoney(player1, fifty);
        bank.AddMoney(player2, fifty);
        pokerCasino.CreateNewCasinoAccount(player1);
        pokerCasino.CreateNewCasinoAccount(player2);
        bankPokerFacade.TopUpCasinoAccount(player1, fifty);
        bankPokerFacade.TopUpCasinoAccount(player2, fifty);
        
        game.AddPlayerToGame(player1);
        game.AddPlayerToGame(player2);
        
        game.DealCards();
        Assert.Equal(2, player1.GetPlayerHand().GetPlayerCards().Count);
        Assert.Equal(2, player2.GetPlayerHand().GetPlayerCards().Count);
        
        game.GetBlinds(pokerFacade, 5);
        Assert.Equal(45, player1.GetCasinoAccount().GetBalance());
        Assert.Equal(40, player2.GetCasinoAccount().GetBalance());
        
        game.FoldCards(player1);
        List<Player> players = game.SeePlayersPlaying();
        Assert.DoesNotContain(player1, players);
        
        game.LayFirstThreeCardsOnTable();
        Assert.Equal(5, player2.GetPlayerHand().GetPlayerCards().Count);
        
        game.LayFourthCardOnTable();
        Assert.Equal(6, player2.GetPlayerHand().GetPlayerCards().Count);
        
        game.LayLastCardOnTable();
        Assert.Equal(7, player2.GetPlayerHand().GetPlayerCards().Count);
    }
}