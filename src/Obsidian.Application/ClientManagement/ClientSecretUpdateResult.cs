using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Application.ClientManagement
{
    public class ClientSecretUpdateResult
    {
        public bool Succeed { get; set; }
        public string Message { get; set; }
        public string Secret { get; set; }
    }
}
