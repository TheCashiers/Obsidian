using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Obsidian.Misc;
using System;
using System.ComponentModel.DataAnnotations;

namespace Obsidian.Models
{
    [ModelBinder(BinderType = typeof(JsonDeserializeModelBinder<AuthorizationCodeGrantRequestModel>))]
    public class AuthorizationCodeGrantRequestModel
    {
        [Required, JsonProperty("code")]
        public Guid Code { get; set; }

        [Required, JsonProperty("client_id")]
        public Guid ClientId { get; set; }

        [Required, JsonProperty("client_secret")]
        public string ClientSecret { get; set; }

        [Required, Url, JsonProperty("redirect_uri")]
        public string RedirectUri { get; set; }

        [Required, JsonProperty("grant_type")]
        public string GrantType { get; set; }
    }
}