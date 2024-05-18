using OOP_ICT.Fifth.Constants;
using OOP_ICT.Fifth.EntityCreatingBeforeStart;
using OOP_ICT.Fifth.MainMenuUI;
using OOP_ICT.Fifth.Messages;
using OOP_ICT.Fourth.Facades;
using Spectre.Console;

namespace OOP_ICT.Fifth.SecondTurnUI;

public class GetBetCommandHandler
{
    public void Handle(GetBetCommand command)
    {
        switch (command.Choice)
        {
            case ConstantStrings.Bet:
                var betAskMessage = "Enter your bet:";
                var betAskMessageColor = "blue";
                var bet = MessageGenerator.PromptForNumberInt(betAskMessageColor, betAskMessage);
                command.BetAmount = bet;
                command.Player.BetAmount = bet;
                
                if (command.BetAmount > command.Player.GetCasinoAccount().GetBalance())
                {
                    PrintNotEnoughBalanceException(command);
                }
                
                if (GetBetsMenuView.Bets.Count == 0)
                {
                    MarkThatPlayerMadeChoice(command);
                }

                if (GetBetsMenuView.Bets.Count != 0)
                {
                    if (command.BetAmount < GetBetsMenuView.GetSortedBets()[GetBetsMenuView.Bets.Count-1])
                    {
                        PrintBetRulesException(command);
                    }
                    else if (command.BetAmount >= GetBetsMenuView.GetBiggestBet() && GetBetsMenuView.GetBiggestBet() != 0)
                    {
                        if (GetBetsMenuView.PlayersWithChoices.Count != 0)
                        {
                            AddNewPlayerWithRaisedBet(command);
                        }
                        MarkThatPlayerMadeChoice(command);
                    }
                }
                
                MakeBet(command);
                AddPlayersToListsToKnowTheyBet(command);
                PrintMessageAboutSuccessfulBet(command);
                
                break;
            case ConstantStrings.Fold:
                MainMenuCommandHandler.Games[ConstantNumbers.IndexOfGameWeAreIn]
                    .FoldCards(command.Player);
                DisplayFoldMessage(command);
                break;
            case ConstantStrings.Exit:
                Environment.Exit(0);
                return;
        }
    }

    private void PrintNotEnoughBalanceException(GetBetCommand command)
    {
        var betAskMessage = "Enter your bet:";
        var betAskMessageColor = "blue";
        
        var message = "You can't bet more than you have on your casino account";
        var color = "red";
        MessageGenerator.DisplayMessage(color, message);
                    
        var newBet = MessageGenerator.PromptForNumberInt(betAskMessageColor, betAskMessage);
        command.BetAmount = newBet;
        command.Player.BetAmount = newBet;
    }

    private void PrintBetRulesException(GetBetCommand command)
    {
        var betAskMessage = "Enter your bet:";
        var betAskMessageColor = "blue";
        
        var notEnoughBetMessage = $"You can't bet less than {GetBetsMenuView.GetBiggestBet()}";
        var notEnoughBetMessageColor = "red";
        MessageGenerator.DisplayMessage(notEnoughBetMessageColor, notEnoughBetMessage);
                        
        var newBet = MessageGenerator.PromptForNumberInt(betAskMessageColor, betAskMessage);
        command.BetAmount = newBet;
        command.Player.BetAmount = newBet;
    }

    private void AddNewPlayerWithRaisedBet(GetBetCommand command)
    {
        GetBetsMenuView.PlayersWithChoices.Clear();
        GetBetsMenuView.PlayersWithChoices.Add(command.Player);
        command.Player.BetAmount = command.BetAmount;
    }

    private void MakeBet(GetBetCommand command)
    {
        var facade = new BankPokerFacade(
            BankCreationMenuView.Banks[ConstantNumbers.IndexOfBankWeAreIn],
            CasinoCreationCommandHandler.GetCasino(),
            CasinoCreationMenuView.Casinos[ConstantNumbers.IndexOfCasinoWeAreIn]);

        CasinoCreationMenuView.Casinos[ConstantNumbers.IndexOfCasinoWeAreIn]
            .MakeBet(command.Player, facade, command.BetAmount);
    }

    private void PrintMessageAboutSuccessfulBet(GetBetCommand command)
    {
        var successfulBetMessage = $"{command.Player.GetPlayerName()} bet {command.BetAmount} successfully";
        var successfulBetMessageColor = "green";
        MessageGenerator.DisplayMessage(successfulBetMessageColor, successfulBetMessage);
    }

    private void AddPlayersToListsToKnowTheyBet(GetBetCommand command)
    {
        GetBetsMenuView.PlayersWithBets.Add(command.Player); // added that player have bet
        GetBetsMenuView.Bets.Add(command.BetAmount); // added player's bet
    }

    private void MarkThatPlayerMadeChoice(GetBetCommand command)
    {
        GetBetsMenuView.PlayersWithChoices.Add(command.Player);
        command.Player.BetAmount = command.BetAmount;
    }
    
    private void DisplayFoldMessage(GetBetCommand command) 
    {
        var foldMessage = $"{command.Player.GetPlayerName()} is not in game anymore";
        var foldMessageColor = "red";
        MessageGenerator.DisplayMessage(foldMessageColor, foldMessage);
    }
}