using OOP_ICT.Second.Models;
using OOP_ICT.Second.PersonalAccounts;

namespace OOP_ICT.Second.AccountFactories
{
    public interface IAccountFactory<T>
    {
        public IAccount<T> CreateAccount();
        public int GenerateUniquePlayerId();
    }
}


