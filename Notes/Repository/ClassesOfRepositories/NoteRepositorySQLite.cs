using Microsoft.EntityFrameworkCore;
using Notes.Models;
using Notes.Repository.InterfacesOfRepositories;
using Notes.Repository.InterfacesOfStorage;
using Notes.TestDbSQLite;

namespace Notes.Repository.ClassesOfRepositories
{
    /// <summary>
    /// Репозиторий заметок для базы данных SQLite.
    /// </summary>
    public class NoteRepositorySQLite : INoteRepository
    {
        /// <summary>
        /// Контекст SQLite.
        /// </summary>
        private SQLiteDbContext Context;
        /// <summary>
        /// Заметки в контексте данных.
        /// </summary>
        private DbSet<Note> Notes;
        /// <summary>
        /// Задать контекст базы данных для репозитория.
        /// </summary>
        /// <param name="storageContext">Контекст базы данных.</param>
        public void SetStorageContext(IStorageContext storageContext)
        {
            Context = (SQLiteDbContext)storageContext;
            Notes = Context.Notes;
        }
        /// <summary>
        /// Добавить новую заметку.
        /// </summary>
        /// <param name="note"> Заметка для создания. </param>
        /// <returns> Созданная заметка. </returns>
        public Note Create(Note note)
        {
            Notes.Add(note);
            return note;
        }
        /// <summary>
        /// Получить слайс заметок.
        /// </summary>
        /// <param name="page"> Порядковый номер слайса заметок. </param>
        /// <param name="size"> Размер одного слайса заметок. </param>
        /// <returns> Слайс заметок. </returns>
        public IEnumerable<Note> Read(int page, int size)
        {
            return Notes.Skip(page * size).Take(size);
        }

        /// <summary>
        /// Обновить заметку.
        /// </summary>
        /// <param name="note"> Измененная заметка. </param>
        /// <returns> Измененная заметка. </returns>
        public Note Update(Note note)
        {
            Notes.Entry(note).State = EntityState.Modified;
            return note;
        }

        /// <summary>
        /// Удалить заметку.
        /// </summary>
        /// <param name="id">Айди заметки для удаления.</param>
        /// <exception cref="ArgumentException"> Заметка с таким айди не найдена. </exception>
        public void Delete(int id)
        {
            var note = Notes.Find(id);
            if (note != null)
                Notes.Remove(note);
            else
                throw new ArgumentException("Заметки для удаления с таким айди не найдено.");
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
