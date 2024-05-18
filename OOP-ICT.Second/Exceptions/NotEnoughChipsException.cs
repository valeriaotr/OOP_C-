namespace OOP_ICT.Second.Exceptions;

public class NotEnoughChipsException : Exception
{
    public NotEnoughChipsException()
    {
    }

    public NotEnoughChipsException(string message) : base(message)
    {
    }
}