namespace OOP_ICT.Second.Exceptions;

public class NegativeAmountOfChipsException : Exception
{
    public NegativeAmountOfChipsException()
    {
    }

    public NegativeAmountOfChipsException(string message) : base(message)
    {
    }
}