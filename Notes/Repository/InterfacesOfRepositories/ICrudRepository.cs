using Notes.Repository.InterfacesOfStorage;

namespace Notes.Repository.InterfacesOfRepositories
{
    /// <summary>
    /// Интерфейс репозитория CRUD операций.
    /// </summary>
    public interface ICrudRepository<T>
    {
        /// <summary>
        /// Создать новый объект.
        /// </summary>
        /// <param name="item"> Объект для сохранения. </param>
        /// <returns> Сохраненный объект. </returns>
        public T Create(T item);
        /// <summary>
        /// Получить слайс объектов.
        /// </summary>
        /// <param name="page"> Порядковый номер слайса объектов. </param>
        /// <param name="size"> Размер одного слайса объектов. </param>
        /// <returns> Слайс объектов. </returns>
        public IEnumerable<T> Read(int page, int size);
        /// <summary>
        /// Изменить объект.
        /// </summary>
        /// <param name="item"> Измененный объект. </param>
        /// <returns> Измененный объект. </returns>
        public T Update(T item);
        /// <summary>
        /// Удалить объект.
        /// </summary>
        /// <param name="item"> Объект для удаления. </param>
        public void Delete(int id);
    }
}
