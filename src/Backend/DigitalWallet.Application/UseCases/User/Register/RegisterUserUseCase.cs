namespace DigitalWallet.Application.UseCases.User.Register;

public class RegisterUserUseCase(IUserWriteOnlyRepository writeOnlyRepository,
    IUserReadOnlyRepository readOnlyRepository, IMapper mapper,
    IPasswordEncripter passwordEncripter, IUnitOfWork unitOfWork,
    IAccessTokenGenerator accessTokenGenerator, IWalletWriteOnlyRepository walletWriteOnlyRepository) : IRegisterUserUseCase
{
    private readonly IUserWriteOnlyRepository _writeOnlyRepository = writeOnlyRepository;
    private readonly IUserReadOnlyRepository _readOnlyRepository = readOnlyRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IPasswordEncripter _passwordEncripter = passwordEncripter;
    private readonly IAccessTokenGenerator _accessTokenGenerator = accessTokenGenerator;
    private readonly IWalletWriteOnlyRepository _walletWriteOnlyRepository = walletWriteOnlyRepository;

    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
    {
        await Validate(request);

        var user = _mapper.Map<Domain.Entities.User>(request);
        user.Password = _passwordEncripter.Encrypt(request.Password);

        await _writeOnlyRepository.Add(user);

        await _unitOfWork.Commit();

        await CreateWalletForUser(user.Id);

        await _unitOfWork.Commit();

        var accessToken = _accessTokenGenerator.Generate(user.UserIdentifier);

        return new ResponseRegisteredUserJson
        {
            Name = user.Name,
            Tokens = new ResponseTokensJson()
            {
                AccessToken = accessToken,
            },
        };
    }

    private async Task CreateWalletForUser(long userId)
    {
        await _walletWriteOnlyRepository.Add(new Domain.Entities.Wallet
        {
            UserId = userId,
            Balance = 0,
        });
    }

    private async Task Validate(RequestRegisterUserJson request)
    {
        var result = new RegisterUserValidator().Validate(request);

        var emailExist = await _readOnlyRepository.ExistActiveUserWithEmail(request.Email);

        if (emailExist)
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceMessagesException.EMAIL_ALREADY_REGISTERED));

        if (result.IsValid.IsFalse())
            throw new OnValidationException([.. result.Errors.Select(e => e.ErrorMessage)]);
    }
}
