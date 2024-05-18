using OOP_ICT.Fifth.Constants;
using OOP_ICT.Fifth.MainMenuUI;
using OOP_ICT.Fifth.Messages;
using OOP_ICT.Fourth.Poker;
using Spectre.Console;

namespace OOP_ICT.Fifth.EntityCreatingBeforeStart;

public class CasinoCreationMenuView
{
    public static List<PokerCasino> Casinos = new List<PokerCasino>();
    
    public void DisplayOrNot()
    {
        if (Casinos.Count > 0)
        {
            // if we already have bank and casino we display main menu
            MainMenuView mainMenu = new MainMenuView();
            mainMenu.DisplayMainMenu();
        }
        DisplayCasinoCreationMenu();
    }
    
    public void DisplayCasinoCreationMenu()
    {
        var message = "Before starting a game we need to create casino.";
        var color = "red";
        MessageGenerator.DisplayMessage(color, message);

        while (true)
        {
            var menuName = "CASINO CREATION MENU:";
            var menuColor = "cyan";
            MessageGenerator.DisplayMessage(menuColor, menuName);
            
            var selection = new SelectionPrompt<string>()
                .Title(ConstantStrings.QuestionBeforeStart)
                .PageSize(ConstantNumbers.PageSize);

            selection.AddChoice(ConstantStrings.CreateCasino);
            selection.AddChoice(ConstantStrings.Exit);

            var choice = AnsiConsole.Prompt(selection);

            var command = new CasinoCreationCommand { CreateOrExit = choice };
            var commandHandler = new CasinoCreationCommandHandler();
            commandHandler.Handle(command);
        }
    }
}