using OOP_ICT.Models;
using OOP_ICT.Second.Models;

namespace OOP_ICT.Fourth.HandEvaluator;

public class RoyalFlush : ICombination
{
    public bool CheckCombination(Player player)
    {
        if (!HasRoyalFlush(player))
        {
            return false;
        }
        return true;
    }
    
    public string GetName()
    {
        return "RoyalFlush";
    }
    
    private bool IsRoyalFlushDenominations(Player player)
    {
        List<string> neededDenominations = new List<string>() { "Ten", "Jack", "Queen", "Kind", "Ace" };
        List<string> uniqueDenominations = new List<string>();
        List<Card> playerCards = player.GetPlayerHand().GetPlayerCards();
        foreach(Card card in playerCards)
        {
            uniqueDenominations.Add(card.GetDenomination());
        }

        foreach (string denomination in uniqueDenominations)
        {
            if (!uniqueDenominations.Contains(denomination))
            {
                return false;
            }
        }

        return true;
    }

    private bool HasRoyalFlush(Player player)
    {
        List<Card> playerCards = player.GetPlayerHand().GetPlayerCards();
        var royalFlushDenominations = IsRoyalFlushDenominations(player);
        bool flush = false;
        foreach (var group in playerCards.GroupBy(card => card.GetSuit()))
        {
            if (group.Count() >= 5)
            {
                flush = true;
                break;
            }
        }

        return flush && royalFlushDenominations;
    }
}