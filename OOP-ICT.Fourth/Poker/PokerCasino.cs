using OOP_ICT.Fourth.Facades;
using OOP_ICT.Second.AccountFactories;
using OOP_ICT.Second.Exceptions;
using OOP_ICT.Second.Models;
using OOP_ICT.Second.PersonalAccounts;

namespace OOP_ICT.Fourth.Poker;

public class PokerCasino 
{
    private GeneralCasinoAccount _generalCasino;
    private PersonalCasinoAccount _casinoAccount;
    private Dictionary<string, int> _idByPlayerNameStoreInPokerCasino = new Dictionary<string, int>();

    public PokerCasino(GeneralCasinoAccount generalCasinoAccount)
    {
        _generalCasino = generalCasinoAccount;
    }

    public GeneralCasinoAccount GetGeneralCasino()
    {
        return _generalCasino;
    }
    
    public bool CasinoAccountExists(string name, int playerId)
    {
        if (_idByPlayerNameStoreInPokerCasino.TryGetValue(name, out int storedPlayerId))
        {
            return storedPlayerId == playerId;
        }
        return false;
    }
    
    public PersonalCasinoAccount CreateNewCasinoAccount(Player player)
    {
        PersonalCasinoAccountFactory casinoAccountFactory = new PersonalCasinoAccountFactory();
        IAccount<double> casinoAccount = casinoAccountFactory.CreateAccount();
        PersonalCasinoAccount newPersonalCasinoAccount = (PersonalCasinoAccount)casinoAccount;

        string name = player.GetPlayerName();
        int playerId = newPersonalCasinoAccount.GetAccountId();
        
        _idByPlayerNameStoreInPokerCasino.Add(name, playerId);
        player.SetCasinoAccount(newPersonalCasinoAccount);
        return newPersonalCasinoAccount;
    }
    
    public PersonalCasinoAccount GetCasinoAccount(Player player, string name, int playerId)
    {
        if (!CasinoAccountExists(name, playerId))
        {
            throw new AccountDoesntExistException("There is no casino account for this player");
        }
        return _casinoAccount;
    }
    
    public void AddChips(Player player, BankPokerFacade facade, PayUnit chips)
    {
        facade.TopUpCasinoAccount(player, chips);
    }

    public void PayWinnings(Player player, BankPokerFacade facade, int winning)
    {
        facade.PayToPlayer(player, winning);
    }

    public void ChargeLoss(Player player, BankPokerFacade facade, int loss)
    {
        facade.ChargePlayerLoss(player, loss);
    }
    
    public bool IsPossibleToTransferChips(Player player, double chips)
    {
        string name = player.GetPlayerName();
        int playerId = player.GetPlayerId();
        PersonalCasinoAccount account = GetCasinoAccount(player, name, playerId);
        
        if (!CasinoAccountExists(name, playerId))
        {
            throw new AccountDoesntExistException("Account doesn't exist");
        }
        return account.PossibleTransfer(chips);
    }
    
    public void TransferChipsToMoney(Player player, BankPokerFacade facade, double chips)
    {
        facade.TransferChipsToMoney(player, chips);
    }

    public void AllInBet(Player player)
    {
        PersonalCasinoAccount playerCasinoAccount = player.GetCasinoAccount();
        double amountOfChips = playerCasinoAccount.GetBalance();
        playerCasinoAccount.DeductFromBalance(amountOfChips);
        PokerGame.SumOfBets += amountOfChips;
        PokerGame.PlayersWithAllInBet.Add(player);
    }

    public void MakeBet(Player player, BankPokerFacade facade, double chips)
    {
        facade.MakeBet(player, chips);
        PokerGame.SumOfBets += chips;
    }
}