namespace ContextRepro.Factory
{
    public class ContextFactory : IContextFactory
    {
        private string _connString;

        public ContextFactory(string connectionString)
        {
            _connString = connectionString;
        }

        public Context GetContext()
        {
            return new Context(_connString);
        }
    }
}
