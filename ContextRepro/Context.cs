using ContextRepro.Entity.Belege;
using ContextRepro.Infrastructure.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace ContextRepro
{
    public class Context : DbContext
    {
        private string ConnectionString { get; }

        public static readonly ILoggerFactory MyLoggerFactory
            = LoggerFactory.Create(builder => { builder.AddConsole(); });

        #region Entities
        public virtual DbSet<Beleg> Belege { get; set; }
        public virtual DbSet<Vorgang> Vorgaenge { get; set; }
        public virtual DbSet<BelegAdresse> BelegAdressen { get; set; }
        #endregion

        internal Context(DbContextOptions<Context> options, long? mandantId = null, bool forTesting = false) : base(options)
        {
        }

        public Context(string connectionString)
        {
            ConnectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!string.IsNullOrEmpty(ConnectionString))
            {
                optionsBuilder
                    .UseLazyLoadingProxies()
                    .UseLoggerFactory(MyLoggerFactory)
                    .UseSqlServer(ConnectionString, builder =>
                    {
                        builder.EnableRetryOnFailure();
                    });
#if DEBUG
                optionsBuilder.LogTo(x => Debug.WriteLine(x));
                optionsBuilder.EnableSensitiveDataLogging();
#endif
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new ContextModelBuilder(modelBuilder).ApplyConfigurations();
        }

        public override int SaveChanges()
        {
            try
            {
                var entities = ChangeTracker.Entries();

                // entities: internal usage

                int success = base.SaveChanges();

                return success;
            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}