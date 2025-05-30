﻿namespace DigitalWallet.Domain.Extensions;

public static class StringExtension
{
    public static bool NotEmpty([NotNullWhen(true)] this string? value) => string.IsNullOrWhiteSpace(value).IsFalse();

    public static bool Empty(this string? value) => string.IsNullOrWhiteSpace(value);
}
