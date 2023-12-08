using Notes.Models;

namespace Notes.Repository.InterfacesOfRepositories
{
    public interface ITagRepository : IRepository, ICrudRepository<Tag> { }
}
