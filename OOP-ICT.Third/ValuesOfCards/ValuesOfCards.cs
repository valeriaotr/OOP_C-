using OOP_ICT.Second.Exceptions;

namespace OOP_ICT.Third.ValuesOfCards;

public class ValuesOfCards
{
    public static Dictionary<string, int> CardsValues = new Dictionary<string, int>
    {
        { "King", 10 },
        { "Queen", 10 },
        { "Jack", 10 },
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

    public int GetValueForCard(string denomination,int newValue)
    {
        if (denomination == "Ace")
        {
            return SetValueForAce(newValue); //throw new AceValueException("You have to set your own value for ace");
        }
        return CardsValues[denomination];
    }

    public int SetValueForAce(int newValue)
    {
        return newValue;
    }
}