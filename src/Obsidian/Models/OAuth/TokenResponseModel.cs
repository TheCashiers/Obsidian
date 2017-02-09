using Newtonsoft.Json;
using Obsidian.Application.OAuth20;
using System.Linq;

namespace Obsidian.Models.OAuth
{
    public class TokenResponseModel
    {
        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expire_in")]
        public long ExpireInSecond { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("refresh_token", NullValueHandling = NullValueHandling.Ignore)]
        public string RefreshToken { get; set; }

        [JsonProperty("authrentication_token")]
        public string AuthrneticationToken { get; set; }

        public static TokenResponseModel FromOAuth20Result(OAuth20Result result)
            => new TokenResponseModel
            {
                TokenType = "bearer",
                AccessToken = result.Token.AccessToken,
                AuthrneticationToken = result.Token.AuthrneticationToken,
                ExpireInSecond = (long)result.Token.ExpireIn.TotalSeconds,
                Scope = string.Join(" ", result.Token.Scope.Select(s => s.ScopeName)),
                RefreshToken = result.Token.RefreshToken
            };
    }
}