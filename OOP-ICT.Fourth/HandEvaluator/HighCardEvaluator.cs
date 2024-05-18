using OOP_ICT.Fourth.Combinations;
using OOP_ICT.Models;
using OOP_ICT.Second.Models;

namespace OOP_ICT.Fourth.HandEvaluator;

public class HighCard : ICombination
{
    public bool CheckCombination(Player player)
    {
        return true;
    }
    
    public string GetName()
    {
        return "HighCard";
    }

    public int FindHighCard(Player player)
    {
        List<Card> playerCards = player.GetPlayerHand().GetPlayerCards();
        List<int> denominations = GetDenominationValues(playerCards);
        denominations.Sort();
        int amountOfCards = denominations.Count;
        return denominations[amountOfCards - 1];
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