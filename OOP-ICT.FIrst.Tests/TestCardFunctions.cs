using OOP_ICT.Models;
using Xunit;

namespace OOP_ICT.FIrst.Tests;

public class TestCardFunctions
{
    [Fact]
    public void CardTesting()
    {
        Card card = new Card("Diamonds", "Two", false);
        Assert.Equal("Diamonds", card.GetSuit());
        Assert.Equal("Two", card.GetDenomination());
        Assert.False(card.GetIsVisible());
        card.SetIsVisible(true);
        Assert.True(card.GetIsVisible());
    }

    [Fact]
    public void CardDeckTesting()
    {
        CardDeck cardDeck = new CardDeck();
        List<string> actualCardSuits = Suits.CardSuits;
        List<string> suits = new List<string>() { "Hearts", "Diamonds", "Clubs", "Spades" };
        
        for (int i = 0; i < suits.Count; i++)
        {
            Assert.Equal(suits.ElementAt(i), actualCardSuits.ElementAt(i));
        }

        List<string> actualDenominations = Values.CardDenominations;
        List<string> denominations = new List<string>()
            { "Ace", "King", "Queen", "Jack", "Ten", "Nine", "Eight", "Seven", "Six", "Five", "Four", "Three", "Two" };
        
        for (int i = 0; i < denominations.Count; i++)
        {
            Assert.Equal(denominations.ElementAt(i), actualDenominations.ElementAt(i));
        }
        
        Assert.Equal(52, cardDeck.GetCardDeck().Count);

        List<Card> cardsInDeck = cardDeck.GetCardDeck();
        for (int i = 0; i<cardsInDeck.Count; i+=4)
        {
            Assert.Equal(cardDeck.GetCardDeck().ElementAt(i).GetDenomination(), cardsInDeck.ElementAt(i).GetDenomination());
        }
    }

    [Fact]
    public void DealerTesting()
    {
        Dealer dealer = new Dealer();
        dealer.ShuffleDeck();
        List<Card> shuffledCardDeck = dealer.GetCardDeckForUnitTests();
        Assert.Equal("Hearts", shuffledCardDeck.ElementAt(0).GetSuit());
        Assert.Equal("Clubs", shuffledCardDeck.ElementAt(1).GetSuit());
        Assert.Equal("Diamonds", shuffledCardDeck.ElementAt(2).GetSuit());
        Assert.Equal("Spades", shuffledCardDeck.ElementAt(3).GetSuit());
    }
}