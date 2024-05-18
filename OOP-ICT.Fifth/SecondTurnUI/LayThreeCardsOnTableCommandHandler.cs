using OOP_ICT.Fifth.Constants;
using OOP_ICT.Fifth.MainMenuUI;
using OOP_ICT.Fifth.Messages;
using Spectre.Console;

namespace OOP_ICT.Fifth.SecondTurnUI;

public class LayThreeCardsOnTableCommandHandler
{
    public void Handle(LayThreeCardsOnTableCommand command)
    {
        switch (command.LayOrExit)
        {
            case ConstantStrings.LayThreeCards:
                MainMenuCommandHandler.Games[ConstantNumbers.IndexOfGameWeAreIn]
                    .LayFirstThreeCardsOnTable();
                var message = "Three cards are on table:";
                var color = "green";
                MessageGenerator.DisplayMessage(color, message);

                foreach (var card in MainMenuCommandHandler.Games[ConstantNumbers.IndexOfGameWeAreIn].GetTable())
                {
                    var cardsMessage = $"{card.GetSuit()} {card.GetDenomination()}";
                    var cardsMessageColor = "yellow";
                    MessageGenerator.DisplayMessage(cardsMessageColor, cardsMessage);
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