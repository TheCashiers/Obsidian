using System;

namespace Obsidian.Persistence.Repositories
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class RepositoryAttribute : Attribute
    {
        public RepositoryAttribute(Type serviceType)
        {
            ServiceType = serviceType;
        }

        public Type ServiceType { get; private set; }
    }
}