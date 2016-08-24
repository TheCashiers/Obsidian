using Microsoft.AspNetCore.Mvc;
using System;

namespace Obsidian.Models
{
    //TODO: map json proerty names
    public class AuthorizationCodeGrantRequestModel
    {
        public Guid Code{ get; set; }
        public Guid ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string RedirectUri { get; set; }
        public string GrantType { get; set; }
    }
}