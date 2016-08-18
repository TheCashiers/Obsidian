using OAuth20;

namespace Obsidian.Application.OAuth20
{
    public class AuthorizeResult
    {
        public string ErrorMessage { get; set; }
        public OAuth20Status Status { get; internal set; }
    }
}