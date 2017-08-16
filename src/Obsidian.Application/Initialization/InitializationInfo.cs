using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Application.Initialization
{
    public class InitializationInfo
    {
       
        public DefaultUserInfo User { get; set; }

        public class DefaultUserInfo
        {
            public string UserName { get; set; }

            public string Password { get; set; }

            public string ConfirmPassword { get; set; }
        }

    }
}
