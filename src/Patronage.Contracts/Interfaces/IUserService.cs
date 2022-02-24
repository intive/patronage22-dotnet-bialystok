using Microsoft.AspNetCore.Identity;
using Patronage.Contracts.ModelDtos.User;
using Patronage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patronage.Contracts.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> CreateUserAsync(CreateUserDto createUser);
        Task<bool> ConfirmEmail(string id, string token);
        Task<bool> GenerateRecoveryPassword(string id);
        Task<bool> RecoverPassword(string id, string token);
    }
}
