using OOP_ICT.Models;

class Game
{
    public static void Main(string[] args)
    {
        Dealer dealer = new Dealer(); 
        dealer.ShuffleDeck();
    }
}