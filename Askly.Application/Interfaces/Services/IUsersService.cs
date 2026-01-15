namespace Askly.Application.Interfaces.Services;

public interface IUsersService
{
    Task Register(string userName, string email, string password);
}