using Microsoft.EntityFrameworkCore;
using Notes.Models;
using Notes.Repository.InterfacesOfRepositories;
using Notes.Repository.InterfacesOfStorage;
using Notes.TestDbSQLite;

namespace Notes.Repository.ClassesOfRepositories
{
    public class TagRepositorySQLite : ITagRepository
    {
        /// <summary>
        /// Контекст SQLite.
        /// </summary>
        private SQLiteDbContext Context;
        /// <summary>
        /// Гэги в контексте данных.
        /// </summary>
        private DbSet<Tag> Tags;
        /// <summary>
        /// Задать контекст базы данных для репозитория.
        /// </summary>
        /// <param name="storageContext">Контекст базы данных.</param>
        public void SetStorageContext(IStorageContext storageContext)
        {
            Context = (SQLiteDbContext)storageContext;
            Tags = Context.Tags;
        }
        /// <summary>
        /// Добавить новый тэг.
        /// </summary>
        /// <param name="tag"> Тэг для создания. </param>
        /// <returns> Созданный тэг. </returns>
        public Tag Create(Tag tag)
        {
            Tags.Add(tag);
            return tag;
        }
        /// <summary>
        /// Получить слайс тэгов.
        /// </summary>
        /// <param name="page"> Порядковый номер слайса тэгов. </param>
        /// <param name="size"> Размер одного слайса тэгов. </param>
        /// <returns> Слайс тэгов. </returns>
        public IEnumerable<Tag> Read(int page, int size)
        {
            return Tags.Skip(page * size).Take(size).ToList();
        }

        /// <summary>
        /// Обновить тэг.
        /// </summary>
        /// <param name="tag"> Измененный тэг. </param>
        /// <returns> Измененный тэг. </returns>
        public Tag Update(Tag tag)
        {
            if (Tags.Find(tag.Id) is null)
            {
                throw new ArgumentException("Попытка изменить тэг, которого нет в базе данных");
            }
            Tags.Entry(tag).State = EntityState.Modified;
            return tag;
        }

        /// <summary>
        /// Удалить тэг.
        /// </summary>
        /// <param name="id"> Тэг с таким айди не найден. </param>
        /// <exception cref="ArgumentException"> Тэг с таким айди не найден. </exception>
        public void Delete(int id)
        {
            var note = Tags.Find(id);
            if (note != null)
                Tags.Remove(note);
            else
                throw new ArgumentException("Тэга для удаления с таким айди не найдено.");
        }

        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (disposed)
                if (disposing)
                    Context.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
