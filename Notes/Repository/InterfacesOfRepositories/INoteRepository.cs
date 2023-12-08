using Notes.Models;

namespace Notes.Repository.InterfacesOfRepositories
{
    public interface INoteRepository : IRepository, ICrudRepository<Note> { }
}
