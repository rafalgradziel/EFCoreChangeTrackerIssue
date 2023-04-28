using ContextRepro.Factory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ContextRepro
{
    public class Program
    {
        public static string ConnectionString = "Server=(server);Database=EFreproDB;Uid=user;Pwd=password;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=False;";

        public static void Main(string[] args) 
        {
            var services = new ServiceCollection();

            services.AddLogging(configure => configure.AddConsole());
            services.AddDbContext<Context>(options => options.UseSqlServer(ConnectionString));
            services.AddScoped<IContextFactory>(s => new ContextFactory(ConnectionString));
            services.AddScoped<ServiceTest>();

            var serviceProvider = services.BuildServiceProvider();
            var service = serviceProvider.GetRequiredService<ServiceTest>();

            // test #1
            service.ScenarioOne();

            // test #2
            service.ScenarioTwo();
        }
    }

    // Create DbContext from a design-time factory
    public class ReproContextFactory : IDesignTimeDbContextFactory<Context>
    {
        public Context CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<Context>();
            optionsBuilder.UseSqlServer(Program.ConnectionString);

            return new Context(optionsBuilder.Options);
        }
    }
}
