using System;

namespace APBD_3;

public class OverfillException : Exception
{
    public OverfillException(string message) : base(message)
    {
    }
}