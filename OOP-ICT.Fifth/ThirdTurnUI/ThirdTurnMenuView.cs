using OOP_ICT.Fifth.Constants;
using OOP_ICT.Fifth.Messages;
using Spectre.Console;

namespace OOP_ICT.Fifth.ThirdTurnUI;

public class ThirdTurnMenuView
{
    public void DisplayMenu()
    {
        var message = "Now we can lay 4th card on table";
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

            selection.AddChoice(ConstantStrings.LayFourthCard);
            selection.AddChoice(ConstantStrings.Exit);

            var choice = AnsiConsole.Prompt(selection);

            var command = new LayFourthCardOnTableCommand { LayOrExit = choice };
            var commandHandler = new LayFourthCardOnTableCommandHandler();
            commandHandler.Handle(command);
        }
    }
}