using OOP_ICT.Second.Models;

namespace OOP_ICT.Second.PersonalAccounts;

public interface IAccount<T>
{ 
    public T GetBalance();
    public void ReplenishBalance(T money);
    public void DeductFromBalance(T money);
}