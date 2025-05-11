using DigitalWallet.Domain.Entities;

namespace DigitalWallet.Domain.Services.LoggedUser;

public interface ILoggedUser
{
    Task<User> User();
}
