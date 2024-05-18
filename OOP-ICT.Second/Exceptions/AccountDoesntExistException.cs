namespace OOP_ICT.Second.Exceptions;

public class AccountDoesntExistException : Exception
{
    public AccountDoesntExistException()
    {
    }

    public AccountDoesntExistException(string message) : base(message)
    {
    }
}