using OOP_ICT.Fifth.Constants;
using OOP_ICT.Fifth.DbContext;
using OOP_ICT.Fifth.EntityCreatingBeforeStart;
using OOP_ICT.Fifth.MainMenuUI;
using OOP_ICT.Fifth.Messages;
using OOP_ICT.Fourth.Combinations;
using OOP_ICT.Fourth.Facades;
using OOP_ICT.Second.Models;
using Spectre.Console;

namespace OOP_ICT.Fifth.LastTurnUI;

public class GetNameOfWinnerCommandHandler
{
    public void Handle(GetNameOfWinnerCommand command)
    {
        switch (command.GetOrExit)
        {
            case ConstantStrings.SeeWinner:
                var facade =
                    new PokerFacade(
                        MainMenuCommandHandler.Games[ConstantNumbers.IndexOfGameWeAreIn]
                            .GetGameDealer(),
                        CasinoCreationMenuView.Casinos[ConstantNumbers.IndexOfCasinoWeAreIn]);
                var bankPokerFacade = new BankPokerFacade(
                    BankCreationMenuView.Banks[ConstantNumbers.IndexOfBankWeAreIn],
                    CasinoCreationCommandHandler.GetCasino(),
                    CasinoCreationMenuView.Casinos[ConstantNumbers.IndexOfCasinoWeAreIn]);
                var winner = MainMenuCommandHandler.Games[ConstantNumbers.IndexOfGameWeAreIn]
                    .DetermineWinner(facade);
                var winning = MainMenuCommandHandler.Games[ConstantNumbers.IndexOfGameWeAreIn]
                    .GetSumOfBets();

                PrintWinnersName(winner);
                PrintWinnersCombination(winner);
                PrintAmountOfWinning(winning);
                PrintUpdatedWinnerBalance(winner);
                
                winner.Winning = winning;
                winner.PlayedGameId = JSONGetter.GetCurrentGameId();
                var dbRepository = new DbRepository();
                dbRepository.AddPlayerToRating(winner);

                MainMenuCommandHandler.Games[ConstantNumbers.IndexOfGameWeAreIn].GiveWinningToPlayer(
                    CasinoCreationMenuView.Casinos[ConstantNumbers.IndexOfCasinoWeAreIn],
                    bankPokerFacade, facade);
                
                // add that players that lose have winning = 0
                foreach (var player in MainMenuCommandHandler.Games[ConstantNumbers.IndexOfGameWeAreIn].SeePlayersPlaying())
                {
                    if (player != winner)
                    {
                        player.Winning = ConstantNumbers.AmountOfWinningIfLoose;
                        player.PlayedGameId = JSONGetter.GetCurrentGameId();
                        dbRepository.AddPlayerToRating(player);
                    }
                }
                
                var jsonSetter = new JSONSetter();
                jsonSetter.UpdateId(); // id has changed
                var mainMenu = new MainMenuView();
                mainMenu.DisplayMainMenu();
                break;
            case ConstantStrings.Exit:
                Environment.Exit(0);
                return;
        }
    }

    private void PrintWinnersName(Player player)
    {
        var message = $"OUR WINNER IS {player.GetPlayerName()}";
        var color = "green";
        MessageGenerator.DisplayMessage(color, message);
        AnsiConsole.WriteLine();
    }

    private void PrintWinnersCombination(Player player)
    {
        var combinationName = Combinations.GetCombinationName(player.GetBestHand());
        var combinationMessage = $"{player.GetPlayerName()} has a combination: {combinationName}";
        var combinationMessageColor = "yellow";
        MessageGenerator.DisplayMessage(combinationMessageColor, combinationMessage);
        AnsiConsole.WriteLine();
    }

    private void PrintAmountOfWinning(double winning)
    {
        var winningMessage = $"Winning is {winning}";
        var winningMessageColor = "green";
        MessageGenerator.DisplayMessage(winningMessageColor, winningMessage);
    }

    private void PrintUpdatedWinnerBalance(Player player)
    {
        var newBalanceMessage =
            $"{player.GetPlayerName()}'s casino account balance is now {player.GetCasinoAccount().GetBalance()}";
        var newBalanceMessageColor = "yellow";
        MessageGenerator.DisplayMessage(newBalanceMessageColor, newBalanceMessage);
    }
}