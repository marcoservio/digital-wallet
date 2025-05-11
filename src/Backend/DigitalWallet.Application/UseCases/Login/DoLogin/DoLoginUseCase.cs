namespace DigitalWallet.Application.UseCases.Login.DoLogin;

public class DoLoginUseCase(IUserReadOnlyRepository readRepository,
    IPasswordEncripter passwordEncripter,
    IAccessTokenGenerator accessTokenGenerator) : IDoLoginUseCase
{
    private readonly IUserReadOnlyRepository _readRepository = readRepository;
    private readonly IPasswordEncripter _passwordEncripter = passwordEncripter;
    private readonly IAccessTokenGenerator _accessTokenGenerator = accessTokenGenerator;

    public async Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request)
    {
        var user = await _readRepository.GetByEmail(request.Email);

        if (user is null || _passwordEncripter.IsValid(request.Password, user.Password).IsFalse())
            throw new InvalidLoginException();

        var accessToken = _accessTokenGenerator.Generate(user.UserIdentifier);

        return new ResponseRegisteredUserJson()
        {
            Name = user.Name,
            Tokens = new()
            {
                AccessToken = accessToken,
            },
        };
    }
}
