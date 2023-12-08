using Notes.Repository.ClassesOfRepositories;
using Notes.Repository.InterfacesOfRepositories;
using Notes.Repository.InterfacesOfStorage;
using Notes.TestDbSQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNoteProject
{
    [TestClass]
    public class TestSQLiteStorage
    {
        [TestMethod]
        public void TestGetRepository_Valid()
        {
            IStorage storage = new SQLiteStorage();
            var noteRepos = storage.GetRepository<INoteRepository>();
            var tagRepos = storage.GetRepository<ITagRepository>();
            var pushRepos = storage.GetRepository<IPushRepository>();

            Assert.IsTrue(noteRepos is NoteRepositorySQLite);
            Assert.IsTrue(tagRepos is TagRepositorySQLite);
            Assert.IsTrue(pushRepos is PushRepositorySQLite);
        }

        private interface IRepos_Invalid : IRepository { }

        [TestMethod]
        public void TestGetRepository_Invalid()
        {
            IStorage storage = new SQLiteStorage();
            var invalidRepos = storage.GetRepository<IRepos_Invalid>();
            Assert.IsTrue(invalidRepos is null);
        }
    }
}
