﻿namespace Patronage.Contracts.ModelDtos.User
{
    public class SignInDto
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}