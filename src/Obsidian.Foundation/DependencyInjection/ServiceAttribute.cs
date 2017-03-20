using Microsoft.Extensions.DependencyInjection;
using System;

namespace Obsidian.Foundation.DependencyInjection
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ServiceAttribute : Attribute
    {
        public ServiceAttribute(ServiceLifetime lifetime, Type serviceType = null)
        {
            Lifetime = lifetime;
            ServiceType = serviceType;
        }

        public ServiceLifetime Lifetime { get; private set; }
        public Type ServiceType { get; private set; }
    }
}