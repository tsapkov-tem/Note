using Microsoft.EntityFrameworkCore;
using Notes.Models;
using Notes.Repository.InterfacesOfStorage;
using System.Reflection.Emit;
using System.Threading.Tasks;

namespace Notes.TestDbSQLite
{
    /// <summary>
    /// Контекст тестовой SQLite базы данных.
    /// </summary>
    public class SQLiteDbContext : DbContext, IStorageContext
    {
        /// <summary>
        /// Строка подключения к базе данных.
        /// </summary>
        private readonly string ConnectionString;

        public DbSet<Note> Notes => Set<Note>();
        public DbSet<Tag> Tags => Set<Tag>();
        public DbSet<Push> Pushes => Set<Push>();

        /// <summary>
        /// Создать новый контекст базы данных.
        /// </summary>
        /// <param name="connectionString"> Строка подключения к базе данных. </param>
        public SQLiteDbContext(string connectionString)
        {
            ConnectionString = connectionString;
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(ConnectionString);
        }
    }
}
