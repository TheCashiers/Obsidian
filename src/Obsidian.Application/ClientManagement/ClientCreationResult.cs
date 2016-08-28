using System;

namespace Obsidian.Application.ClientManagement
{
    public class ClientCreationResult
    {
        public bool Succeed { get; set; }
        public string Message { get; set; }
        public Guid Id { get; set; }
    }
}