namespace OOP_ICT.Second.Exceptions;

public class NegativeAmountOfMoneyException : Exception
{
    public NegativeAmountOfMoneyException()
    {
    }

    public NegativeAmountOfMoneyException(string message) : base(message)
    {
    }
}