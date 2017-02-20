using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace Obsidian.Models.OAuth
{
    public class AuthorizationRequestModel
    {
        [Required, FromQuery(Name = "response_type")]
        public string ResponseType { get; set; }

        [Required, FromQuery(Name = "client_id")]
        public Guid ClientId { get; set; }

        [Required, FromQuery(Name = "redirect_uri")]
        public string RedirectUri { get; set; }

        [Required, FromQuery(Name = "scope")]
        public string Scope { get; set; }
    }
}