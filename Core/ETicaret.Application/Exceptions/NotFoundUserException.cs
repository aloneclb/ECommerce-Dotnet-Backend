﻿namespace ETicaret.Application.Exceptions;

public class NotFoundUserException : Exception
{
    public NotFoundUserException() : base("Kullanıcı adı/emaili veya şifresi hatalı.")
    {
    }

    public NotFoundUserException(string? message) : base(message)
    {
    }

    public NotFoundUserException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}