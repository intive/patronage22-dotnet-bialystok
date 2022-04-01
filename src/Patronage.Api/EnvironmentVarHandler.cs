namespace Patronage.Api
{
    public static class EnvironmentVarHandler
    {
        public static bool IsAuthEnabled() => Environment.GetEnvironmentVariable("Authorization")?.Equals("true") ?? false;
    }
}