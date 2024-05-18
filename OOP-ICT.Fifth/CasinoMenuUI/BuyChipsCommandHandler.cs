using OOP_ICT.Fifth.Constants;
using OOP_ICT.Fifth.EntityCreatingBeforeStart;
using OOP_ICT.Fifth.Messages;
using OOP_ICT.Fourth.Facades;
using OOP_ICT.Second.Models;
using Spectre.Console;

namespace OOP_ICT.Fifth.CasinoMenuUI;

public class BuyChipsCommandHandler
{
    public void CheckIfPlayerWantsToBuyChips(Player player)
    {
        var message = $"{player.GetPlayerName()}'s casino account balance = 0.";
        var color = "yellow";
        MessageGenerator.DisplayMessage(color, message);
        AnsiConsole.WriteLine();
        
        var selection = new SelectionPrompt<string>()
            .Title(ConstantStrings.QuestionBeforeChoice)
            .PageSize(ConstantNumbers.PageSize);

        selection.AddChoice(ConstantStrings.BuyChips);
        selection.AddChoice(ConstantStrings.Exit);

        var choice = AnsiConsole.Prompt(selection);
        var buyChipsCommand = new BuyChipsCommand { Player = player, Choice = choice };
        Handle(buyChipsCommand);
    }

    public void Handle(BuyChipsCommand command)
    {
        var facade = new BankPokerFacade(BankCreationMenuView.Banks[ConstantNumbers.IndexOfBankWeAreIn],
            CasinoCreationMenuView.Casinos[ConstantNumbers.IndexOfCasinoWeAreIn].GetGeneralCasino(),
            CasinoCreationMenuView.Casinos[ConstantNumbers.IndexOfCasinoWeAreIn]);
        switch (command.Choice)
        {
            case ConstantStrings.BuyChips:
                while (true)
                {
                    var chipsAddMessage =
                        $"Enter amount of chips to add to {command.Player.GetPlayerName()}'s casino account:";
                    var chipsAddMessageColor = "blue";
                    double chipsToBuy = MessageGenerator.PromptForNumberDouble(chipsAddMessageColor, chipsAddMessage);

                    if (chipsToBuy <= 0)
                    {
                        var message = "Please enter a valid positive amount of chips you want to buy.";
                        var color = "red";
                        MessageGenerator.DisplayMessage(color, message);
                        continue;
                    }

                    if (chipsToBuy > command.Player.GetBankAccount().GetBalance().Integer)
                    {
                        var notEnoughBalanceMessage = "Not enough money on bank account to buy this amount of chips.";
                        var notEnoughBalanceMessageColor = "red";

                        var balanceMessage =
                            $"Your bank account balance is {command.Player.GetBankAccount().GetBalance().Integer}.";
                        var balanceMessageColor = "red";

                        MessageGenerator.DisplayMessage(notEnoughBalanceMessageColor, notEnoughBalanceMessage);
                        MessageGenerator.DisplayMessage(balanceMessageColor, balanceMessage);
                        continue;
                    }

                    command.Chips = new PayUnit(chipsToBuy);
                    CasinoCreationMenuView.Casinos[ConstantNumbers.IndexOfCasinoWeAreIn]
                        .AddChips(command.Player, facade, command.Chips);

                    var updatedBalanceMessage =
                        $"{command.Player.GetPlayerName()}'s casino account balance is now {chipsToBuy}.";
                    var updatedBalanceMessageColor = "green";
                    MessageGenerator.DisplayMessage(updatedBalanceMessageColor, updatedBalanceMessage);
                    break;
                }
                break;
            case ConstantStrings.Exit:
                Environment.Exit(0);
                return;
        }
    }
}