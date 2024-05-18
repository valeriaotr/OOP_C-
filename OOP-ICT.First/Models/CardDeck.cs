namespace OOP_ICT.Models;

public class CardDeck
{
    private const int _numOfCards = 52;
    private List<Card> _cards;

    public CardDeck() 
    {
        _cards = new List<Card>();
    }
    
    public List<Card> GetCardDeck()
    {
        List<string> cardDenominations = Values.CardDenominations;
        List<string> cardSuits = Suits.CardSuits;
        var cardDeck = new List<Card>();

        foreach (string denomination in cardDenominations) 
        {
            foreach (string suit in cardSuits) 
            {
                cardDeck.Add(new Card(suit, denomination, false));
            }
        }
        return cardDeck;
    }
}