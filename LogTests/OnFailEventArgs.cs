using System;

namespace LogTests;

public class OnFailEventArgs : EventArgs
{
    public string Message { get; }

    public OnFailEventArgs(string message)
    {
        Message = message;
    }
}