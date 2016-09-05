using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Application.OAuth20
{
    public class OAuth20Configuration
    {
        public string GetTokenSigningKey() => "Obsidian.OAuth20.SigningKey.Jwt";
        public string GetTokenAudience() => "ObsidianAud";
        public string GetTokenIssuer() => "Obsidian";
    }
}
