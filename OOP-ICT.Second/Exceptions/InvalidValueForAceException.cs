namespace OOP_ICT.Second.Exceptions;

public class InvalidValueForAceException : Exception
{
    public InvalidValueForAceException()
    {
    }

    public InvalidValueForAceException(string message) : base(message)
    {
    }
}