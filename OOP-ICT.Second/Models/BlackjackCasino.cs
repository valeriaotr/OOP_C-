using OOP_ICT.Second.AccountFactories;
using OOP_ICT.Second.Exceptions;
using OOP_ICT.Second.PersonalAccounts;

namespace OOP_ICT.Second.Models;

public class BlackjackCasino
{
    private GeneralCasinoAccount _casino;
    private PersonalCasinoAccount _casinoAccount;
    private Dictionary<string, int> _idByPlayerNameStoreInCasino = new Dictionary<string, int>();

    public BlackjackCasino(GeneralCasinoAccount casino)
    {
        _casino = casino;
    }
    
    public bool CasinoAccountExists(string name, int playerId)
    {
        if (_idByPlayerNameStoreInCasino.TryGetValue(name, out int storedPlayerId))
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
        
        _idByPlayerNameStoreInCasino.Add(name, playerId);
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

    public void AddChips(Player player, BankCasinoFacade facade, PayUnit chips)
    {
        facade.TopUpCasinoAccount(player, chips);
    }

    public void PayWinnings(Player player, BankCasinoFacade facade, int winning)
    {
        facade.PayToPlayer(player, winning);
    }

    public void ChargeLoss(Player player, BankCasinoFacade facade, int loss)
    {
        facade.ChargePlayerLoss(player, loss);
    }

    public void HandleBlackjack(Player player, BankCasinoFacade facade, int bet)
    {
        int blackjackPayoutRatio = 2; 
        int winning = bet * blackjackPayoutRatio;
        PayWinnings(player, facade, winning);
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

    public void TransferChipsToMoney(Player player, BankCasinoFacade facade, double chips)
    {
        facade.TransferChipsToMoney(player, chips);
    }
}