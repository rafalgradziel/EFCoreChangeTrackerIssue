using Microsoft.EntityFrameworkCore;

namespace ContextRepro
{
    public static class DbSetExtensions
    {
        public static T CreateInstance<T>(this DbSet<T> dbSet) where T : class
        {
            var newEntity = (T)Activator.CreateInstance(typeof(T));
            dbSet.Add(newEntity);
            return newEntity;
        }
    }
}
