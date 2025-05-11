global using System.Globalization;
global using System.Text.Json;

global using AutoMapper;

global using Bogus;

global using CommonTestUtilities.Cryptography;

global using DigitalWallet.Application.Services.AutoMapper;
global using DigitalWallet.Communication.Requests;
global using DigitalWallet.Domain.Entities;
global using DigitalWallet.Domain.Repositories.User;
global using DigitalWallet.Domain.Repositories.Wallet;
global using DigitalWallet.Domain.Security.Cryptography;
global using DigitalWallet.Domain.Security.Tokens;
global using DigitalWallet.Domain.Services.Caching;
global using DigitalWallet.Domain.Services.LoggedUser;
global using DigitalWallet.Exceptions;
global using DigitalWallet.Infrastructure.Security.Cryptography;
global using DigitalWallet.Infrastructure.Security.Tokens.Generator;

global using Moq;

global using static System.Text.Json.JsonElement;
