using Notes.Repository.InterfacesOfRepositories;
using Notes.Repository.InterfacesOfStorage;
using System.Reflection;

namespace Notes.TestDbSQLite
{
    /// <summary>
    /// Класс взаимодействия с контекстом ДБ SQLite
    /// </summary>
    public class SQLiteStorage : IStorage
    {
        /// <summary>
        /// Контекст базы данных.
        /// </summary>
        public SQLiteDbContext StorageContext { get; private set; }

        /// <summary>
        /// Создать новый класс взаимодействия с контекстом SQLite. 
        /// </summary>
        /// <param name="connection"> Строка подключения к БД. </param>
        public SQLiteStorage(string connection)
        {
            StorageContext = new SQLiteDbContext(connection);
        }

        /// <summary>
        /// Получить репозиторий.
        /// </summary>
        /// <typeparam name="T"> Тип требуемого репозитория </typeparam>
        /// <returns> Репозиторий, указанного типа. </returns>
        public T GetRepository<T>() where T : IRepository
        {
            foreach (Type type in GetType().GetTypeInfo().Assembly.GetTypes())
            {
                if (typeof(T).GetTypeInfo().IsAssignableFrom(type) && type.GetTypeInfo().IsClass)
                {
                    T repository = (T)Activator.CreateInstance(type);
                    repository.SetStorageContext(StorageContext);
                    return repository;
                }
            }

            return default;
        }

        /// <summary>
        /// Сохранение изменений в базе данных.
        /// </summary>
        public void Save()
        {
            StorageContext.SaveChanges();
        }
    }
}
