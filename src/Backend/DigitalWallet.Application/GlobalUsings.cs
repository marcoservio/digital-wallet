global using AutoMapper;

global using DigitalWallet.Application.Services.AutoMapper;
global using DigitalWallet.Application.SharedValidators;
global using DigitalWallet.Application.UseCases.Login.DoLogin;
global using DigitalWallet.Application.UseCases.Transfer.Filter;
global using DigitalWallet.Application.UseCases.Transfer.Register;
global using DigitalWallet.Application.UseCases.User.Register;
global using DigitalWallet.Application.UseCases.Wallet.Balance.Add;
global using DigitalWallet.Application.UseCases.Wallet.Balance.Get;
global using DigitalWallet.Communication.Requests;
global using DigitalWallet.Communication.Responses;
global using DigitalWallet.Domain.Dtos;
global using DigitalWallet.Domain.Entities;
global using DigitalWallet.Domain.Extensions;
global using DigitalWallet.Domain.Repositories;
global using DigitalWallet.Domain.Repositories.Transaction;
global using DigitalWallet.Domain.Repositories.User;
global using DigitalWallet.Domain.Repositories.Wallet;
global using DigitalWallet.Domain.Security.Cryptography;
global using DigitalWallet.Domain.Security.Tokens;
global using DigitalWallet.Domain.Services.Caching;
global using DigitalWallet.Domain.Services.LoggedUser;
global using DigitalWallet.Exceptions;
global using DigitalWallet.Exceptions.ExceptionsBase;

global using FluentValidation;
global using FluentValidation.Results;
global using FluentValidation.Validators;

global using Microsoft.Extensions.DependencyInjection;
