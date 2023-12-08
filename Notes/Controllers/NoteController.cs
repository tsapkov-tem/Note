using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using NLog;
using Notes.Models;
using Notes.Repository.InterfacesOfRepositories;
using Notes.Repository.InterfacesOfStorage;
using SQLitePCL;
using System.Text.Json;

namespace Notes.Controllers
{
    /// <summary>
    /// Контроллер для взаимодействия с заметками.
    /// </summary>
    [Route("api/[controller]")]
    public class NoteController : Controller
    {
        readonly IStorage Storage;
        readonly INoteRepository NoteRepos;
        readonly Logger logger;

        public NoteController(IStorage storage)
        {
            logger = LogManager.GetLogger("main");
            Storage = storage;
            NoteRepos = storage.GetRepository<INoteRepository>();
        }
        
        /// <summary>
        /// Получить слайс заметок.
        /// </summary>
        /// <param name="page"> Порядковый номер слайса. </param>
        /// <param name="size"> Размер слайса. </param>
        /// <returns> Слайс заметок.(Json) </returns>
        [Route("notes")]
        [HttpGet]
        public JsonResult GetNotes(int page, int size)
        {
            var list = NoteRepos.Read(page, size);
            return Json(data: list);
        }

        /// <summary>
        /// Изменить заметку.
        /// </summary>
        /// <param name="note"> Заметка для изменения. </param>
        /// <returns> Измененная заметка.(Json) </returns>
        [Route("update")]
        [HttpPost]
        public JsonResult UpdateNotes([FromBody] Note note)
        {
            try
            {
                NoteRepos.Update(note);
            }
            catch(ArgumentException ex)
            {
                logger.Error(ex.Message);
                return Json(ex.Message);
            }
            return Json(note);
        }

        /// <summary>
        /// Добавить тэг к заметке.
        /// </summary>
        /// <param name="note"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        [Route("update/addTag")]
        [HttpPost]
        public JsonResult UpdateNote([FromBody] Note note, [FromBody] Tag tag)
        {
            note.Tags.Add(tag);
            try
            {
                NoteRepos.Update(note);
            }
            catch (ArgumentException ex)
            {
                logger.Error(ex.Message);
                return Json(ex.Message);
            }
            return Json(note);
        }

        /// <summary>
        /// Создать новую заметку.
        /// </summary>
        /// <param name="note"> Заметка для создания. </param>
        /// <returns> Созданная заметка.(Json) </returns>
        [Route("Create")]
        [HttpPost]
        public JsonResult CreateNotes([FromBody] Note note)
        {
            return Json(NoteRepos.Create(note));
        }

        /// <summary>
        /// Удалить заметку.
        /// </summary>
        /// <param name="id"> Айди заметки. </param>
        /// <returns> True, если удалено. </returns>
        [Route("Delete")]
        [HttpPost]
        public JsonResult DeleteNotes(int id)
        {
            try
            {
                NoteRepos.Delete(id);
            }catch(ArgumentException ex)
            {
                logger.Error(ex.Message);
                return Json(ex.Message);
            }
            return Json(true);
        }

        /// <summary>
        /// Сохранить все изменения.
        /// </summary>
        [Route("Save")]
        [HttpPost]
        public void SaveNotes()
        {
            Storage.Save();
        }
    }

}
