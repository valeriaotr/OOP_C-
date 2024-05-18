using OOP_ICT.Hands;

namespace OOP_ICT.Models;

public class Dealer
{
    private List<Card> _cardDeck;
    private DealerHand _dealerHand;

    public Dealer()
    {
        _cardDeck = new CardDeck().GetCardDeck();
        _dealerHand = new DealerHand();
    }

    private List<Card> ShuffleCardDeck(List<Card> cardDeck)
    {
        int shuffleHalfSizeValue = 26;
        List<Card> shuffledDeck = new List<Card>();

        for (int i = 0; i < shuffleHalfSizeValue; i++)
        {
            shuffledDeck.Add(cardDeck[i]);
            shuffledDeck.Add(cardDeck[i + shuffleHalfSizeValue]);
        }

        return shuffledDeck;
    }

    public void ShuffleDeck()
    {
        _cardDeck = ShuffleCardDeck(_cardDeck);
    }

    public List<Card> GetCardDeckForUnitTests()
    {
        return _cardDeck;
    }

    public DealerHand GetDealerHand()
    {
        return _dealerHand;
    }

    public void DealCardsToPlayers(PlayerHand hand, int numberOfCardsToDeal)
    {
        for (int i = 0; i < numberOfCardsToDeal; i++)
        {
            if (_cardDeck.Count > 0)
            {
                int cardsAmount = _cardDeck.Count;
                hand.ReceiveCard(_cardDeck[cardsAmount-1]);
                _cardDeck.RemoveAt(cardsAmount-1);
            }
        }
    }

    public void DealCardsToDealer(int numberOfCardsToDeal)
    {
        for (int i = 0; i < numberOfCardsToDeal; i++)
        {
            if (_cardDeck.Count > 0)
            {
                int cardsAmount = _cardDeck.Count;
                _dealerHand.ReceiveCard(_cardDeck[cardsAmount-1]);
                _cardDeck.RemoveAt(cardsAmount-1);
            }
        }
    }

    public Card LayCardOnTable()
    {
        int cardsAmount = _cardDeck.Count();
        Card cardToLay = _cardDeck[cardsAmount - 1];
        _cardDeck.RemoveAt(cardsAmount - 1);
        return cardToLay;
    }
}