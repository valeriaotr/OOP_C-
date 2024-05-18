using OOP_ICT.Models;
using OOP_ICT.Second.Exceptions;
using OOP_ICT.Second.Models;
using OOP_ICT.Second.PersonalAccounts;
using static OOP_ICT.Third.ValuesOfCards.ValuesOfCards;

namespace OOP_ICT.Third.PlayerFacade;

public class GameFacade
{
    private Player _player;
    private Dealer _dealer;
    private BlackjackCasino _blackjackCasino;
    private BankCasinoFacade _bankCasinoFacade;

    public GameFacade(Player player, Dealer dealer, BlackjackCasino blackjackCasino, BankCasinoFacade bankCasinoFacade)
    {
        _player = player;
        _dealer = dealer;
        _blackjackCasino = blackjackCasino;
        _bankCasinoFacade = bankCasinoFacade;
    }

    public void ToBet(Player player, double amountOfChips)
    {
        if (amountOfChips <= 0)
        {
            throw new NegativeAmountOfChipsException("Your bet should be > 0");
        }
        PersonalCasinoAccount personalCasinoAccount = player.GetCasinoAccount();
        
        if (!personalCasinoAccount.PossibleTransfer(amountOfChips))
        {
            throw new NotEnoughChipsException("Not enough chips on your balance to bet");
        }
        personalCasinoAccount.DeductFromBalance(amountOfChips);
    }

    public int CalculatePlayerPoints(int newValueForAce)
    {
        int totalPlayerPoints = 0;
        
        var playerCards = _player.GetPlayerHand().GetPlayerCards();
        ValuesOfCards.ValuesOfCards valuesOfCards = new ValuesOfCards.ValuesOfCards();
        
        foreach (Card card in playerCards)
        {
            if (card.GetDenomination() == "Ace")
            {
                valuesOfCards.SetValueForAce(newValueForAce);
            }
            int cardValue = valuesOfCards.GetValueForCard(card.GetDenomination(),newValueForAce);
            totalPlayerPoints += cardValue;
        }

        return totalPlayerPoints;
    }

    public int CalculateDealerPoints(int newValueForAce)
    {
        int totalDealerPoints = 0;
    
        var dealerCards = _dealer.GetDealerHand().GetDealerCards();
        ValuesOfCards.ValuesOfCards valuesOfCards = new ValuesOfCards.ValuesOfCards();
        
        foreach (Card card in dealerCards)
        {
            if (card.GetDenomination() == "Ace")
            {
                if (newValueForAce != 1 && newValueForAce != 11)
                {
                    throw new InvalidValueForAceException("Your value for ace should be 1 or 11");
                }
                valuesOfCards.SetValueForAce(newValueForAce);
                totalDealerPoints += newValueForAce;
            }
            int cardValue = valuesOfCards.GetValueForCard(card.GetDenomination(),newValueForAce);
            totalDealerPoints += cardValue;
        }
        
        return totalDealerPoints;
    }

    public void DetermineWinners(int totalPlayerPoints, int totalDealerPoints, int bet)
    {
        if (totalDealerPoints > 21 || (totalPlayerPoints <= 21 && totalPlayerPoints > totalDealerPoints))
        {
            _blackjackCasino.PayWinnings(_player, _bankCasinoFacade, bet * 2);
        }
        else if (totalPlayerPoints < 21 && totalPlayerPoints < totalDealerPoints)
        {
            _blackjackCasino.ChargeLoss(_player, _bankCasinoFacade, 0);
        }
        else if (totalPlayerPoints == totalDealerPoints)
        {
            _bankCasinoFacade.PayToPlayer(_player, bet);
        }
        else if (totalPlayerPoints == 21)
        {
            _blackjackCasino.HandleBlackjack(_player, _bankCasinoFacade, bet);
        }
    }
}