using OOP_ICT.Fifth.Constants;
using OOP_ICT.Fifth.Messages;
using Spectre.Console;

namespace OOP_ICT.Fifth.StartGameUI;

public class StartGameMenuView
{
    public void DisplayStartGameMenu()
    {
        var message = "ARE YOU READY TO START A GAME???";
        var color = "red";
        MessageGenerator.DisplayMessage(color, message);

        while (true)
        {
            var menuName = "GAME MENU:";
            var menuColor = "cyan";
            MessageGenerator.DisplayMessage(menuColor, menuName);
            
            var selection = new SelectionPrompt<string>()
                .Title(ConstantStrings.QuestionBeforeStart)
                .PageSize(10);

            selection.AddChoice(ConstantStrings.StartGame);
            selection.AddChoice(ConstantStrings.Exit);

            var choice = AnsiConsole.Prompt(selection);

            var command = new StartGameCommand { PlayOrExit = choice };
            var commandHandler = new StartGameCommandHandler();
            commandHandler.Handle(command);
        }
    }
}