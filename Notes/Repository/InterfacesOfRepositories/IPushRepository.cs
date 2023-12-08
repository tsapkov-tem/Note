using Notes.Models;

namespace Notes.Repository.InterfacesOfRepositories
{
    public interface IPushRepository : IRepository {
        public Push Create(Push push);
    }
}
