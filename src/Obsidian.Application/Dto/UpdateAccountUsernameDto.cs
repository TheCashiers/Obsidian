using System.ComponentModel.DataAnnotations;

namespace Obsidian.Application.Dto
{
    public class UpdateAccountUsernameDto
    {
        [Required]
        public string NewUsername { get; set; }
    }
}
