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
    public class TestTagRepository
    {
        [TestMethod]
        public void TestAdd()
        {
            IStorage storage = new SQLiteStorage();
            var tagRepos = storage.GetRepository<ITagRepository>();
            string name = Guid.NewGuid().ToString();
            var tag = new Tag(name);

            Assert.AreEqual(tagRepos.Create(tag), tag);
        }

        [TestMethod]
        public void TestRead()
        {
            IStorage storage = new SQLiteStorage();
            var tagRepos = storage.GetRepository<ITagRepository>();

            string name1 = "name1";
            var tag1 = new Tag(name1);
            string name2 = "name2";
            var tag2 = new Tag(name2);
            string name3 = "name1";
            var tag3 = new Tag(name3);
            string name4 = "name4";
            var tag4 = new Tag(name4);
            string name5 = "name5";
            var tag5 = new Tag(name5);

            tagRepos.Create(tag1);
            tagRepos.Create(tag2);
            tagRepos.Create(tag3);
            tagRepos.Create(tag4);
            tagRepos.Create(tag5);

            storage.Save();

            tagRepos = storage.GetRepository<ITagRepository>();

            IList<Tag> slice1 = tagRepos.Read(0, 2).ToList();
            IList<Tag> slice2 = tagRepos.Read(1, 2).ToList();
            IList<Tag> slice3 = tagRepos.Read(2, 2).ToList();

            Assert.IsTrue(slice1.Contains(tag1));
            Assert.IsTrue(slice1.Contains(tag2));
            Assert.AreEqual(slice1.Count(), 2);
            Assert.IsTrue(slice2.Contains(tag3));
            Assert.IsTrue(slice2.Contains(tag4));
            Assert.AreEqual(slice2.Count(), 2);
            Assert.IsTrue(slice3.Contains(tag5));
            Assert.AreEqual(slice3.Count(), 1);
        }

        [TestMethod]
        public void TestUpdate_Valid()
        {
            IStorage storage = new SQLiteStorage();
            var tagRepos = storage.GetRepository<ITagRepository>();

            Note note = new Note("title1");
            Note note2 = new Note("title2");

            string name1 = "name1";
            var tag1 = new Tag(name1);
            tag1.Notes.Add(note);
            tag1.Notes.Add(note2);

            tagRepos.Create(tag1);
            storage.Save();
            tagRepos = storage.GetRepository<ITagRepository>();

            tag1.Name = "New name";
            tagRepos.Update(tag1);

            storage.Save();
            tagRepos = storage.GetRepository<ITagRepository>();
            IList<Tag> slice = tagRepos.Read(0, 1).ToList();

            Assert.AreEqual(slice[0].Name, tag1.Name);
            Assert.AreEqual(slice[0].Notes[0], note);
            Assert.AreEqual(slice[0].Notes[1], note2);
        }

        [TestMethod]
        public void TestDelete_Valid()
        {
            IStorage storage = new SQLiteStorage();
            var tagRepos = storage.GetRepository<ITagRepository>();

            string name1 = "name1";
            var tag1 = new Tag(name1);

            tagRepos.Create(tag1);
            storage.Save();
            tagRepos = storage.GetRepository<ITagRepository>();

            tagRepos.Delete(tag1.Id);

            storage.Save();
            tagRepos = storage.GetRepository<ITagRepository>();
            IList<Tag> slice = tagRepos.Read(0, 1).ToList();

            Assert.AreEqual(slice.Count(), 0);
        }

        [TestMethod]
        public void TestDelete_Invalid()
        {
            IStorage storage = new SQLiteStorage();
            var tagRepos = storage.GetRepository<ITagRepository>();

            string name1 = "name1";
            var tag1 = new Tag(name1);

            tagRepos.Create(tag1);
            storage.Save();
            tagRepos = storage.GetRepository<ITagRepository>();

            string name2 = "name2";
            var tag2 = new Tag(name2);

            Assert.ThrowsException<ArgumentException>(() => tagRepos.Delete(tag2.Id));
        }
    }
}
