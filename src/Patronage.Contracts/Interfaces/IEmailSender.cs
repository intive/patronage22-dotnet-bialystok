﻿namespace Patronage.Contracts.Interfaces
{
    public interface IEmailSender
    {
        void SendEmail(string email, string subject, string message);
    }
}
