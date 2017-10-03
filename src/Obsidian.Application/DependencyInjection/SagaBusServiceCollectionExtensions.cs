using Microsoft.Extensions.DependencyInjection;
using Obsidian.Foundation.Collections;
using Obsidian.Foundation.ProcessManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Obsidian.Application.DependencyInjection
{
    public static class SagaBusServiceCollectionExtensions
    {
        /// <summary>
        /// Register <see cref="SagaBus"/> as a service in <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add servicesto.</param>
        /// <returns>The same service collection so that multiple calls can be chained.</returns>
        public static IServiceCollection AddSagaBus(this IServiceCollection services) => services.AddSingleton(sp =>
        {
            var bus = new SagaBus(sp);
            return bus;
        });

        /// <summary>
        /// Register sagas as a service in <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add servicesto.</param>
        /// <returns>The same service collection so that multiple calls can be chained.</returns>
        public static IServiceCollection AddSagas(this IServiceCollection services)

        {
            _sagaTypes.SelectMany(sagaType => sagaType.GetInterfaces(),
                                 (t, i) => new ServiceDescriptor(i, t, ServiceLifetime.Transient))
                      .ForEach(services.Add);
            return services;
        }

        private static readonly IEnumerable<Type> _sagaTypes = FindSagas();

        private static IEnumerable<Type> FindSagas()
        {
            var assembly = typeof(SagaBusServiceCollectionExtensions).GetTypeInfo().Assembly;
            return assembly.GetTypes()
                .Where(t => (!t.IsAbstract) && t.HasBaseType(typeof(Saga)));
        }

        private static bool HasBaseType(this Type type, Type targetType) 
            => GetBaseTypes(type).Any(t => t.IsEquivalentTo(targetType));

        private static IEnumerable<Type> GetBaseTypes(this Type type)
        {
            while ((type = type.BaseType) != null)
                yield return type;
        }
    }
}