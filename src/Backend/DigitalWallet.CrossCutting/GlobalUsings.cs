global using System.Globalization;
global using System.Text.Json;
global using System.Text.Json.Serialization;
global using System.Text.RegularExpressions;
global using System.Threading.RateLimiting;

global using DigitalWallet.Application;
global using DigitalWallet.Communication.Responses;
global using DigitalWallet.CrossCutting.Converters;
global using DigitalWallet.CrossCutting.Filters;
global using DigitalWallet.CrossCutting.Middleware;
global using DigitalWallet.CrossCutting.Token;
global using DigitalWallet.Domain.Extensions;
global using DigitalWallet.Domain.Repositories.User;
global using DigitalWallet.Domain.Security.Tokens;
global using DigitalWallet.Exceptions;
global using DigitalWallet.Exceptions.ExceptionsBase;
global using DigitalWallet.Infrastructure;
global using DigitalWallet.Infrastructure.DataAccess;
global using DigitalWallet.Infrastructure.Extensions;

global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Diagnostics.HealthChecks;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.Filters;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Diagnostics.HealthChecks;
global using Microsoft.Extensions.Hosting;
global using Microsoft.Extensions.Logging;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.OpenApi.Models;

global using Serilog;
