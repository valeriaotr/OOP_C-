using OOP_ICT.Fourth.Combinations;
using OOP_ICT.Models;
using OOP_ICT.Second.Models;

namespace OOP_ICT.Fourth.HandEvaluator;

public class OnePair : ICombination
{
    public bool CheckCombination(Player player)
    {
        if (!HasOnePair(player))
        {
            return false;
        }
        return true;
    }
    
    public string GetName()
    {
        return "OnePair";
    }

    private bool HasOnePair(Player player)
    {
        List<Card> playerCards = player.GetPlayerHand().GetPlayerCards();
        List<int> valuesOfDenominationsInHand = GetDenominationValues(playerCards);

        for (int i = 0; i < valuesOfDenominationsInHand.Count - 1; i++)
        {
            for (int j = i + 1; j < valuesOfDenominationsInHand.Count; j++)
            {
                if (valuesOfDenominationsInHand[i] == valuesOfDenominationsInHand[j])
                {
                    return true; 
                }
            }
        }

        return false;
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