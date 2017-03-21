using System.ComponentModel.DataAnnotations;

namespace Obsidian.Application.Dto
{
    public class UpdateAccountPasswordDto
    {
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
    }
}
