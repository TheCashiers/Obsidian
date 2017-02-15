using System.ComponentModel.DataAnnotations;

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
