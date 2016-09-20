using Obsidian.Domain;
using System;

namespace Obsidian.Application.Dto
{
    public class UpdateProfileDto
    {
        public Guid UserId { get; set; }
        public UserProfile NewProfile { get; set; }
    }
}