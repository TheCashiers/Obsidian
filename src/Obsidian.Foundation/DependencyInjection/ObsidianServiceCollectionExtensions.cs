using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using Obsidian.Foundation.Collections;
using System.Linq;
using System.Reflection;

namespace Obsidian.Foundation.DependencyInjection
{
    public static class ObsidianServiceCollectionExtensions
    {
        public static IServiceCollection AddObsidianServices(this IServiceCollection services)
        {
            var projectLibs = DependencyContext.Default.RuntimeLibraries.Where(lib => lib.Type == "project");
            var typeInfos = projectLibs.Select(lib => new AssemblyName(lib.Name)).Select(Assembly.Load).SelectMany(asm => asm.DefinedTypes);
            var serviceDescriptors = typeInfos
                .SelectMany(t => t.GetCustomAttributes<ServiceAttribute>(),
                            (ti, attr) => new { TypeInfo = ti, Attribute = attr })
                .Select(pair =>
                {
                    var implType = pair.TypeInfo.AsType();
                    var serviceType = pair.Attribute.ServiceType ?? implType;
                    return new ServiceDescriptor(serviceType, implType, pair.Attribute.Lifetime);
                });
            serviceDescriptors.ForEach(d => services.Add(d));
            return services;
        }
    }
}
