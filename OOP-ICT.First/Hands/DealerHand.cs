using OOP_ICT.Hands;

namespace OOP_ICT.Models;

public class DealerHand : IHand
{
    private List<Card> _hand;

    public DealerHand()
    {
        _hand = new List<Card>();
    }

    public void ReceiveCard(Card card)
    {
        _hand.Add(card);
    }

    public List<Card> GetDealerCards()
    {
        return _hand;
    }
}