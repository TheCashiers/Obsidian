using System;
using System.ComponentModel.DataAnnotations;

namespace Obsidian.Models.OAuth
{
    public class TokenVerificationModel
    {
        [Required]
        public Guid ClientId { get; set; }

        [Required]
        public string Token { get; set; }
    }
}