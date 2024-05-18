using OOP_ICT.Fifth.Constants;
using OOP_ICT.Fifth.EntityCreatingBeforeStart;
using OOP_ICT.Fifth.MainMenuUI;
using OOP_ICT.Fifth.Messages;
using OOP_ICT.Fourth.Facades;
using Spectre.Console;

namespace OOP_ICT.Fifth.FirstTurnUI;

public class GetBlindsCommandHandler
{
    public void Handle(GetBlindsCommand command)
    {
        switch (command.PlayOrExit)
        {
            case ConstantStrings.GetBlinds:
                var blindMessage = "Enter the amount of small blind:";
                var blindMessageColor = "blue";
                var blindAmount = MessageGenerator.PromptForNumberInt(blindMessageColor, blindMessage);
                command.SmallBlindAmount = blindAmount;

                var facade =
                    new PokerFacade(
                        MainMenuCommandHandler.Games[ConstantNumbers.IndexOfGameWeAreIn]
                            .GetGameDealer(),
                        CasinoCreationMenuView.Casinos[ConstantNumbers.IndexOfCasinoWeAreIn]);
                MainMenuCommandHandler.Games[ConstantNumbers.IndexOfGameWeAreIn]
                    .GetBlinds(facade, command.SmallBlindAmount);

                var nextMenu = new CallBlindsOrFoldMenu();
                
                // ask player w small blind
                var callOrFoldForPlayerWithSmallBlind = new CallBlindsOrFoldCommand
                {
                    Player = MainMenuCommandHandler.Games[ConstantNumbers.IndexOfGameWeAreIn]
                        .SeePlayersPlaying()[ConstantNumbers.IndexOfFirstPlayerInList],
                    SmallBlind = command.SmallBlindAmount
                };
                nextMenu.DisplayMenu(callOrFoldForPlayerWithSmallBlind);

                // ask players with no blinds
                foreach (var player in MainMenuCommandHandler.Games[ConstantNumbers.IndexOfGameWeAreIn].SeePlayersPlaying().Skip(ConstantNumbers.NumberOfPlayersToSkip))
                {
                    var callOrFoldCommand = new CallBlindsOrFoldCommand
                        { Player = player, SmallBlind = command.SmallBlindAmount };
                    nextMenu.DisplayMenu(callOrFoldCommand);
                }
                
                foreach (var player in MainMenuCommandHandler.Games[ConstantNumbers.IndexOfGameWeAreIn].SeePlayersPlaying())
                {
                    var updatedBalanceMessage =
                        $"Now {player.GetPlayerName()}'s casino account balance is {player.GetCasinoAccount().GetBalance()}";
                    var updatedBalanceMessageColor = "green";
                    MessageGenerator.DisplayMessage(updatedBalanceMessageColor, updatedBalanceMessage);
                }
                break;
            case ConstantStrings.Exit:
                Environment.Exit(0);
                return;
        }
    }
}