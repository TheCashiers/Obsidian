using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Obsidian.Misc;

namespace Obsidian.Models.OAuth
{
    [ModelBinder(BinderType = typeof(JsonDeserializeModelBinder<ResourceOwnerPasswordCredentialsGrantRequestModel>))]
    public class ResourceOwnerPasswordCredentialsGrantRequestModel
    {

        [Required, JsonProperty("client_id")]
        public Guid ClientId { get; set; }

        [Required, JsonProperty("client_secret")]
        public string ClientSecret { get; set; }

        [Required, JsonProperty("scope")]
        public string Scope { get; set; }

        [Required, JsonProperty("username")]
        public string UserName { get; set; }

        [Required, JsonProperty("password")]
        public string Password { get; set; }

        [Required, JsonProperty("grant_type")]
        public string GrantType { get; set; }
    }
}