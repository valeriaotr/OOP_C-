using OOP_ICT.Second.Models;

namespace OOP_ICT.Third.GameFactory;

public interface IGame
{
    void DealCards();
    void AddPlayerToGame(Player player);
    void GiveMoreCardsToPlayer(Player player);
}