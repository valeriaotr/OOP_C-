using OOP_ICT.Fifth.Constants;
using OOP_ICT.Fifth.MainMenuUI;
using OOP_ICT.Fifth.Messages;
using OOP_ICT.Second.Models;
using Spectre.Console;

namespace OOP_ICT.Fifth.SecondTurnUI;

public class CallBetOrFoldMenu
{
    public void DisplayMenu(CallBetOrFoldCommand command)
    {
        var message = $"{command.Player.GetPlayerName()} can call bet or fold cards:";
        var color = "cyan";
       MessageGenerator.DisplayMessage(color, message);
        
        // flag to control the loop
        bool continueLoop = true;

        while (continueLoop)
        {
            var selection = new SelectionPrompt<string>()
                .Title(ConstantStrings.QuestionBeforeStart)
                .PageSize(ConstantNumbers.PageSize);

            selection.AddChoice(ConstantStrings.Call);
            selection.AddChoice(ConstantStrings.Fold);
            selection.AddChoice(ConstantStrings.Exit);

            var choice = AnsiConsole.Prompt(selection);

            command.CallOrFold = choice;
            var commandHandler = new CallBetOrFoldCommandHandler();
            commandHandler.Handle(command);
            
            GetBetsMenuView.PlayersWithChoices.Add(command.Player);

            // check if all players've made choices
            if (GetBetsMenuView.PlayersWithChoices.Count >= MainMenuCommandHandler.Games[ConstantNumbers.IndexOfGameWeAreIn].SeePlayersPlaying().Count)
            {
                continueLoop = false;  // exit the loop if all players've made choices
            }
            else
            {
                // ask next player if they want to call the raised bet
                var nextPlayer = GetNextPlayer(command.Player);
                var nextCommand = new CallBetOrFoldCommand
                {
                    Player = nextPlayer,
                    Bet = command.Bet  
                };
                command = nextCommand;  
            }
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
}