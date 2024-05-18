using OOP_ICT.Fourth.HandEvaluator;
using OOP_ICT.Fourth.Poker;
using OOP_ICT.Models;
using OOP_ICT.Second.Exceptions;
using OOP_ICT.Second.Models;
using OOP_ICT.Second.PersonalAccounts;

namespace OOP_ICT.Fourth.Facades;

public class PokerFacade
{
    private PokerCasino _pokerCasino;
    private BankPokerFacade _bankPokerFacade;
    
    public PokerFacade(Dealer dealer, PokerCasino pokerCasino)
    {
        _pokerCasino = pokerCasino;
    }

    public void CallBlindInFirstTurn(Player player, double smallBlind)
    {
        if (smallBlind <= 0)
        {
            throw new NegativeAmountOfChipsException("Blinds should be > 0");
        }

        double bigBlind = smallBlind * 2;
        PersonalCasinoAccount personalCasinoAccount = player.GetCasinoAccount();
        if (!personalCasinoAccount.PossibleTransfer(bigBlind))
        {
            _pokerCasino.AllInBet(player);
        }
        personalCasinoAccount.DeductFromBalance(bigBlind);
    }

    public void PutBlind(Player player, double smallBlind)
    {
        if (smallBlind <= 0)
        {
            throw new NegativeAmountOfChipsException("Small blind should be > 0");
        }
        PersonalCasinoAccount personalCasinoAccount = player.GetCasinoAccount();

        if (!personalCasinoAccount.PossibleTransfer(smallBlind))
        {
            _pokerCasino.AllInBet(player);
        }
        personalCasinoAccount.DeductFromBalance(smallBlind);
    }

    public int DeterminePlayerCombination(Player player)
    {
        Combinations.Combinations combinations = new Combinations.Combinations();
        combinations
            .CheckCombination<RoyalFlush>(player)
            .CheckCombination<StraightFlush>(player)
            .CheckCombination<FourOfAKind>(player)
            .CheckCombination<FullHouse>(player)
            .CheckCombination<Flush>(player)
            .CheckCombination<Straight>(player)
            .CheckCombination<ThreeOfAKind>(player)
            .CheckCombination<TwoPair>(player)
            .CheckCombination<OnePair>(player)
            .CheckCombination<HighCard>(player);

        int foundCombination = combinations.GetPlayersCombinationValue(player);
        return foundCombination;
    }

    public Player DetermineWinner(List<Player> players)
    {
        List<int> listOfCombinationsPlayersGot = new List<int>();
        foreach (Player player in players)
        {
            listOfCombinationsPlayersGot.Add(DeterminePlayerCombination(player));
        }
        
        listOfCombinationsPlayersGot.Sort();
        int winnerCombination = listOfCombinationsPlayersGot[0];
        Player winner = null;
        foreach (Player player in players)
        {
            if (DeterminePlayerCombination(player) == winnerCombination)
            {
                winner = player;
            }
        }
        
        return winner;
    }
}