using Notes.Models;
using Notes.Repository.ClassesOfRepositories;
using Notes.Repository.InterfacesOfRepositories;
using Notes.Repository.InterfacesOfStorage;
using Notes.TestDbSQLite;

namespace TestNoteProject
{
    [TestClass]
    public class TestNoteRepository
    {

        [TestMethod]
        public void TestAdd()
        {
            IStorage storage = new SQLiteStorage("Data Source= #TestDb.db");
            var noteRepos = storage.GetRepository<INoteRepository>();
            

            string title = Guid.NewGuid().ToString();
            string text = Guid.NewGuid().ToString();
            Random random = new Random();
            int days = random.Next(30);
            DateTime dateTime = DateTime.Now.AddDays(days);
            Note note = new Note(title, text, dateTime);

            Assert.AreEqual(noteRepos.Create(note), note);
        }

        [TestMethod]
        public void TestRead()
        {
            IStorage storage = new SQLiteStorage("Data Source= #TestDb.db");
            var noteRepos = storage.GetRepository<INoteRepository>();

            string title1 = "title1";
            string text1 = "text1";
            var note1 = new Note(title1, text1);

            string title2 = "title2";
            string text2 = "text2";
            var note2 = new Note(title2, text2);

            string title3 = "title3";
            string text3 = "text3";
            var note3 = new Note(title3, text3);

            string title4 = "title4";
            string text4 = "text4";
            var note4 = new Note(title4, text4);

            string title5 = "title5";
            string text5 = "text5";
            var note5 = new Note(title5, text5);

            noteRepos.Create(note1);
            noteRepos.Create(note2);
            noteRepos.Create(note3);
            noteRepos.Create(note4);
            noteRepos.Create(note5);

            storage.Save();

            noteRepos = storage.GetRepository<INoteRepository>();

            IList<Note> slice1 = noteRepos.Read(0, 2).ToList();
            IList<Note> slice2 = noteRepos.Read(1, 2).ToList();
            IList<Note> slice3 = noteRepos.Read(2, 2).ToList();

            Assert.IsTrue(slice1.Contains(note1));
            Assert.IsTrue(slice1.Contains(note2));
            Assert.AreEqual(slice1.Count(), 2);
            Assert.IsTrue(slice2.Contains(note3));
            Assert.IsTrue(slice2.Contains(note4));
            Assert.AreEqual(slice2.Count(), 2);
            Assert.IsTrue(slice3.Contains(note5));
            Assert.AreEqual(slice3.Count(), 1);
        }

        [TestMethod]
        public void TestUpdate_Valid()
        {
            IStorage storage = new SQLiteStorage("Data Source= #TestDb.db");
            var noteRepos = storage.GetRepository<INoteRepository>();

            string title = "title";
            string text = "text";
            var note = new Note(title, text);

            noteRepos.Create(note);
            storage.Save();
            noteRepos = storage.GetRepository<INoteRepository>();

            note.Text = "New text";
            noteRepos.Update(note);

            storage.Save();
            noteRepos = storage.GetRepository<INoteRepository>();
            IList<Note> slice = noteRepos.Read(0, 1).ToList();

            Assert.AreEqual(slice[0].Title, note.Title);
            Assert.AreEqual(slice[0].Text, "New text");
        }

        [TestMethod]
        public void TestUpdate_Invalid()
        {
            IStorage storage = new SQLiteStorage("Data Source= #TestDb.db");
            var noteRepos = storage.GetRepository<INoteRepository>();

            string title = "title";
            string text = "text";
            var note = new Note(title, text);

            noteRepos.Create(note);
            storage.Save();
            noteRepos = storage.GetRepository<INoteRepository>();

            string title1 = "title1";
            string text1 = "text1";
            var note1 = new Note(title1, text1);

            Assert.ThrowsException<ArgumentException>(() => noteRepos.Update(note1));
        }

        [TestMethod]
        public void TestDelete_Valid()
        {
            IStorage storage = new SQLiteStorage("Data Source= #TestDb.db");
            var noteRepos = storage.GetRepository<INoteRepository>();

            string title = "title";
            string text = "text";
            var note = new Note(title, text);

            noteRepos.Create(note);
            storage.Save();
            noteRepos = storage.GetRepository<INoteRepository>();

            noteRepos.Delete(note.Id);

            storage.Save();
            noteRepos = storage.GetRepository<INoteRepository>();
            IList<Note> slice = noteRepos.Read(0, 1).ToList();

            Assert.AreEqual(slice.Count(), 0);
        }

        [TestMethod]
        public void TestDelete_Invalid()
        {
            IStorage storage = new SQLiteStorage("Data Source= #TestDb.db");
            var noteRepos = storage.GetRepository<INoteRepository>();

            string title = "title";
            string text = "text";
            var note = new Note(title, text);

            noteRepos.Create(note);
            storage.Save();
            noteRepos = storage.GetRepository<INoteRepository>();

            string title1 = "title1";
            string text1 = "text1";
            var note1 = new Note(title1, text1);

            Assert.ThrowsException<ArgumentException>(() => noteRepos.Delete(note1.Id));
        }
    }
}