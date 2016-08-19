using System.Runtime.Serialization;

namespace Obsidian.Models
{
    public class AccessTokenResponseModel
    {
        [DataMember(Name = "token_type")]
        public string TokenType { get; set; }

        [DataMember(Name = "expire_in")]
        public long ExpireInSecond { get; set; }

        [DataMember(Name = "scope")]
        public string Scope { get; set; }

        [DataMember(Name ="access_token")]
        public string AccessToken { get; set; }

        [DataMember(Name ="refresh_token")]
        public string RefreshToken { get; set; }

        [DataMember(Name ="authrentication_token")]
        public string AuthrneticationToken { get; set; }
    }
}