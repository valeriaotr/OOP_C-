namespace OOP_ICT.Second.Exceptions;

public class NotEnoughMoneyException : Exception
{
    public NotEnoughMoneyException()
    {
    }

    public NotEnoughMoneyException(string message) : base(message)
    {
    }
}