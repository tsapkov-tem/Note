using Microsoft.EntityFrameworkCore;
using Notes.Models;
using Notes.Repository.InterfacesOfRepositories;
using Notes.Repository.InterfacesOfStorage;
using Notes.TestDbSQLite;

namespace Notes.Repository.ClassesOfRepositories
{
    /// <summary>
    /// Репозиторий пуш оповещений.
    /// </summary>
    public class PushRepositorySQLite : IPushRepository
    {
        /// <summary>
        /// Контекст SQLite.
        /// </summary>
        private SQLiteDbContext Context;
        /// <summary>
        /// Пуши в контексте данных.
        /// </summary>
        private DbSet<Push> Pushes;
        /// <summary>
        /// Задать контекст базы данных для репозитория.
        /// </summary>
        /// <param name="storageContext">Контекст базы данных.</param>
        public void SetStorageContext(IStorageContext storageContext)
        {
            Context = (SQLiteDbContext)storageContext;
            Pushes = Context.Pushes;
        }
        /// <summary>
        /// Добавить новый пуш.
        /// </summary>
        /// <param name="push"> Пуш для создания. </param>
        /// <returns> Созданный пуш. </returns>
        public Push Create(Push push)
        {
            Pushes.Add(push);
            return push;
        }

        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (disposed)
                if (disposing)
                    Context.Dispose();
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
