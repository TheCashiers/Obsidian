using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Obsidian.Models
{
    [ModelBinder(BinderType = typeof(JsonDeserializeModelBinder))]
    public class AuthorizationCodeGrantRequestModel
    {
        [JsonProperty("code")]
        public Guid Code { get; set; }

        [JsonProperty("client_id")]
        public Guid ClientId { get; set; }

        [JsonProperty("client_secret")]
        public string ClientSecret { get; set; }

        [JsonProperty("redirect_uri")]
        public string RedirectUri { get; set; }

        [JsonProperty("grant_type")]
        public string GrantType { get; set; }

    }
}