namespace CommonTestUtilities.Tokens;

public class JwtTokenGeneratorBuilder
{
    public static IAccessTokenGenerator Build() =>
        new JwtTokenGenerator(expirationTimeInMinutes: 1, signingKey: "ttttttttttttttttttttttttttttttttttt");

    public static string TokenExpired() =>
        "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9zaWQiOiJmMDQ4ZWM4ZS00MmYzLTQwMGYtYTg0NC1lMWEzZjNjYjRlNzUiLCJuYmYiOjE3NDY4ODE0MjgsImV4cCI6MTc0Njg4MTcyOCwiaWF0IjoxNzQ2ODgxNDI4fQ.YMYZ9QJ67iwam0Hkanx0GB1quWwXIxWRiDJJKFQa62U";
}
