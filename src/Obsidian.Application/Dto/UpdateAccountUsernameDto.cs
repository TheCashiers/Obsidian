using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Obsidian.Application.Dto
{
    public class UpdateAccountUsernameDto
    {
        [Required]
        public string NewUsername { get; set; }
    }
}
