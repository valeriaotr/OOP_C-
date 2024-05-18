using OOP_ICT.Models;

namespace OOP_ICT.Fourth.Combinations;

public class CardConverter
{
    public static Dictionary<string, int> CardValues = new Dictionary<string, int>
    {
        {"Ace", 14},
        { "King", 13 },
        { "Queen", 12 },
        { "Jack", 11 },
        { "Ten", 10 },
        { "Nine", 9 },
        { "Eight", 8 },
        { "Seven", 7 },
        { "Six", 6 },
        { "Five", 5 },
        { "Four", 4 },
        { "Three", 3 },
        { "Two", 2 },
    };

    public int GetCardValue(string denomination)
    {
        return CardValues[denomination];
    }
}
