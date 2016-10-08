using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Application.OAuth20
{
    public class OAuth20Configuration
    {
        public string TokenSigningKey { get; set; }
        public string TokenAudience { get; set; }
        public string TokenIssuer { get; set; }
    }
}
