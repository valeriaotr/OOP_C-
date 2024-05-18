using OOP_ICT.Models;
using OOP_ICT.Second.Models;

namespace OOP_ICT.Fourth.HandEvaluator;

public class FourOfAKind : ICombination
{
    public bool CheckCombination(Player player)
    {
        if (!HasFourOfAKind(player))
        {
            return false;
        }
        return true;
    }
    
    public string GetName()
    {
        return "FourOfAKind";
    }

    private bool HasFourOfAKind(Player player)
    {
        List<Card> playerCards = player.GetPlayerHand().GetPlayerCards();

        foreach (var group in playerCards.GroupBy(card => card.GetDenomination()))
        {
            if (group.Count() == 4)
            {
                return true;
            }
        }

        return false;
    }
}