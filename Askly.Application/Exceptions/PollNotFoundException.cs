namespace Askly.Application.Exceptions;

public class PollNotFoundException : ApplicationExceptionBase
{
    public PollNotFoundException(Guid pollId) : 
        base($"Poll with id {pollId} not found") { }
}