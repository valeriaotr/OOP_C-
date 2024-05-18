namespace OOP_ICT.Models;

public class Card
{
    private string _suit;
    private string _denomination;
    private bool _isVisible;

    public Card(string suit, string denomination, bool isVisible) 
    {
        _suit = suit;
        _denomination = denomination;
        _isVisible = isVisible;
    }

    public string GetSuit() 
    {
        return _suit;
    }

    public string GetDenomination() 
    {
        return _denomination;
    }

    public bool GetIsVisible() 
    {
        return _isVisible;
    }

    public void SetIsVisible(bool isVisible) 
    {
        _isVisible = isVisible;
    }
}