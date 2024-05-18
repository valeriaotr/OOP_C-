using OOP_ICT.Fifth.Constants;
using OOP_ICT.Fifth.Messages;
using Spectre.Console;

namespace OOP_ICT.Fifth.LastTurnUI;

public class LastMenuView
{
    public void DisplayMenu()
    {
        var message = "Now we can lay last card on table.";
        var color = "green";
        MessageGenerator.DisplayMessage(color, message);
        AnsiConsole.WriteLine();
        
        while (true)
        {
            var menuName = "CARDS MENU:";
            var menuColor = "cyan";
            MessageGenerator.DisplayMessage(menuColor, menuName);
            
            var selection = new SelectionPrompt<string>()
                .Title(ConstantStrings.QuestionBeforeStart)
                .PageSize(ConstantNumbers.PageSize);

            selection.AddChoice(ConstantStrings.LayLastCard);
            selection.AddChoice(ConstantStrings.Exit);

            var choice = AnsiConsole.Prompt(selection);

            var command = new LayLastCardOnTableCommand { LayOrExit = choice };
            var commandHandler = new LayLastCardOnTableCommandHandler();
            commandHandler.Handle(command);
        }
    }
}