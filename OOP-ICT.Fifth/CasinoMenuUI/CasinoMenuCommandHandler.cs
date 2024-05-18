using OOP_ICT.Fifth.Constants;
using OOP_ICT.Fifth.EntityCreatingBeforeStart;
using OOP_ICT.Fifth.MainMenuUI;
using OOP_ICT.Fifth.Messages;
using OOP_ICT.Fifth.StartGameUI;
using Spectre.Console;

namespace OOP_ICT.Fifth.CasinoMenuUI;

public class CasinoMenuCommandHandler
{
    public void ProcessPlayers()
    {
        ProcessPlayersInCasino();
        Buychips();

        var startGameMenu = new StartGameMenuView();
        startGameMenu.DisplayStartGameMenu();
    }

    public void ProcessPlayersInCasino()
    {
        foreach (var player in MainMenuCommandHandler.Games[ConstantNumbers.IndexOfGameWeAreIn].SeePlayersPlaying())
        {
            if (!CasinoCreationMenuView.Casinos[ConstantNumbers.IndexOfCasinoWeAreIn].CasinoAccountExists(player.GetPlayerName(), player.GetPlayerId()))
            {
                var message = $"Casino account for {player.GetPlayerName()} not found.";
                var color = "yellow";
                MessageGenerator.DisplayMessage(color, message);
                AnsiConsole.WriteLine();
                
                var selection = new SelectionPrompt<string>()
                    .Title(ConstantStrings.QuestionBeforeChoice)
                    .PageSize(ConstantNumbers.PageSize);

                selection.AddChoice(ConstantStrings.CreateCasinoAccount);
                selection.AddChoice(ConstantStrings.Exit);

                var choice = AnsiConsole.Prompt(selection);

                var casinoMenuCommand = new CasinoMenuCommand { Player = player, Choice = choice};
                Handle(casinoMenuCommand);
            }
        }
    }

    private void Handle(CasinoMenuCommand command)
    {
        switch (command.Choice)
        {
            case ConstantStrings.CreateCasinoAccount:
                var createCasinoAccountCommand = new CreateCasinoAccountCommand { Player = command.Player };
                var createCasinoAccountCommandHandler = new CreateCasinoAccountCommandHandler();
                createCasinoAccountCommandHandler.Handle(createCasinoAccountCommand);

                var message =
                    $"Casino account for {command.Player.GetPlayerName()} is successfully created! {command.Player.GetPlayerName()} is ready to play.";
                var color = "green";
                MessageGenerator.DisplayMessage(color, message);
                AnsiConsole.WriteLine();
                break;
            case ConstantStrings.Exit:
                Environment.Exit(0);
                return;
        }
    }

    private void Buychips()
    {
        var buyChipsCommandHandler = new BuyChipsCommandHandler();
        foreach (var player in MainMenuCommandHandler.Games[ConstantNumbers.IndexOfGameWeAreIn].SeePlayersPlaying())
        {
            if (CasinoCreationMenuView.Casinos[ConstantNumbers.IndexOfCasinoWeAreIn].CasinoAccountExists(player.GetPlayerName(), player.GetPlayerId()))
            {
                buyChipsCommandHandler.CheckIfPlayerWantsToBuyChips(player);
            }
        }
    }
}