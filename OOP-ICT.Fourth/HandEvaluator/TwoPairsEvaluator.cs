using OOP_ICT.Fourth.Combinations;
using OOP_ICT.Models;
using OOP_ICT.Second.Models;

namespace OOP_ICT.Fourth.HandEvaluator;

public class TwoPair : ICombination
{
    public bool CheckCombination(Player player)
    {
        if (!HasTwoPairs(player))
        {
            return false;
        }
        return true;
    }
    
    public string GetName()
    {
        return "TwoPair";
    }

    private bool HasTwoPairs(Player player)
    {
        List<Card> playerCards = player.GetPlayerHand().GetPlayerCards();
        List<int> valuesOfDenominationsInHand = GetDenominationValues(playerCards);
        

        valuesOfDenominationsInHand.Sort();
        int pairCount = 0;

        for (int i = 0; i < valuesOfDenominationsInHand.Count - 1; i++)
        {
            if (valuesOfDenominationsInHand[i] == valuesOfDenominationsInHand[i + 1])
            {
                pairCount++;
                i++; // skip next card in pair
            }
        }

        return pairCount == 2; 
    }
    
    private List<int> GetDenominationValues(List<Card> cards)
    {
        List<int> denominationValues = new List<int>();
        CardConverter cardConverter = new CardConverter();
        
        foreach (Card card in cards)
        {
            denominationValues.Add(cardConverter.GetCardValue(card.GetDenomination()));
        }

        return denominationValues;
    }
}