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
                            (ti, attr) => new { TypeInfo = ti, Service = attr })
                .Select(info =>
                {
                    var implType = info.TypeInfo.AsType();
                    var serviceType = info.Service.ServiceType ?? implType;
                    return new ServiceDescriptor(serviceType, implType, info.Service.Lifetime);
                });
            serviceDescriptors.ForEach(services.Add);
            return services;
        }
    }
}
