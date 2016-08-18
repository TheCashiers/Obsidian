using System;

namespace Obsidian.Application.OAuth20
{
    public class AuthorizeResult
    {
        public string ErrorMessage { get; set; }
        public string RedirectUri { get; set; }
        public Guid SagaId { get; set; }
        public OAuth20Status Status { get; set; }
    }
}