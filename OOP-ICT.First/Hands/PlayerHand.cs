using System.Collections;
using System.Text;
using OOP_ICT.Hands;

namespace OOP_ICT.Models;

public class PlayerHand : IHand
{
    private List<Card> _hand;

    public PlayerHand()
    {
        _hand = new List<Card>();
    }

    public void ReceiveCard(Card card)
    {
        _hand.Add(card);
    }

    public List<Card> GetPlayerCards()
    {
        return _hand;
    }
}