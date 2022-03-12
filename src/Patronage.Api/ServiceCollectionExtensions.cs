using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;

namespace Patronage.Api
{
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
                    Server = Environment.GetEnvironmentVariable("EMAIL_SERVER"),
                    Port = Int32.Parse(Environment.GetEnvironmentVariable("EMAIL_PORT") ?? throw new Exception("Could not parse EMAIL_PORT to integer. EMAIL_PORT is null.")),
                    SenderName = Environment.GetEnvironmentVariable("EMAIL_SENDERNAME"),
                    SenderEmail = Environment.GetEnvironmentVariable("EMAIL_SENDEREMAIL"),
                    Account = Environment.GetEnvironmentVariable("EMAIL_ACCOUNT"),
                    Password = Environment.GetEnvironmentVariable("EMAIL_PASSWORD"),
                    Security = Environment.GetEnvironmentVariable("EMAIL_SECURITY") == "true"
                };
            }
            else
            {
                mailKitOptions = configuration.GetSection("Email").Get<MailKitOptions>();
            }

            return mailKitOptions;
        }
    }
}
