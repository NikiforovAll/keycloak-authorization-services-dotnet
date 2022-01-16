namespace Api.Application.Authorization;

public class ForbiddenAccessException : Exception
{
    public ForbiddenAccessException() : base() { }
}
