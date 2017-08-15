using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Models
{
    public class InitializationInfo
    {

        class DbInfo
        {
            public string DbUri { get; set; }

            public string DbName { get; set; }
        }

        class DefaultUserInfo
        {
            public string UserName { get; set; }

            public string Password { get; set; }

            public string ConfirmPassword { get; set; }
        }



    }
}
