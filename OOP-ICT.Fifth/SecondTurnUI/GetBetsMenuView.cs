using OOP_ICT.Fifth.Constants;
using OOP_ICT.Fifth.DbContext;
using OOP_ICT.Fifth.LastTurnUI;
using OOP_ICT.Fifth.MainMenuUI;
using OOP_ICT.Fifth.Messages;
using OOP_ICT.Second.Models;
using Spectre.Console;

namespace OOP_ICT.Fifth.SecondTurnUI;

public class GetBetsMenuView
{
    public static List<Player> PlayersWithBets;
    public static List<Player> PlayersWithChoices;
    public static List<int> Bets;

    public GetBetsMenuView()
    {
        PlayersWithBets = new List<Player>();
        PlayersWithChoices = new List<Player>();
        Bets = new List<int>();
    }

    public static List<int> GetSortedBets()
    {
        Bets.Sort();
        return Bets;
    }
    
    public static int GetBiggestBet()
    {
        Bets.Sort();
        return Bets[Bets.Count - 1];
    }

    public void DisplayMenu(GetBetCommand command)
    {
        var message = "Now we need to get your bets!";
        var color = "green";
        MessageGenerator.DisplayMessage(color, message);
        AnsiConsole.WriteLine();

        while (PlayersWithBets.Count < MainMenuCommandHandler.Games[ConstantNumbers.IndexOfGameWeAreIn].SeePlayersPlaying().Count)
        {
            var menuName = "BET MENU:";
            var menuColor = "cyan";
            MessageGenerator.DisplayMessage(menuColor, menuName);
            
            var selection = new SelectionPrompt<string>()
                .Title($"What would {command.Player.GetPlayerName()} like to do?")
                .PageSize(ConstantNumbers.PageSize);

            selection.AddChoice(ConstantStrings.Bet);
            selection.AddChoice(ConstantStrings.Fold);
            selection.AddChoice(ConstantStrings.Exit);

            var choice = AnsiConsole.Prompt(selection);
            command.Choice = choice;
            var commandHandler = new GetBetCommandHandler();
            commandHandler.Handle(command);

            // move to the next player
            command.Player = GetNextPlayer(command.Player);
        }
        
        CheckIfSomeoneRaised();
        
        if (MainMenuCommandHandler.Games[ConstantNumbers.IndexOfGameWeAreIn].SeePlayersPlaying().Count == PlayersWithBets.Count)
        {
            foreach (var player in MainMenuCommandHandler.Games[ConstantNumbers.IndexOfGameWeAreIn].SeePlayersPlaying())
            {
                var balanceMessage =
                    $"Now {player.GetPlayerName()}'s casino account balance is {player.GetCasinoAccount().GetBalance()}";
                var balanceMessageColor = "green";
                MessageGenerator.DisplayMessage(balanceMessageColor, balanceMessage);
            }
        }

        if (MainMenuCommandHandler.Games[ConstantNumbers.IndexOfGameWeAreIn].GetNumberOfCardsOnTable() == ConstantNumbers.NumberOfCardsToDisplayThirdTurnMenu && PlayersWithBets.Count == MainMenuCommandHandler.Games[0].SeePlayersPlaying().Count)
        {
            var thirdTurn = new ThirdTurnUI.ThirdTurnMenuView();
            thirdTurn.DisplayMenu();
        }

        if (MainMenuCommandHandler.Games[ConstantNumbers.IndexOfGameWeAreIn].GetNumberOfCardsOnTable() == ConstantNumbers.NumberOfCardsToDisplayLastTurnMenu &&
            PlayersWithBets.Count == MainMenuCommandHandler.Games[ConstantNumbers.IndexOfGameWeAreIn].SeePlayersPlaying().Count)
        {
            var lastTurn = new LastMenuView();
            lastTurn.DisplayMenu();
        }

        if (MainMenuCommandHandler.Games[ConstantNumbers.IndexOfGameWeAreIn].GetNumberOfCardsOnTable() == ConstantNumbers.NumberOfCardsToSeeWinner &&
            PlayersWithBets.Count == MainMenuCommandHandler.Games[ConstantNumbers.IndexOfGameWeAreIn].SeePlayersPlaying().Count)
        {
            var seeWinnerMenu = new SeeWinnerMenuView();
            seeWinnerMenu.DisplayMenu();
        }
    }

    private void CheckIfSomeoneRaised()
    {
        if (PlayersWithBets.Any(player => player.BetAmount < GetBiggestBet()))
        {
            var message = "Someone raised the bet! Check if you want to call.";
            var color = "yellow";
            MessageGenerator.DisplayMessage(color, message);
            foreach (var player in PlayersWithBets)
            {
                if (PlayersWithChoices.Contains(player))
                {
                    UpdatePlayerChoiceInfo(player, ConstantStrings.Raise);
                    UpdatePlayerBets(player, GetBiggestBet());
                    var db = new DbRepository();
                    db.AddLog(player);
                }
                if (!PlayersWithChoices.Contains(player))
                {
                    // ask each player if they want to call the raised bet
                    var callOrFoldMenu = new CallBetOrFoldMenu();
                    var callOrFoldCommand = new CallBetOrFoldCommand
                    {
                        Player = player,
                        Bet = GetBiggestBet()
                    };
                    callOrFoldMenu.DisplayMenu(callOrFoldCommand);
                }
            }
        }
    }
     
    private Player GetNextPlayer(Player currentPlayer)
    {
        var players = MainMenuCommandHandler.Games[ConstantNumbers.IndexOfGameWeAreIn]
            .SeePlayersPlaying();
        var currentIndex = players.FindIndex(player => player == currentPlayer);
        
        var nextIndex = (currentIndex + ConstantNumbers.IndexToGetNextPlayer) % players.Count;
        return players[nextIndex];
    }
    
    private void UpdatePlayerChoiceInfo(Player player, string choice)
    {
        if (MainMenuCommandHandler.Games[ConstantNumbers.IndexOfGameWeAreIn].GetTable().Count == 
            ConstantNumbers.NumberOfCardsToDisplayThirdTurnMenu)
        {
            player.FirstChoice = choice;
        }

        else if (MainMenuCommandHandler.Games[ConstantNumbers.IndexOfGameWeAreIn].GetTable().Count == 
            ConstantNumbers.NumberOfCardsToDisplayLastTurnMenu)
        {
            player.SecondChoice = choice;
        }

        else if (MainMenuCommandHandler.Games[ConstantNumbers.IndexOfGameWeAreIn].GetTable().Count == ConstantNumbers.NumberOfCardsToSeeWinner)
        {
            player.ThirdChoice = choice;
        }
    }

    private void UpdatePlayerBets(Player player, int bet)
    {
        if (MainMenuCommandHandler.Games[ConstantNumbers.IndexOfGameWeAreIn].GetTable().Count == ConstantNumbers.NumberOfCardsToDisplayThirdTurnMenu)
        {
            player.FirstBet = bet;
        }
        else if (MainMenuCommandHandler.Games[ConstantNumbers.IndexOfGameWeAreIn].GetTable().Count == ConstantNumbers.NumberOfCardsToDisplayLastTurnMenu)
        {
            player.SecondBet = bet;
        }
        else if (MainMenuCommandHandler.Games[ConstantNumbers.IndexOfGameWeAreIn].GetTable().Count == ConstantNumbers.NumberOfCardsToSeeWinner)
        {
            player.ThirdBet = bet;
        }
    }
}