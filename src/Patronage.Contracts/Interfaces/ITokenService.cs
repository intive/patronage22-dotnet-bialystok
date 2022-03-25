using Patronage.Contracts.Helpers;
using System.Security.Claims;

namespace Patronage.Contracts.Interfaces
{
    public interface ITokenService
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);

        RefreshToken GenerateRefreshToken();

        ClaimsPrincipal GetPrincipalFromExpiredToken(string accessToken);
    }
}