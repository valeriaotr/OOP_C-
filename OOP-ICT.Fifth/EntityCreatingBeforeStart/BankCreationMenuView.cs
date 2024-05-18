using OOP_ICT.Fifth.Constants;
using OOP_ICT.Fifth.Messages;
using Spectre.Console;
using OOP_ICT.Second.Models;

namespace OOP_ICT.Fifth.EntityCreatingBeforeStart;

public class BankCreationMenuView
{
    public static List<Bank> Banks = new List<Bank>();
    public void DisplayOrNot()
    {
        if (Banks.Count > 0)
        {
            // if we already have a bank -> we create casino
            CasinoCreationMenuView casinoCreationMenuView = new CasinoCreationMenuView();
            casinoCreationMenuView.DisplayOrNot();
        }
        DisplayBankCreationMenu();
    }
    
    public void DisplayBankCreationMenu()
    {
        var message = "Before starting a game we need to create bank";
        var color = "red";
        MessageGenerator.DisplayMessage(color, message);

        while (true)
        {
            var menuName = "BANK MENU CREATION:";
            var menuColor = "cyan";
            MessageGenerator.DisplayMessage(menuColor, menuName);
            
            var selection = new SelectionPrompt<string>()
                .Title(ConstantStrings.QuestionBeforeStart)
                .PageSize(ConstantNumbers.PageSize);

            selection.AddChoice(ConstantStrings.CreateBank);
            selection.AddChoice(ConstantStrings.Exit);

            var choice = AnsiConsole.Prompt(selection);

            var command = new BankCreationCommand {CreateOrExit = choice};
            var commandHandler = new BankCreationCommandHandler();
            commandHandler.Handle(command);
        }
    }
}