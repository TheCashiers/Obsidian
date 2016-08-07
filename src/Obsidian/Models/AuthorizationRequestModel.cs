using Microsoft.AspNetCore.Mvc;

namespace Obsidian.Models
{
    public class AuthorizationRequestModel
    {
        [FromQuery(Name = "response_type")]
        public string ResponseType { get; set; }

        [FromQuery(Name ="client_id")]
        public string ClientId { get; set; }

        [FromQuery(Name ="redirect_uri")]
        public string RedirectUri { get; set; }

        [FromQuery(Name ="scope")]
        public string Scope { get; set; }
    }
}