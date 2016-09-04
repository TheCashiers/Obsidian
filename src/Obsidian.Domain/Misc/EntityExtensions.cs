using Obsidian.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Domain.Misc
{
    public static class EntityExtensions
    {
        public static bool EntityEquals<TEntity>(this TEntity entity, object obj) where TEntity : class, IEntity
        {
            if (obj is TEntity)
            {
                return entity.Id == (obj as TEntity).Id;
            }
            return false;
        }
    }
}
