using OOP_ICT.Fifth.Constants;
using OOP_ICT.Fifth.EntityCreatingBeforeStart;
using OOP_ICT.Fifth.MainMenuUI;
using OOP_ICT.Fifth.Messages;
using OOP_ICT.Fourth.Facades;
using Spectre.Console;

namespace OOP_ICT.Fifth.FirstTurnUI;

public class CallBlindsOrFoldCommandHandler
{
    public void Handle(CallBlindsOrFoldCommand command)
    {
        switch (command.CallOrFold)
        {
            case ConstantStrings.Call:
                var facade =
                    new PokerFacade(
                        MainMenuCommandHandler.Games[ConstantNumbers.IndexOfGameWeAreIn]
                            .GetGameDealer(),
                        CasinoCreationMenuView.Casinos[ConstantNumbers.IndexOfCasinoWeAreIn]);

                var bankFacade = new BankPokerFacade(
                    BankCreationMenuView.Banks[ConstantNumbers.IndexOfBankWeAreIn],
                    CasinoCreationCommandHandler.GetCasino(),
                    CasinoCreationMenuView.Casinos[ConstantNumbers.IndexOfCasinoWeAreIn]);

                if (command.Player.GetCasinoAccount().GetBalance() >= command.SmallBlind * ConstantNumbers.DifferenceBetweenBlinds)
                {
                    MainMenuCommandHandler.Games[ConstantNumbers.IndexOfGameWeAreIn]
                        .CallBlind(facade, command.Player, command.SmallBlind);
                }

                if (command.Player.GetCasinoAccount().GetBalance() < command.SmallBlind * ConstantNumbers.DifferenceBetweenBlinds)
                {
                    CasinoCreationMenuView.Casinos[ConstantNumbers.IndexOfCasinoWeAreIn]
                        .AllInBet(command.Player);
                }
                
                // if person already bet small blind but wants to call, we give them back 1/2 of big blind
                if (BlindsMenuView.PlayerWithSmallBlind.Contains(command.Player))
                {
                    CasinoCreationMenuView.Casinos[ConstantNumbers.IndexOfCasinoWeAreIn]
                        .PayWinnings(command.Player, bankFacade, command.SmallBlind);
                }

                var callMessage = $"{command.Player.GetPlayerName()} is in game!";
                var callColor = "green";
                MessageGenerator.DisplayMessage(callColor, callMessage);
                break;
            case ConstantStrings.Fold:
                MainMenuCommandHandler.Games[ConstantNumbers.IndexOfGameWeAreIn]
                    .FoldCards(command.Player);
                var foldMessage = $"{command.Player.GetPlayerName()} is not participating in this game anymore.";
                var foldColor = "red";
                MessageGenerator.DisplayMessage(foldColor, foldMessage);
                break;
            case ConstantStrings.Exit:
                Environment.Exit(0);
                return;
        }
    }
}