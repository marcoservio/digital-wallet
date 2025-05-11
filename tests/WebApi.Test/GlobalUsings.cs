global using System.Collections;
global using System.Net;
global using System.Net.Http.Headers;
global using System.Net.Http.Json;
global using System.Text.Json;

global using CommonTestUtilities.Entities;
global using CommonTestUtilities.ErrorMessage;
global using CommonTestUtilities.Requests;
global using CommonTestUtilities.Tokens;

global using DigitalWallet.Communication.Requests;
global using DigitalWallet.Domain.Extensions;
global using DigitalWallet.Infrastructure.DataAccess;

global using FluentAssertions;

global using Microsoft.AspNetCore.Hosting;
global using Microsoft.AspNetCore.Mvc.Testing;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.DependencyInjection;

global using WebApi.Test.InlineData;
