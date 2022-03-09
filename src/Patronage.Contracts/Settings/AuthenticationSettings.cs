namespace Patronage.Contracts.Settings
{
    public static class AuthenticationSettings
    {
        public const string Audience = "https://localhost:7009/"; // TODO: KD URL for localhost and different for Heroku
        public const  string Issuer = "https://localhost:7009/";
        public const string SecretKey = "This_is_a_security_key";
    }
}
