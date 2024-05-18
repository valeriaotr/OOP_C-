using OOP_ICT.Fourth.HandEvaluator;
using OOP_ICT.Models;
using OOP_ICT.Second.Models;

namespace OOP_ICT.Fourth.Combinations;

public class Combinations
{
    public static Dictionary<string, int> ValuesOfCombinations = new Dictionary<string, int>()
    {
        { "RoyalFlush", 1 },
        { "StraightFlush", 2 },
        { "FourOfAKind", 3 },
        { "FullHouse", 4 },
        { "Flush", 5 },
        { "Straight", 6 },
        { "ThreeOfAKind", 7 },
        { "TwoPair", 8 },
        { "OnePair", 9 },
        { "HighCard", 10 }
    };

    public List<string> GetCombinationsNames()
    {
        List<string> keys = ValuesOfCombinations.Keys.ToList();
        return keys;
    }

    public int GetCombinationValue(string combination)
    {
        return ValuesOfCombinations[combination];
    }

    public static string GetCombinationName(int numberOfCombination)
    {
        foreach (var pair in ValuesOfCombinations)
        {
            if (pair.Value == numberOfCombination)
            {
                return pair.Key;
            }
        }

        return "Unknown";
    } 

    public int GetPlayersCombinationValue(Player player)
    {
        return player.GetBestHand();
    }
    
    public Combinations CheckCombination<T>(Player player) where T : ICombination, new()
    {
        T combination = new T();
        bool result = combination.CheckCombination(player);
        if (result)
        {
            int combinationValue = GetCombinationValue(combination.GetName());
            if (player.GetBestHand() != 0 && combinationValue < player.GetBestHand())
            {
                player.SetBestHand(combinationValue);
            }
            else if (player.GetBestHand() == 0)
            {
                player.SetBestHand(combinationValue);
            }
        }
        return this;
    }
}