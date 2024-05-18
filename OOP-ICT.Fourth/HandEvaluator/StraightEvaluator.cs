using OOP_ICT.Fourth.Combinations;
using OOP_ICT.Models;
using OOP_ICT.Second.Models;

namespace OOP_ICT.Fourth.HandEvaluator;

public class Straight : ICombination
{
    public bool CheckCombination(Player player)
    {
        if (!HasStraight(player))
        {
            return false;
        }
        return true;
    }
    
    public string GetName()
    {
        return "Straight";
    }
    
    public bool HasStraight(Player player)
    {
        List<Card> playerCards = player.GetPlayerHand().GetPlayerCards();
        List<int> valuesOfDenominationsInHand = GetDenominationValues(playerCards);
        
        valuesOfDenominationsInHand.Sort();
        for (int i = 0; i < valuesOfDenominationsInHand.Count - 4; i++)
        {
            bool inOrder = true;
            for (int j = i; j < i + 4; j++)
            {
                if (valuesOfDenominationsInHand[j + 1] - valuesOfDenominationsInHand[j] != 1)
                {
                    inOrder = false;
                    break;
                }
            }
            if (inOrder)
            {
                return true; 
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