namespace DigitalWallet.Application;

public static class ApplicationDependecyInjection
{
    public static void AddAutoMapper(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapping));
    }

    public static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();

        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();

        services.AddScoped<IGetBalanceWalletUseCase, GetBalanceWalletUseCase>();
        services.AddScoped<IAddBalanceWalletUseCase, AddBalanceWalletUseCase>();

        services.AddScoped<IRegisterTransferUseCase, RegisterTransferUseCase>();
        services.AddScoped<IFilterTransferUseCase, FilterTransferUseCase>();
    }
}
