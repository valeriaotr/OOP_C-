using OOP_ICT.Fifth.Constants;
using OOP_ICT.Fifth.MainMenuUI;
using OOP_ICT.Second.Models;

namespace OOP_ICT.Fifth.AddPlayerUI;

public class AddPlayerCommandHandler
{
    public void Handle(AddPlayerCommand command)
    {
        Player player = new Player(command.PlayerName);
        MainMenuCommandHandler.Games[ConstantNumbers.IndexOfGameWeAreIn].AddPlayerToGame(player);
    }
}