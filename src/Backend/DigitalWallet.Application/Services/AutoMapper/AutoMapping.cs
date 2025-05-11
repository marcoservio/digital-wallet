namespace DigitalWallet.Application.Services.AutoMapper;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToDomain();
        RequestToDto();
        DomainToResponse();
    }

    private void RequestToDomain()
    {
        CreateMap<RequestRegisterUserJson, User>()
            .ForMember(dest => dest.Password, opt => opt.Ignore());
    }

    private void RequestToDto()
    {
        CreateMap<RequestFilterTransferJson, FilterTransferDto>();
    }

    private void DomainToResponse()
    {
        CreateMap<User, ResponseUserProfileJson>();

        CreateMap<Transaction, ResponseTransferJson>()
            .ForMember(dest => dest.ToWallet, opt => opt.MapFrom(source => source.ToWallet!.WalletKey))
            .ForMember(dest => dest.FromWallet, opt => opt.MapFrom(source => source.FromWallet!.WalletKey))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(source => nameof(source.Status)))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(source => source.CreatedAt));

        CreateMap<Wallet, ResponseBalanceJson>();
    }
}
