namespace Obsidian.Models
{
    public class OAuthSignInModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RememberMe { get; set; }
        public string ProtectedOAuthContext { get; set; }
    }
}