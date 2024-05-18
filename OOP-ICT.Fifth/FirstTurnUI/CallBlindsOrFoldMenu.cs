using OOP_ICT.Fifth.Constants;
using OOP_ICT.Fifth.MainMenuUI;
using OOP_ICT.Fifth.Messages;
using OOP_ICT.Fifth.SecondTurnUI;
using OOP_ICT.Second.Models;
using Spectre.Console;

namespace OOP_ICT.Fifth.FirstTurnUI;

public class CallBlindsOrFoldMenu
{
    public void DisplayMenu(CallBlindsOrFoldCommand command)
    {
        while (true)
        {
            var choiceMessage = $"{command.Player.GetPlayerName()} can call big blind or fold cards:";
            var choiceMessageColor = "cyan";
            MessageGenerator.DisplayMessage(choiceMessageColor, choiceMessage);
            
            var selection = new SelectionPrompt<string>()
                .Title(ConstantStrings.QuestionBeforeStart)
                .PageSize(ConstantNumbers.PageSize);

            selection.AddChoice(ConstantStrings.Call);
            selection.AddChoice(ConstantStrings.Fold);
            selection.AddChoice(ConstantStrings.Exit);

            var choice = AnsiConsole.Prompt(selection);

            command.CallOrFold = choice;
            var commandHandler = new CallBlindsOrFoldCommandHandler();
            commandHandler.Handle(command);
            
            BlindsMenuView.PlayersWithChoices.Add(command.Player);
            AskAllPlayersInGame(command);
            DisplayBalancesAndNextMenu();
        }
    }
    
    private Player GetNextPlayer(Player currentPlayer)
    {
        var players = MainMenuCommandHandler.Games[ConstantNumbers.IndexOfGameWeAreIn]
            .SeePlayersPlaying();
        var currentIndex = players.FindIndex(player => player == currentPlayer);
        
        var nextIndex = (currentIndex + ConstantNumbers.IndexToGetNextPlayer) % players.Count;
        return players[nextIndex];
    }

    private void AskAllPlayersInGame(CallBlindsOrFoldCommand command)
    {
        var nextCommandIfNeeded = new CallBlindsOrFoldCommand();
        if (BlindsMenuView.PlayersWithChoices.Count < MainMenuCommandHandler.Games[ConstantNumbers.IndexOfGameWeAreIn].SeePlayersPlaying().Count)
        {
            if (command.Player == MainMenuCommandHandler.Games[ConstantNumbers.IndexOfGameWeAreIn].SeePlayersPlaying()[ConstantNumbers.IndexOfFirstPlayerInList])
            {
                // we skip 2nd player because they already bet big blind
                var thirdPlayer =
                    MainMenuCommandHandler.Games[ConstantNumbers.IndexOfGameWeAreIn].SeePlayersPlaying()
                        [ConstantNumbers.IndexOfThirdPlayerInList];
                nextCommandIfNeeded.Player = thirdPlayer;
                nextCommandIfNeeded.SmallBlind = command.SmallBlind;
                DisplayMenu(nextCommandIfNeeded);
            }
            var nextPlayer = GetNextPlayer(command.Player);
            nextCommandIfNeeded.Player = nextPlayer;
            nextCommandIfNeeded.SmallBlind = command.SmallBlind;
            DisplayMenu(nextCommandIfNeeded);
        }
    }

    private void DisplayBalancesAndNextMenu()
    {
        // when all players have made choices we display their balances
        foreach (var player in MainMenuCommandHandler.Games[ConstantNumbers.IndexOfGameWeAreIn].SeePlayersPlaying())
        {
            var updatedBalanceMessage =
                $"Now {player.GetPlayerName()}'s casino account balance is {player.GetCasinoAccount().GetBalance()}";
            var updatedBalanceMessageColor = "green";
            MessageGenerator.DisplayMessage(updatedBalanceMessageColor, updatedBalanceMessage);
        }
        var secondTurnMenu = new SecondTurnMenuView();
        secondTurnMenu.DisplayMenu();
    }
}