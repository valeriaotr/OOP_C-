using OOP_ICT.Fifth.AddPlayerUI;
using OOP_ICT.Fifth.BankMenuUI;
using OOP_ICT.Fifth.Constants;
using OOP_ICT.Fifth.Messages;
using Spectre.Console;

namespace OOP_ICT.Fifth.ChoiceOfPlayersAmountUI;

public class ChoiceOfPlayersAmountCommandHandler
{
    public void Handle(ChoiceOfPlayersAmountCommand command)
    {
        while (true)
        {
            if (command.NumberOfPlayers < ConstantNumbers.MinimalQuantityOfPlayers || command.NumberOfPlayers > ConstantNumbers.MaximalQuantityOfPlayers)
            {
                var message = "You can't add less than 2 players, and you can add up to 5 players.";
                var color = "red"; 
                MessageGenerator.DisplayMessage(color, message); 
                AnsiConsole.WriteLine();
                
                // reask about amount of players
                var ask = "Enter the number of players:";
                var askColor = "blue";
                var newNumberOfPlayers = MessageGenerator.PromptForNumberInt(askColor, ask);

                command.NumberOfPlayers = newNumberOfPlayers;
            }
            else
            {
                for (int i = 0; i < command.NumberOfPlayers; i++)
                {
                    var askNameMessage = $"Enter name for Player {i + ConstantNumbers.IndexToGetNextPlayer}:";
                    var askNameMessageColor = "blue";
                    string playerName = MessageGenerator.PromprForString(askNameMessageColor, askNameMessage);
                    
                    var addPlayerHandler = new AddPlayerCommandHandler();
                    var addPlayerCommand = new AddPlayerCommand { PlayerName = playerName };
                    addPlayerHandler.Handle(addPlayerCommand);
                }
                var bankMenuCommandHandler = new BankMenuCommandHandler();
                bankMenuCommandHandler.ProcessPlayers();

                break;
            }
        }
    }
}