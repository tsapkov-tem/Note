using Microsoft.VisualStudio.TestTools.UnitTesting;
using Notes.Models;
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
    public class TestPushRepository
    {
        [TestMethod]
        public void TestAdd()
        {
            IStorage storage = new SQLiteStorage("Data Source= #TestDb.db");
            var pushRepos = storage.GetRepository<IPushRepository>();
            string title = Guid.NewGuid().ToString();
            string text = Guid.NewGuid().ToString();
            var date = DateTime.Now.AddDays(1);
            var note = new Note(title, text, date);
            var push = new Push(note);

            Assert.AreEqual(pushRepos.Create(push), push);
        }
    }
}
