using System.Reflection.Metadata;
using OOP_ICT.Fifth.Constants;
using OOP_ICT.Fifth.Messages;
using OOP_ICT.Second.Models;
using Spectre.Console;

namespace OOP_ICT.Fifth.EntityCreatingBeforeStart;

public class BankCreationCommandHandler
{
    public Bank CreatedBank;
    public void Handle(BankCreationCommand command)
    {
        switch (command.CreateOrExit)
        {
            case ConstantStrings.CreateBank:
                CreatedBank = new Bank();
                BankCreationMenuView.Banks.Add(CreatedBank);

                var message = "Bank was successfully created!";
                var color = "green";
                MessageGenerator.DisplayMessage(color, message);

                var casinoMenu = new CasinoCreationMenuView();
                casinoMenu.DisplayOrNot();
                break;
            case ConstantStrings.Exit:
                Environment.Exit(0);
                return;
        }
    }
}