namespace DigitalWallet.Application.UseCases.Transfer.Filter;

public class FilterTransferUseCase(IMapper mapper, ILoggedUser loggedUser, ITransactionReadOnlyRepository transactionReadOnlyRepository, ICacheService cache) : IFilterTransferUseCase
{
    private readonly IMapper _mapper = mapper;
    private readonly ILoggedUser _loggedUser = loggedUser;
    private readonly ITransactionReadOnlyRepository _transactionReadOnlyRepository = transactionReadOnlyRepository;
    private readonly ICacheService _cache = cache;

    public async Task<ResponseTransfersJson> Execute(RequestFilterTransferJson request)
    {
        Validate(request);

        var authenticatedUser = await _loggedUser.User();

        var filter = _mapper.Map<FilterTransferDto>(request);

        var cacheKey = $"filter-transfer-{authenticatedUser.UserIdentifier}-{request.StartDate}-{request.EndDate}";

        var transactions = await _cache.GetAsync<IList<Transaction>>(cacheKey);

        if (transactions is null)
        {
            transactions = await _transactionReadOnlyRepository.Filter(authenticatedUser, filter);

            await _cache.SetAsync(cacheKey, transactions);
        }

        return new ResponseTransfersJson
        {
            Transfers = _mapper.Map<IList<ResponseTransferJson>>(transactions),
        };
    }

    private static void Validate(RequestFilterTransferJson request)
    {
        var result = new FilterTransferValidator().Validate(request);

        if (result.IsValid.IsFalse())
            throw new OnValidationException([.. result.Errors.Select(e => e.ErrorMessage).Distinct()]);
    }
}
