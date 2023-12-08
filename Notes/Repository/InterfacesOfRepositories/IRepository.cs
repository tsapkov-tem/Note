using Notes.Repository.InterfacesOfStorage;

namespace Notes.Repository.InterfacesOfRepositories
{
    /// <summary>
    /// Базовый интерфейс всех репозиториев.
    /// </summary>
    public interface IRepository : IDisposable 
    {
        /// <summary>
        /// Задать конкретный контекст базы данных.
        /// </summary>
        /// <param name="storageContext"> Реализация контекста базы данных. </param>
        void SetStorageContext(IStorageContext storageContext);
    }
}
