using OOP_ICT.Models;
using OOP_ICT.Second.Exceptions;
using OOP_ICT.Second.Models;
using OOP_ICT.Second.PersonalAccounts;
using OOP_ICT.Third.PlayerFacade;

namespace OOP_ICT.Third.GameFactory;

public class Game : IGame
{
    private List<Player> _players;
    private Dealer _dealer;
    private Bank _bank;
    private BlackjackCasino _blackjackCasino;
    
    private const int NumberOfCardsToAddToHand = 1;
    private const int NumberOfCardsDealerGets = 1;
    private const int NumberOfCardsPlayerGets = 2;
    
    public Game()
    {
        _players = new List<Player>();
        _dealer = new Dealer();
    }

    public Dealer GetGameDealer()
    {
        return _dealer; 
    }

    public void InitializeBank(Bank bank)
    {
        _bank = bank;
    }

    public BlackjackCasino InitializeBlackjackCasino(GeneralCasinoAccount generalCasinoAccount)
    {
        _blackjackCasino = new BlackjackCasino(generalCasinoAccount);
        return _blackjackCasino;
    }
    
    public void AddPlayerToGame(Player player)
    {
        if (_players.Contains(player))
        {
            throw new PlayerAlreadyAddedException("This player is already in game");
        }
        _players.Add(player);
    }

    public List<Player> SeePlayersPlaying()
    {
        return _players;
    }

    public void DealCards()
    {
        foreach (Player player in _players)
        {
            _dealer.DealCardsToPlayers(player.GetPlayerHand(), NumberOfCardsPlayerGets);
        }
        _dealer.DealCardsToDealer(NumberOfCardsDealerGets);
    }

    public void MakeBet(Player player, GameFacade gameFacade, double bet)
    {
        gameFacade.ToBet(player, bet);
    }
    

    public void GiveMoreCardsToPlayer(Player player)
    {
        if (!_players.Contains(player))
        {
            throw new PlayerNotFound("Player not found in the current game");
        }

        PlayerHand playerHand = player.GetPlayerHand();
        _dealer.DealCardsToPlayers(playerHand, NumberOfCardsToAddToHand);
    }

    public void GiveMoreCardsToDealer()
    {
        DealerHand dealerHand = _dealer.GetDealerHand();
        _dealer.DealCardsToDealer(NumberOfCardsToAddToHand);
    }

    public int CalculatePointsOfPlayer(PlayerFacade.GameFacade gameFacade, int newValueForAce)
    {
        int playerPoints = gameFacade.CalculatePlayerPoints(newValueForAce);
        return playerPoints;
    }

    public int CalculatePointsOfDealer(PlayerFacade.GameFacade gameFacade, int newValueForAce)
    {
        int dealerPoints = gameFacade.CalculateDealerPoints(newValueForAce);
        return dealerPoints;
    }

    public void FindOutWinner(PlayerFacade.GameFacade gameFacade, int playerPoints, int dealerPoints, int bet)
    {
        gameFacade.DetermineWinners(playerPoints, dealerPoints, bet);
    }

    public List<Card> GetCards()
    {
        return _dealer.GetCardDeckForUnitTests();
    }
}