using Obsidian.Foundation.ProcessManagement;
using System;

namespace Obsidian.Application.OAuth20.TokenVerification
{
    public class VerifyTokenCommand : Command<bool>
    {
        public Guid ClientId { get; set; }
        public string Token { get; set; }
    }
}