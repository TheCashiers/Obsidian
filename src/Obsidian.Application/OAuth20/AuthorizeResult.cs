using System;
using Obsidian.Domain;

namespace Obsidian.Application.OAuth20
{
    public class AuthorizeResult
    {
        public Client Client { get; set; }
        public string ErrorMessage { get; set; }
        public string RedirectUri { get; set; }
        public Guid SagaId { get; set; }
        public PermissionScope[] Scopes { get;  set; }
        public OAuth20State Status { get; set; }
    }
}