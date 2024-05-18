using OOP_ICT.Fifth.Constants;
using OOP_ICT.Fifth.FirstTurnUI;
using OOP_ICT.Fifth.MainMenuUI;
using OOP_ICT.Fifth.Messages;
using Spectre.Console;

namespace OOP_ICT.Fifth.StartGameUI;

public class StartGameCommandHandler
{
    public void Handle(StartGameCommand command)
    {
        switch (command.PlayOrExit)
        {
            case ConstantStrings.StartGame:
                MainMenuCommandHandler.Games[ConstantNumbers.IndexOfGameWeAreIn].DealCards();

                var message = "Now all the players have their 2 cards";
                var color = "green";
                MessageGenerator.DisplayMessage(color, message);
                AnsiConsole.WriteLine();

                foreach (var player in MainMenuCommandHandler.Games[ConstantNumbers.IndexOfGameWeAreIn].SeePlayersPlaying())
                {
                    var cardsMessage = $"{player.GetPlayerName()}'s cards:";
                    var cardsMessageColor = "yellow";
                    MessageGenerator.DisplayMessage(cardsMessageColor, cardsMessage);
                    AnsiConsole.WriteLine();
                    
                    var cards = player.GetPlayerHand().GetPlayerCards();
                    foreach (var playerCard in cards)
                    {
                        var card = $"{playerCard.GetSuit()} {playerCard.GetDenomination()}";
                        var cardMessageColor = "green";
                        MessageGenerator.DisplayMessage(cardMessageColor, card);
                    }
                }

                var blindsMenu = new BlindsMenuView();
                blindsMenu.DisplayBlindsMenu();
                break;
            case ConstantStrings.Exit:
                Environment.Exit(0);
                return;
        }
    }
}