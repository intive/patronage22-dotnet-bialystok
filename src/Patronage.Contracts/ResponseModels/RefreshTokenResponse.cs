using Patronage.Contracts.Helpers;
using Patronage.Contracts.ModelDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patronage.Contracts.ResponseModels
{
    public class RefreshTokenResponse
    {
        public RefreshToken? RefreshToken { get; set; }
        public string? AccessToken { get; set; }
    }
}