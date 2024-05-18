using OOP_ICT.Fifth.Constants;
using OOP_ICT.Fifth.MainMenuUI;
using OOP_ICT.Fifth.Messages;
using OOP_ICT.Fourth.Poker;
using OOP_ICT.Second.PersonalAccounts;
using Spectre.Console;

namespace OOP_ICT.Fifth.EntityCreatingBeforeStart;

public class CasinoCreationCommandHandler
{
    public PokerCasino CreatedCasino;
    public static GeneralCasinoAccount Casino;

    public CasinoCreationCommandHandler()
    {
        Casino = new GeneralCasinoAccount(ConstantNumbers.MaximumPossibleBalanceOfCasino);
    }
        
    public void Handle(CasinoCreationCommand command)
    {
        switch (command.CreateOrExit)
        {
            case ConstantStrings.CreateCasino:
                CreatedCasino = new PokerCasino(Casino);
                CasinoCreationMenuView.Casinos.Add(CreatedCasino);

                var message = "Casino was successfully created!";
                var color = "green";
               MessageGenerator.DisplayMessage(color, message);
                
                var mainMenu = new MainMenuView();
                mainMenu.DisplayMainMenu();
                break;
            case ConstantStrings.Exit:
                Environment.Exit(0);
                return;
        }
    }

    public static GeneralCasinoAccount GetCasino()
    {
        return Casino;
    }
}