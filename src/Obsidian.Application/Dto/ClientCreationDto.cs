using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Application.Dto
{
    public class ClientCreationDto
    {
        [Required]
        public string DisplayName { get; set; }

        [Required, Url]
        public string RedirectUri { get; set; }
    }
}
