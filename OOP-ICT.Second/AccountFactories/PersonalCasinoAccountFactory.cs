using OOP_ICT.Second.Models;
using OOP_ICT.Second.PersonalAccounts;

namespace OOP_ICT.Second.AccountFactories
{
    public class PersonalCasinoAccountFactory : IAccountFactory<double>
    {
        private static int _lastPlayerId;
        private const int StartingBalance = 0;
        public IAccount<double> CreateAccount()
        {
            int playerId = GenerateUniquePlayerId();
            return new PersonalCasinoAccount(playerId, StartingBalance);
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