using OOP_ICT.Fourth.Facades;
using OOP_ICT.Hands;
using OOP_ICT.Models;
using OOP_ICT.Second.Exceptions;
using OOP_ICT.Second.Models;
using OOP_ICT.Second.PersonalAccounts;
using OOP_ICT.Third.GameFactory;

namespace OOP_ICT.Fourth.Poker;

public class PokerGame : IGame
{
    private List<Player> _players;
    private Dealer _dealer;
    private PokerCasino _pokerCasino;
    private List<Card> _initialTable;
    private List<Card> _cardsPlayersFold;
    public static double SumOfBets;
    public static List<Player> PlayersWithAllInBet;

    private const int NumberOfCardsPlayerGets = 2;
    private const int NumberOfCardsToLayOnInitialTable = 3;
    private int _currentFirstPlayerIndex = 0;

    public PokerGame()
    {
        _players = new List<Player>();
        _dealer = new Dealer();
        _initialTable = new List<Card>();
        _cardsPlayersFold = new List<Card>();
        _dealer.ShuffleDeck();
        PlayersWithAllInBet = new List<Player>();
    }

    public double GetSumOfBets()
    {
        return SumOfBets;
    }
    
    public Dealer GetGameDealer()
    {
        return _dealer; 
    }

    public List<Card> GetTable()
    {
        return _initialTable;
    }

    public int GetNumberOfCardsOnTable()
    {
        return _initialTable.Count;
    }
    
    public PokerCasino InitializePokerCasino(GeneralCasinoAccount generalCasinoAccount)
    {
        _pokerCasino = new PokerCasino(generalCasinoAccount);
        return _pokerCasino;
    }
    
    public void AddPlayerToGame(Player player)
    {
        if (_players.Contains(player))
        {
            throw new PlayerAlreadyAddedException("This player is already in game");
        }
        _players.Add(player);
    }

    public void RemovePlayerFromGame(Player player)
    {
        _players.Remove(player);
    }

    public List<Player> SeePlayersPlaying()
    {
        return _players;
    }

    public void DealCards() // first turn before opening table cards
    {
        foreach (Player player in _players)
        {
            _dealer.DealCardsToPlayers(player.GetPlayerHand(), NumberOfCardsPlayerGets);
        }
    }

    public void GetBlinds(PokerFacade pokerFacade, double smallBlind)
    {
        int nextFirstPlayerIndex = (_currentFirstPlayerIndex + 1) % _players.Count;
        Player firstPlayer = _players[_currentFirstPlayerIndex];
        Player secondPlayer = _players[nextFirstPlayerIndex];
        double bigBlind = smallBlind * 2;

        pokerFacade.PutBlind(firstPlayer, smallBlind);
        pokerFacade.PutBlind(secondPlayer, bigBlind);
        SumOfBets += smallBlind;
        SumOfBets += bigBlind;

        _currentFirstPlayerIndex = nextFirstPlayerIndex;
    }

    public void CallBlind(PokerFacade pokerFacade, Player player, double smallBlind)
    {
        pokerFacade.CallBlindInFirstTurn(player, smallBlind);
        SumOfBets += smallBlind * 2;
    }

    public void FoldCards(Player player)
    {
        List<Card> playerCards = player.GetPlayerHand().GetPlayerCards();
        foreach (Card card in playerCards)
        {
            _cardsPlayersFold.Add(card);
        }
        playerCards.Clear();
        _players.Remove(player);
    }
 
    public void LayFirstThreeCardsOnTable() // opening first three cards
    {
        for (int i = 0; i < NumberOfCardsToLayOnInitialTable; i++)
        {
            _initialTable.Add(_dealer.LayCardOnTable());
        }
        foreach (Player player in _players)
        {
            player.ReceiveCard(_initialTable[0]);
            player.ReceiveCard(_initialTable[1]);
            player.ReceiveCard(_initialTable[2]);
        }
    }

    public void LayFourthCardOnTable() // opening 4th card
    {
        _initialTable.Add(_dealer.LayCardOnTable());
        foreach (Player player in _players)
        {
            PlayerHand playerHand = player.GetPlayerHand();
            playerHand.ReceiveCard(_initialTable[3]);
        }
    }

    public void LayLastCardOnTable() // opening 5th card
    {
        _initialTable.Add(_dealer.LayCardOnTable());
        foreach (Player player in _players)
        {
            PlayerHand playerHand = player.GetPlayerHand();
            playerHand.ReceiveCard(_initialTable[4]);
        }
    }

    public Player DetermineWinner(PokerFacade pokerFacade)
    {
        return pokerFacade.DetermineWinner(_players);
    }

    public void GiveWinningToPlayer(PokerCasino casino, BankPokerFacade facade, PokerFacade pokerFacade)
    {
        casino.PayWinnings(DetermineWinner(pokerFacade), facade, (int)SumOfBets);
    }
}