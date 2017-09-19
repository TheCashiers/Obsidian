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
            DependencyContext.Default.RuntimeLibraries
                .Where(lib => lib.Type == "project")
                .Select(lib => Assembly.Load(new AssemblyName(lib.Name)))
                .SelectMany(assembly => assembly.GetTypes())
                .SelectMany(t => t.GetCustomAttributes<ServiceAttribute>(),
                            (implType, attribute) => new ServiceDescriptor(attribute.ServiceType ?? implType, 
                                                                              implType, attribute.Lifetime))
                .ForEach(services.Add);
            return services;
        }
    }
}
