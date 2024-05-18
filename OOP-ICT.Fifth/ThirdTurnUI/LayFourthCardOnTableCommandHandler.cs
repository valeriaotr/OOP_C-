using OOP_ICT.Fifth.Constants;
using OOP_ICT.Fifth.MainMenuUI;
using OOP_ICT.Fifth.Messages;
using OOP_ICT.Fifth.SecondTurnUI;
using Spectre.Console;

namespace OOP_ICT.Fifth.ThirdTurnUI;

public class LayFourthCardOnTableCommandHandler
{
    public void Handle(LayFourthCardOnTableCommand command)
    {
        switch (command.LayOrExit)
        {
            case ConstantStrings.LayFourthCard:
                MainMenuCommandHandler.Games[ConstantNumbers.IndexOfGameWeAreIn].LayFourthCardOnTable();

                var message = "Fourth card is on table:";
                var color = "green";
                MessageGenerator.DisplayMessage(color, message);

                foreach (var card in MainMenuCommandHandler.Games[ConstantNumbers.IndexOfGameWeAreIn].GetTable())
                {
                    var cardMessage = $"{card.GetSuit()} {card.GetDenomination()}";
                    var cardMessageColor = "yellow";
                    MessageGenerator.DisplayMessage(cardMessageColor, cardMessage);
                }
                
                var betsMenu = new GetBetsMenuView();

                foreach (var player in MainMenuCommandHandler.Games[ConstantNumbers.IndexOfGameWeAreIn].SeePlayersPlaying())
                {
                    var betCommand = new GetBetCommand();
                    betCommand.Player = player;
                    betsMenu.DisplayMenu(betCommand);
                }
                break;
            case ConstantStrings.Exit:
                Environment.Exit(0);
                return;
        }
    }
}