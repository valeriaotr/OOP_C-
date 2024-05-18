using OOP_ICT.Fifth.Constants;
using OOP_ICT.Fifth.Messages;
using Spectre.Console;

namespace OOP_ICT.Fifth.MainMenuUI;

public class MainMenuView
{
    public void DisplayMainMenu()
    {
        var helloMessage = "Welcome to Poker Game";
        MessageGenerator.DisplayGameName(helloMessage);

        while (true)
        {
            var menuName = "MAIN MENU:";
            var menuColor = "cyan";
            MessageGenerator.DisplayMessage(menuColor, menuName);
            
            var selection = new SelectionPrompt<string>()
                .Title(ConstantStrings.QuestionBeforeStart)
                .PageSize(ConstantNumbers.PageSize);

            selection.AddChoice(ConstantStrings.CreateGameRoom);
            selection.AddChoice(ConstantStrings.SeeRating);
            selection.AddChoice(ConstantStrings.Exit);

            var choice = AnsiConsole.Prompt(selection);

            var command = new MainMenuCommand { PlayOrExitChoice = choice };
            var commandHandler = new MainMenuCommandHandler();
            commandHandler.Handle(command);
        }
    }
}