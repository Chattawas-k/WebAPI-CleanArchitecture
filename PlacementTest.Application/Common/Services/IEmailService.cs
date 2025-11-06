using System;
using System.Collections.Generic;
using System.Text;

namespace PlacementTest.Application.Common.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body);
    }
}
