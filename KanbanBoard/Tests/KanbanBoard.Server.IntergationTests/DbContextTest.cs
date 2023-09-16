using Duende.IdentityServer.EntityFramework.Options;
using KanbanBoard.Server.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace KanbanBoard.Server.IntergationTests
{
    public class DbContextTest : IDisposable
    {
        private readonly SqliteConnection connection;
        private readonly DbContextOptions<ApplicationDbContext> _contextOptions;

        public DbContextTest()
        {
            // Create and open a connection. This creates the SQLite in-memory database, which will persist until the connection is closed
            // at the end of the test (see Dispose below).
            connection = new SqliteConnection("Filename=:memory:");
            connection.Open();

            // These options will be used by the context instances in this test suite, including the connection opened above.
            _contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(connection)
                .Options;

            // Create the schema and seed some data
            using var context = new ApplicationDbContext(_contextOptions, Options.Create(new OperationalStoreOptions { }));

            context.Database.EnsureCreated();
        }

        protected ApplicationDbContext CreateContext() => new ApplicationDbContext(_contextOptions, null);

        public void Dispose() => connection.Dispose();
    }
}
