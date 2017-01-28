using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Application.Dto
{
    public class ClientCreationDto
    {
        public string DisplayName { get; set; }

        [Url]
        public string RedirectUri { get; set; }
    }
}
