namespace OOP_ICT.Second.Exceptions;

public class PlayerAlreadyAddedException : Exception
{
    public PlayerAlreadyAddedException()
    {
    }

    public PlayerAlreadyAddedException(string message) : base(message)
    {
    }
}