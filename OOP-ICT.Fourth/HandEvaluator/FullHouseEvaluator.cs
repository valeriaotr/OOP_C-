using OOP_ICT.Models;
using OOP_ICT.Second.Models;

namespace OOP_ICT.Fourth.HandEvaluator;

public class FullHouse : ICombination
{
    public bool CheckCombination(Player player) 
    {
        if (!HasFullHouse(player))
        {
            return false;
        }
        return true;
    }
    
    public string GetName()
    {
        return "FullHouse";
    }

    private bool HasFullHouse(Player player)
    {
        List<Card> playerCards = player.GetPlayerHand().GetPlayerCards();

        var groups = playerCards.GroupBy(card => card.GetDenomination());

        if (groups.Any(group => group.Count() == 3))
        {
            if (groups.Any(group => group.Count() == 2))
            {
                return true;
            }
        }

        return false;
    }
}