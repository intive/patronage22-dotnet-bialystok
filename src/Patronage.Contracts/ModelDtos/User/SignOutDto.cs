using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patronage.Contracts.ModelDtos.User
{
    public class SignOutDto
    {
        public string AccessToken { get; set; } = null!;
    }
}