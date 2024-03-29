﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;
using System.Text.Json;
using Patronage.DataAccess;
using System.Text;

namespace Patronage.Api
{
    public static class EmailVariable
    {
        private const string Server = "EMAIL_SERVER";
        private const string Port = "EMAIL_PORT";
        private const string SenderName = "EMAIL_SENDERNAME";
        private const string SenderEmail = "EMAIL_SENDEREMAIL";
        private const string Account = "EMAIL_ACCOUNT";
        private const string Password = "EMAIL_PASSWORD";
        private const string Security = "EMAIL_SECURITY";

        public static string GetServer() => Server;

        public static string GetPort() => Port;

        public static string GetSenderName() => SenderName;

        public static string GetSenderEmail() => SenderEmail;

        public static string GetAccount() => Account;

        public static string GetPassword() => Password;

        public static string GetSecurity() => Security;
    }

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEmailService(this IServiceCollection services, IConfiguration configuration) =>
            services.AddMailKit(config => config.UseMailKit(GetMailKitOptions(configuration)));

        public static MailKitOptions GetMailKitOptions(IConfiguration configuration)
        {
            MailKitOptions mailKitOptions;

            if (Environment.GetEnvironmentVariable("IS_HEROKU") == "true")
            {
                mailKitOptions = new MailKitOptions()
                {
                    Server = Environment.GetEnvironmentVariable(EmailVariable.GetServer()),
                    Port = Int32.Parse(Environment.GetEnvironmentVariable(EmailVariable.GetPort()) ?? throw new Exception("Could not parse EMAIL_PORT to integer. EMAIL_PORT is null.")),
                    SenderName = Environment.GetEnvironmentVariable(EmailVariable.GetSenderName()),
                    SenderEmail = Environment.GetEnvironmentVariable(EmailVariable.GetSenderEmail()),
                    Account = Environment.GetEnvironmentVariable(EmailVariable.GetAccount()),
                    Password = Environment.GetEnvironmentVariable(EmailVariable.GetPassword()),
                    Security = Environment.GetEnvironmentVariable(EmailVariable.GetSecurity()) == "true"
                };
            }
            else
            {
                mailKitOptions = configuration.GetSection("Email").Get<MailKitOptions>();
            }

            return mailKitOptions;
        }

        public static IServiceCollection AddAuthenticationConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(config =>
            {
                config.SaveToken = true;
                config.RequireHttpsMetadata = false;
                config.Events = new JwtBearerEvents()
                {
                    OnMessageReceived = context =>
                    {
                        if (context.Request.Query.ContainsKey("accessToken"))
                        {
                            context.Token = context.Request.Query["accessToken"];
                        }
                        return Task.CompletedTask;
                    }
                };

                config.Events = new JwtBearerEvents()
                {
                    OnChallenge = async context =>
                    {
                        context.Error = "Invalid JWT access token.";

                        context.ErrorDescription = "This request requires a valid JWT access token to be provided";

                        var response = new BaseResponse<BaseResponseError>
                        {
                            ResponseCode = StatusCodes.Status401Unauthorized,
                            Message = "Authorization failed.",
                            BaseResponseError = new List<BaseResponseError>{ new BaseResponseError(
                                propertyName: context.Request.Path,
                                message: context.ErrorDescription,
                                code: context.Error)
                            }
                        };
                        context.Response.ContentType = "application/json";
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                        await context.Response.CompleteAsync();
                    }
                };

                config.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = configuration["Authentication:Issuer"],
                    ValidAudience = configuration["Authentication:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Authentication:SecretKey"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    RequireExpirationTime = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
            return services;
        }
    }
}