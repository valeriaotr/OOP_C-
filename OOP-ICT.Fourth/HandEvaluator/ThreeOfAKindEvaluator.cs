using OOP_ICT.Models;
using OOP_ICT.Second.Models;

namespace OOP_ICT.Fourth.HandEvaluator;

public class ThreeOfAKind : ICombination
{
    public bool CheckCombination(Player player)
    {
        if (!HasThreeOfAKind(player))
        {
            return false;
        }
        return true;
    }
    
    public string GetName()
    {
        return "ThreeOfAKind";
    }

    private bool HasThreeOfAKind(Player player)
    {
        List<Card> playerCards = player.GetPlayerHand().GetPlayerCards();

        foreach (var group in playerCards.GroupBy(card => card.GetDenomination()))
        {
            if (group.Count() == 3)
            {
                return true;
            }
        }

        return false;
    }
}