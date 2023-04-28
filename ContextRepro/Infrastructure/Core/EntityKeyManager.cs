using Microsoft.EntityFrameworkCore.Metadata;

namespace ContextRepro.Infrastructure.Core
{
    /// <summary>
    /// Encapsulates low-level EF Core logic to deal with entity keys
    /// </summary>
    public class EntityKeyManager
    {
        public object[] GetEntityKeyValues(IEntityType entityType, object entity)
        {
            var primaryKey = entityType.FindPrimaryKey();
            var keyValues = new object[primaryKey.Properties.Count];
            for (int i = 0; i < keyValues.Length; i++)
                keyValues[i] = primaryKey.Properties[i].GetGetter().GetClrValue(entity);
            return keyValues;
        }
    }
}
