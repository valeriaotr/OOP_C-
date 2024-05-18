namespace OOP_ICT.Second.Exceptions;
using System;

public class NegativeBalanceException : Exception
{
    public NegativeBalanceException()
    {
    }

    public NegativeBalanceException(string message) : base(message)
    {
    }
}