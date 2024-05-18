using OOP_ICT.Second.Models;
using OOP_ICT.Second.PersonalAccounts;

namespace OOP_ICT.Second.AccountFactories
{
    public class PersonalBankAccountFactory : IAccountFactory<PayUnit>
    {
        private static int _lastPlayerId;
        private PayUnit _startingBalance = new PayUnit(0, 0.00);
        
        public IAccount<PayUnit> CreateAccount()
        {
            int playerId = GenerateUniquePlayerId();
            return new PersonalBankAccount(playerId, _startingBalance);
        }
    
        public int GenerateUniquePlayerId()
        {
            return ++_lastPlayerId;
        }

        public int GetId()
        {
            return _lastPlayerId;
        }
    }
}