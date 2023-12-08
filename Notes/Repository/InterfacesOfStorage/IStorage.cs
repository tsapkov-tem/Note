using Notes.Repository.InterfacesOfRepositories;

namespace Notes.Repository.InterfacesOfStorage
{
    /// <summary>
    /// Интерфейс взаимодействий с context.
    /// </summary>
    public interface IStorage
    {
        /// <summary>
        /// Получить репозиторий.
        /// </summary>
        /// <typeparam name="T"> Type репозитория. </typeparam>
        /// <returns> Репозиторий типа T. </returns>
        public T GetRepository<T>() where T : IRepository;
        /// <summary>
        /// Сохранить изменения в базе дынных.
        /// </summary>
        public void Save();
    }
}
