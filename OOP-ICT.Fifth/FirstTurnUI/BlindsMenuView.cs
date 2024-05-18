using OOP_ICT.Fifth.Constants;
using OOP_ICT.Fifth.MainMenuUI;
using OOP_ICT.Fifth.Messages;
using OOP_ICT.Second.Models;
using Spectre.Console;

namespace OOP_ICT.Fifth.FirstTurnUI;

public class BlindsMenuView
{
    public static List<Player> PlayersWithChoices = new List<Player>();
    public static List<Player> PlayerWithSmallBlind = new List<Player>();

    public void GetPlayersWithRequiredBlinds()
    {
        var firstMessage = "Before laying 3 cards on table we need to get blinds";
        var secondMessage = "Two players HAVE TO put blinds, all the others will have choice";
        var messageColor = "green";
        
        MessageGenerator.DisplayMessage(messageColor, firstMessage);
        MessageGenerator.DisplayMessage(messageColor, secondMessage);
        AnsiConsole.WriteLine();

        var firstPlayer =
            MainMenuCommandHandler.Games[ConstantNumbers.IndexOfGameWeAreIn].SeePlayersPlaying()[
                ConstantNumbers.IndexOfFirstPlayerInList];
        var secondPlayer =
            MainMenuCommandHandler.Games[ConstantNumbers.IndexOfGameWeAreIn].SeePlayersPlaying()[
                ConstantNumbers.IndexOfSecondPlayerInList];
        
        PlayersWithChoices.Add(secondPlayer);
        PlayerWithSmallBlind.Add(firstPlayer);

        var smallBlindPlayerMessage = $"{firstPlayer.GetPlayerName()} has small blind.";
        var bigBlindPlayerMessage = $"{secondPlayer.GetPlayerName()} has big blind.";
        var blindsMessageColor = "red";
        
        MessageGenerator.DisplayMessage(blindsMessageColor, smallBlindPlayerMessage);
        MessageGenerator.DisplayMessage(blindsMessageColor, bigBlindPlayerMessage);
    }
    
    public void DisplayBlindsMenu()
    {
        GetPlayersWithRequiredBlinds();
        while (true)
        {
            var menuName = "BLINDS MENU:";
            var menuColor = "cyan";
            MessageGenerator.DisplayMessage(menuColor, menuName);
            
            var selection = new SelectionPrompt<string>()
                .Title(ConstantStrings.QuestionBeforeStart)
                .PageSize(ConstantNumbers.PageSize);

            selection.AddChoice(ConstantStrings.GetBlinds);
            selection.AddChoice(ConstantStrings.Exit);

            var choice = AnsiConsole.Prompt(selection);

            var command = new GetBlindsCommand { PlayOrExit = choice };
            var commandHandler = new GetBlindsCommandHandler();
            commandHandler.Handle(command);
        }
    }
}