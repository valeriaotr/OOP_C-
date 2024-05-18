using OOP_ICT.Models;
using OOP_ICT.Second.Models;

namespace OOP_ICT.Fourth.HandEvaluator;

public class Flush : ICombination
{
    public bool CheckCombination(Player player)
    {
        if (!HasFlush(player))
        {
            return false;
        }
        return true;
    }

    public string GetName()
    {
        return "Flush";
    }

    private bool HasFlush(Player player)
    {
        List<Card> playerCards = player.GetPlayerHand().GetPlayerCards();
        var groups = playerCards.GroupBy(card => card.GetSuit());

        if (groups.Any(group => group.Count() >= 5))
        {
            return true;
        }

        return false;
    }
}