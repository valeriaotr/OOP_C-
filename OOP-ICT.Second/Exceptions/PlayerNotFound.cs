namespace OOP_ICT.Second.Exceptions;

public class PlayerNotFound : Exception
{
    public PlayerNotFound()
    {
    }

    public PlayerNotFound(string message) : base(message)
    {
    }
}