using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Obsidian.Models
{
    public class AccessTokenResponseModel
    {
        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expire_in")]
        public long ExpireInSecond { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty("authrentication_token")]
        public string AuthrneticationToken { get; set; }
    }
}