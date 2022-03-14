using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;
using Newtonsoft.Json;
using System.Text;

namespace Patronage.Api
{
    public static class ServiceCollectionExtensions 
    {
        public static IServiceCollection AddEmailService(this IServiceCollection services, IConfiguration configuration)
        {
            if (Environment.GetEnvironmentVariable("IS_HEROKU") == "true")
            {
                var mailkitOptions = new MailKitOptions()
                {
                    Server = Environment.GetEnvironmentVariable("EMAIL_SERVER"),
                    Port = Int32.Parse(Environment.GetEnvironmentVariable("EMAIL_PORT") ?? throw new Exception("Could not parse EMAIL_PORT to integer. EMAIL_PORT is null.")),
                    SenderName = Environment.GetEnvironmentVariable("EMAIL_SENDERNAME"),
                    SenderEmail = Environment.GetEnvironmentVariable("EMAIL_SENDEREMAIL"),
                    Account = Environment.GetEnvironmentVariable("EMAIL_ACCOUNT"),
                    Password = Environment.GetEnvironmentVariable("EMAIL_PASSWORD"),
                    Security = Environment.GetEnvironmentVariable("EMAIL_SECURITY") == "true"
                };

                services.AddMailKit(config => config.UseMailKit(mailkitOptions));
            }
            else
            {
                var mailkitOptions = configuration.GetSection("Email").Get<MailKitOptions>();

                services.AddMailKit(config => config.UseMailKit(mailkitOptions));
            }

            return services;
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
