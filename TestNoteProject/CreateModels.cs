using Notes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNoteProject
{
    [TestClass]
    public class CreateModels
    {
        [TestMethod]
        public void NoteCreate_Valid()
        {
            string title1 = Guid.NewGuid().ToString();
            var note1 = new Note(title1);
            
            string title2 = Guid.NewGuid().ToString();
            string text2 = Guid.NewGuid().ToString();
            var note2 = new Note(title2, text2);

            string title3 = Guid.NewGuid().ToString();
            string text3 = Guid.NewGuid().ToString();
            Random random = new Random();
            int days = random.Next(30);
            DateTime dateTime3 = DateTime.Now.AddDays(days);
            var note3 = new Note(title3, text3, dateTime3);

            Assert.AreEqual(title1, note1.Title);
            Assert.AreEqual("", note1.Text);
            Assert.AreEqual(title2, note2.Title);
            Assert.AreEqual(text2, note2.Text);
            Assert.AreEqual(title3, note3.Title);
            Assert.AreEqual(text3, note3.Text);
            Assert.AreEqual(dateTime3, note3.DatePin);
        }

        [TestMethod]
        public void NoteCreate_Invalid()
        {
            StringBuilder builder = new StringBuilder();

            builder.Insert(0, Guid.NewGuid().ToString(), 10);
            string title1 = builder.ToString();
            builder.Clear();

            string title2 = builder.ToString();
            builder.Insert(0, Guid.NewGuid().ToString(), 10);
            string text2 = builder.ToString();
            builder.Clear();

            string title3 = Guid.NewGuid().ToString();
            string text3 = Guid.NewGuid().ToString();
            Random random = new Random();
            int days = random.Next(30);
            DateTime dateTime3 = DateTime.Now.AddDays(-days);

            Assert.ThrowsException<ArgumentException>(() => new Note(title1));
            Assert.ThrowsException<ArgumentException>(() => new Note(title2, text2));
            Assert.ThrowsException<ArgumentException>(() => new Note(title3, text3, dateTime3));

            string strNull = null;

            Assert.ThrowsException<ArgumentException>(() => new Note(""));
            Assert.ThrowsException<ArgumentException>(() => new Note(strNull));
            Assert.ThrowsException<ArgumentException>(() => new Note(title2, strNull));
        }

        [TestMethod]
        public void TagCreate_Valid()
        {
            string name = Guid.NewGuid().ToString();
            var tag = new Tag(name);

            Assert.AreEqual(name, tag.Name);
        }

        [TestMethod]
        public void TagCreate_Invalid()
        {
            StringBuilder builder = new StringBuilder();
            builder.Insert(0, Guid.NewGuid().ToString(), 10);
            string name = builder.ToString();

            Assert.ThrowsException<ArgumentException>(() => new Tag(name));
        }

        [TestMethod]
        public void PushCreate_Valid()
        {
            string title = Guid.NewGuid().ToString();
            string text = Guid.NewGuid().ToString();
            Random random = new Random();
            int days = random.Next(30);
            DateTime dateTime = DateTime.Now.AddDays(days);
            var note = new Note(title, text, dateTime);

            var push = new Push(note);

            Assert.AreEqual(push.Date, dateTime);
        }

        [TestMethod]
        public void PushCreate_Invalid()
        {
            string title = Guid.NewGuid().ToString();
            string text = Guid.NewGuid().ToString();
            
            var note1 = new Note(title, text);
            var date = DateTime.Now;
            var note2 = new Note(title, text, date);
            Thread.Sleep(1000);

            Assert.ThrowsException<ArgumentException>(() => new Push(note1));
            Assert.ThrowsException<ArgumentException>(() => new Push(note2));
        }
    }
}
