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
        /// <exception cref="NullReferenceException">Не указана строка подключения.</exception>
        public SQLiteStorage()
        {
            var config = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json")
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .Build();
            string connect = config.GetConnectionString("TestConnection");
            if (string.IsNullOrWhiteSpace(connect))
            {
                throw new NullReferenceException("Строка подключения не указанна в файле конфигурации.");
            }
            StorageContext = new SQLiteDbContext(connect);
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
        public async Task Save()
        {
            await StorageContext.SaveChangesAsync();
        }
    }
}
