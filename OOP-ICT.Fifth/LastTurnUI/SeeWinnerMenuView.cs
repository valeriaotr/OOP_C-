using OOP_ICT.Fifth.Constants;
using OOP_ICT.Fifth.Messages;
using Spectre.Console;

namespace OOP_ICT.Fifth.LastTurnUI;

public class SeeWinnerMenuView
{
    public void DisplayMenu()
    {
        var message = "We have got a winner!";
        var color = "green";
        MessageGenerator.DisplayMessage(color, message);
        AnsiConsole.WriteLine();
        
        while (true)
        {
            var menuName = "WINNER MENU:";
            var menuColor = "cyan";
            MessageGenerator.DisplayMessage(menuColor, menuName);
            
            var selection = new SelectionPrompt<string>()
                .Title(ConstantStrings.QuestionBeforeStart)
                .PageSize(ConstantNumbers.PageSize);

            selection.AddChoice(ConstantStrings.SeeWinner);
            selection.AddChoice(ConstantStrings.Exit);

            var choice = AnsiConsole.Prompt(selection);

            var command = new GetNameOfWinnerCommand { GetOrExit = choice };
            var commandHandler = new GetNameOfWinnerCommandHandler();
            commandHandler.Handle(command);
        }
    }
}