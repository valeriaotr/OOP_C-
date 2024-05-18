using OOP_ICT.Fifth.Constants;
using OOP_ICT.Fifth.Messages;
using Spectre.Console;

namespace OOP_ICT.Fifth.SecondTurnUI;

public class SecondTurnMenuView
{
    public void DisplayMenu()
    {
        var message = "Now we can lay 3 cards on table";
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

            selection.AddChoice(ConstantStrings.LayThreeCards);
            selection.AddChoice(ConstantStrings.Exit);

            var choice = AnsiConsole.Prompt(selection);

            var command = new LayThreeCardsOnTableCommand { LayOrExit = choice };
            var commandHandler = new LayThreeCardsOnTableCommandHandler();
            commandHandler.Handle(command);
        }
    }
}