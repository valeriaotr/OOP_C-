using OOP_ICT.Fifth.Constants;
using OOP_ICT.Fifth.DbContext;
using OOP_ICT.Fifth.EntityCreatingBeforeStart;
using OOP_ICT.Fifth.MainMenuUI;
using OOP_ICT.Fifth.Messages;
using OOP_ICT.Fourth.Facades;
using Spectre.Console;

namespace OOP_ICT.Fifth.SecondTurnUI;

public class CallBetOrFoldCommandHandler
{
    public void Handle(CallBetOrFoldCommand command)
    {
        switch (command.CallOrFold)
        {
            case ConstantStrings.Call:
                UpdatePlayerChoiceInfo(command, ConstantStrings.Call);
                UpdatePlayerBets(command, GetBetsMenuView.GetBiggestBet());
                var db = new DbRepository();
                db.AddLog(command.Player);

                var facade = new BankPokerFacade(
                    BankCreationMenuView.Banks[ConstantNumbers.IndexOfBankWeAreIn],
                    CasinoCreationCommandHandler.GetCasino(),
                    CasinoCreationMenuView.Casinos[ConstantNumbers.IndexOfCasinoWeAreIn]);

                var needToAddToBet =
                    GetBetsMenuView.GetBiggestBet() - command.Player.BetAmount; // find diff between bets

                CasinoCreationMenuView.Casinos[ConstantNumbers.IndexOfCasinoWeAreIn]
                    .MakeBet(command.Player, facade, needToAddToBet);
                GetBetsMenuView.PlayersWithChoices.Add(command.Player);

                var message = $"{command.Player.GetPlayerName()} is in game!";
                var color = "green";
                MessageGenerator.DisplayMessage(color, message);
                break;
            case ConstantStrings.Fold:
                UpdatePlayerChoiceInfo(command, ConstantStrings.Fold);
                var dbase = new DbRepository();
                dbase.AddLog(command.Player);
                MainMenuCommandHandler.Games[ConstantNumbers.IndexOfGameWeAreIn]
                    .FoldCards(command.Player);
                GetBetsMenuView.PlayersWithChoices.Add(command.Player);

                var foldMessage = $"{command.Player.GetPlayerName()} is not participating in this game anymore";
                var foldMessageColor = "red";
                MessageGenerator.DisplayMessage(foldMessageColor, foldMessage);
                break;
            case ConstantStrings.Exit:
                Environment.Exit(0);
                return;
        }
    }

    private void UpdatePlayerChoiceInfo(CallBetOrFoldCommand command, string choice)
    {
        if (MainMenuCommandHandler.Games[ConstantNumbers.IndexOfGameWeAreIn].GetTable().Count == 
            ConstantNumbers.NumberOfCardsToDisplayThirdTurnMenu)
        {
            command.Player.FirstChoice = choice;
        }

        if (MainMenuCommandHandler.Games[ConstantNumbers.IndexOfGameWeAreIn].GetTable().Count == 
            ConstantNumbers.NumberOfCardsToDisplayThirdTurnMenu)
        {
            command.Player.SecondChoice = choice;
        }

        if (MainMenuCommandHandler.Games[ConstantNumbers.IndexOfGameWeAreIn].GetTable().Count == 
            ConstantNumbers.NumberOfCardsToDisplayThirdTurnMenu)
        {
            command.Player.ThirdChoice = choice;
        }
    }

    private void UpdatePlayerBets(CallBetOrFoldCommand command, int bet)
    {
        if (MainMenuCommandHandler.Games[ConstantNumbers.IndexOfGameWeAreIn].GetTable().Count == 
            ConstantNumbers.NumberOfCardsToDisplayThirdTurnMenu)
        {
            command.Player.FirstBet = bet;
        }
        else if (MainMenuCommandHandler.Games[ConstantNumbers.IndexOfGameWeAreIn].GetTable().Count == 
                 ConstantNumbers.NumberOfCardsToDisplayLastTurnMenu)
        {
            command.Player.SecondBet = bet;
        }
        else if (MainMenuCommandHandler.Games[ConstantNumbers.IndexOfGameWeAreIn].GetTable().Count == 
                 ConstantNumbers.NumberOfCardsToSeeWinner)
        {
            command.Player.ThirdBet = bet;
        }
    }
}