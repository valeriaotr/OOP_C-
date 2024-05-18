using OOP_ICT.Fifth.ChoiceOfPlayersAmountUI;
using OOP_ICT.Fifth.Constants;
using OOP_ICT.Fifth.DbContext;
using OOP_ICT.Fifth.Messages;
using OOP_ICT.Fourth.Poker;
using Spectre.Console;

namespace OOP_ICT.Fifth.MainMenuUI;

public class MainMenuCommandHandler
{
    public static List<PokerGame> Games = new List<PokerGame>();
    public void Handle(MainMenuCommand command)
    {
        switch (command.PlayOrExitChoice)
        {
            case ConstantStrings.CreateGameRoom:
                PokerGame pokerGame = new PokerGame();
                Games.Add(pokerGame);

                var askNumberOfPlayersMessage = "How many players are going to play?";
                var askNumberOfPlayersMessageColor = "blue";
                var numberOfPlayers =
                    MessageGenerator.PromptForNumberInt(askNumberOfPlayersMessageColor, askNumberOfPlayersMessage);
                var choiceOfPlayersAmountCommand = new ChoiceOfPlayersAmountCommand { NumberOfPlayers = numberOfPlayers };
                var choiceOfPlayersAmountCommandHandler = new ChoiceOfPlayersAmountCommandHandler();
                choiceOfPlayersAmountCommandHandler.Handle(choiceOfPlayersAmountCommand);
                break;
            case ConstantStrings.SeeRating:
                var db = new DbRepository();
                var select = "SELECT * FROM public.casinoplayers ORDER BY winning DESC;";
                //db.SelectAll(select);
                break;
            case ConstantStrings.Exit:
                return;
        }
    }
}