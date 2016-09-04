using System;

namespace Obsidian.Application.ScopeManagement
{
    public class ScopeCreationResult
    {
        public bool Succeed { get; set; }
        public string Message { get; set; }
        public Guid Id { get; set; }
    }
}