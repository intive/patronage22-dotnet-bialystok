using Patronage.Contracts.Helpers;

namespace Patronage.Contracts.ResponseModels
{
    public class RefreshTokenResponse
    {
        public RefreshToken? RefreshToken { get; set; }
        public string? AccessToken { get; set; }
    }
}