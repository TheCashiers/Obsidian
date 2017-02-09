using Obsidian.Domain.Shared;

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
