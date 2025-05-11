global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;
global using System.Text;
global using System.Text.Json;
global using System.Text.Json.Serialization;

global using DigitalWallet.Domain.Dtos;
global using DigitalWallet.Domain.Entities;
global using DigitalWallet.Domain.Entities.Base;
global using DigitalWallet.Domain.Extensions;
global using DigitalWallet.Domain.Repositories;
global using DigitalWallet.Domain.Repositories.Transaction;
global using DigitalWallet.Domain.Repositories.User;
global using DigitalWallet.Domain.Repositories.Wallet;
global using DigitalWallet.Domain.Security.Cryptography;
global using DigitalWallet.Domain.Security.Tokens;
global using DigitalWallet.Domain.Services.Caching;
global using DigitalWallet.Domain.Services.LoggedUser;
global using DigitalWallet.Infrastructure.DataAccess;
global using DigitalWallet.Infrastructure.DataAccess.Repositories;
global using DigitalWallet.Infrastructure.EntitiesConfiguration.Base;
global using DigitalWallet.Infrastructure.Extensions;
global using DigitalWallet.Infrastructure.Security.Cryptography;
global using DigitalWallet.Infrastructure.Security.Tokens.Generator;
global using DigitalWallet.Infrastructure.Security.Tokens.Validator;
global using DigitalWallet.Infrastructure.Services.Caching;
global using DigitalWallet.Infrastructure.Services.LoggedUser;

global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.Extensions.Caching.Distributed;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging;
global using Microsoft.IdentityModel.Tokens;

global using StackExchange.Redis;
