using AutoMapper;
using System.Linq;
using System.Reflection;

namespace Obsidian.QueryModel.Mapping
{
    public static class MappingConfig
    {
        public static void ConfigureQueryModelMapping()
        {
            const string ns = "Obsidian.QueryModel.Mapping";
            var assembly = typeof(MappingConfig).GetTypeInfo().Assembly;
            var mappers = from t in assembly.GetTypes()
                          where t.Namespace == ns
                          from m in t.GetMethods()
                          where m.GetCustomAttribute<QueryModelMapperAttribute>() != null
                          where m.ReturnType == typeof(void)
                          where !m.ContainsGenericParameters
                          where m.IsStatic
                          select m;

            Mapper.Initialize(cfg =>
            {
                foreach (var m in mappers)
                {
                    m.Invoke(null, new[] { cfg });
                }
            });
        }
    }
}