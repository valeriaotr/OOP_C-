using OOP_ICT.Fifth.CasinoMenuUI;
using OOP_ICT.Fifth.Constants;
using OOP_ICT.Fifth.EntityCreatingBeforeStart;
using OOP_ICT.Fifth.MainMenuUI;
using OOP_ICT.Fifth.Messages;
using Spectre.Console;

namespace OOP_ICT.Fifth.BankMenuUI;

public class BankMenuCommandHandler
{
    public void ProcessPlayers()
    {
        ProcessNewPlayers();
        TopUpAccounts();
        var casinoMenu = new CasinoMenuCommandHandler();
        casinoMenu.ProcessPlayers();
    }
    
    public void ProcessNewPlayers()
    {
        foreach (var player in MainMenuCommandHandler.Games[ConstantNumbers.IndexOfGameWeAreIn].SeePlayersPlaying())
        {
            if (!BankCreationMenuView.Banks[ConstantNumbers.IndexOfBankWeAreIn].BankAccountExists(player.GetPlayerName(), player.GetPlayerId()))
            {
                var message = $"Bank account for {player.GetPlayerName()} not found.";
                var color = "yellow";
                MessageGenerator.DisplayMessage(color, message);
                
                AnsiConsole.WriteLine();
                var selection = new SelectionPrompt<string>()
                    .Title(ConstantStrings.QuestionBeforeChoice)
                    .PageSize(ConstantNumbers.PageSize);

                selection.AddChoice(ConstantStrings.CreateBankAccount);
                selection.AddChoice(ConstantStrings.Exit);

                var choice = AnsiConsole.Prompt(selection);

                var bankMenuCommand = new BankMenuCommand { Player = player, Choice = choice };
                Handle(bankMenuCommand);
            }
        }
    }

    public void Handle(BankMenuCommand command)
    {
        switch (command.Choice)
        {
            case ConstantStrings.CreateBankAccount:
                var createBankAccountCommand = new CreateBankAccountCommand { Player = command.Player };
                var createBankAccountHandler = new CreateBankAccountCommandHandler();
                createBankAccountHandler.Handle(createBankAccountCommand);

                var message =
                    $"Bank account for {command.Player.GetPlayerName()} is successfully created! {command.Player.GetPlayerName()} is ready to play";
                var color = "green";
                MessageGenerator.DisplayMessage(color, message);
                AnsiConsole.WriteLine();
                break;
            case ConstantStrings.Exit:
                Environment.Exit(0);
                return;
        }
    }
    
    private void TopUpAccounts()
    {
        foreach (var player in MainMenuCommandHandler.Games[ConstantNumbers.IndexOfGameWeAreIn].SeePlayersPlaying())
        {
            if (BankCreationMenuView.Banks[ConstantNumbers.IndexOfBankWeAreIn].BankAccountExists(player.GetPlayerName(), player.GetPlayerId()))
            {
                var topUpCommandHandler = new TopUpBankAccountCommandHandler();
                topUpCommandHandler.CheckIfPlayerWantToAddMoney(player);
            }
        }
    }
}