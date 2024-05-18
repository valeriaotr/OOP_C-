using OOP_ICT.Fifth.Constants;
using OOP_ICT.Fifth.EntityCreatingBeforeStart;
using OOP_ICT.Fifth.Messages;
using OOP_ICT.Second.Models;
using Spectre.Console;

namespace OOP_ICT.Fifth.BankMenuUI;

public class TopUpBankAccountCommandHandler
{
    public void CheckIfPlayerWantToAddMoney(Player player)
    {
        var message = $"{player.GetPlayerName()}'s bank account balance = 0.";
        var color = "yellow";
        MessageGenerator.DisplayMessage(color, message);
        AnsiConsole.WriteLine();
        
        var selection = new SelectionPrompt<string>()
            .Title(ConstantStrings.SuggestionToTopUpBankAccount)
            .PageSize(ConstantNumbers.PageSize);

        selection.AddChoice(ConstantStrings.TopUpBankAccount);
        selection.AddChoice(ConstantStrings.Exit);

        var choice = AnsiConsole.Prompt(selection);
        var topUpBankAccountCommand = new TopUpBankAccountCommand { Player = player, Choice = choice };
        Handle(topUpBankAccountCommand);
    }
    
    public void Handle(TopUpBankAccountCommand command)
    {
        switch (command.Choice)
        {
            case ConstantStrings.TopUpBankAccount:
                do
                {
                    var moneyToAddMessage =
                        $"Enter amount of money to add to {command.Player.GetPlayerName()}'s bank account:";
                    var moneyToAddMessageColor = "blue";
                    double moneyToAdd =
                        MessageGenerator.PromptForNumberDouble(moneyToAddMessageColor, moneyToAddMessage);

                    if (moneyToAdd <= 0)
                    {
                        var message = "Please enter a non-negative amount of money to add.";
                        var color = "red";
                        MessageGenerator.DisplayMessage(color, message);
                        continue; 
                    }

                    command.Money = new PayUnit(moneyToAdd);
                    BankCreationMenuView.Banks[ConstantNumbers.IndexOfBankWeAreIn]
                        .AddMoney(command.Player, command.Money);

                    var balanceMessage =
                        $"{command.Player.GetPlayerName()}'s bank account balance is now {moneyToAdd}.";
                    var redColor = "red";
                    MessageGenerator.DisplayMessage(redColor, balanceMessage);
                    break; 
                } while (true);
                break;
            case ConstantStrings.Exit:
                Environment.Exit(0);
                return;
        }
    }
}