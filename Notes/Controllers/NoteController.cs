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
        [Route("notes")]
        [HttpGet]
        public IEnumerable<Note> GetNotes(int page, int size)
        {
            var result = NoteRepos.Read(page, size).ToList();
            return result;
        }

        /// <summary>
        /// Изменить заметку.
        /// </summary>
        /// <param name="note"> Заметка для изменения. </param>
        [Route("update")]
        [HttpPost]
        public IActionResult UpdateNotes([FromBody] Note note)
        {
            if (ModelState.IsValid)
            {
                NoteRepos.Update(note);
                Storage.Save();
                return Ok(note);
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Создать новую заметку.
        /// </summary>
        /// <param name="note"> Заметка для создания. </param>
        [Route("create")]
        [HttpPost]
        public IActionResult CreateNotes([FromBody] Note note)
        {
            if (ModelState.IsValid)
            {
                NoteRepos.Create(note);
                Storage.Save();
                return Ok(note);
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Удалить заметку.
        /// </summary>
        /// <param name="id"> Айди заметки. </param>
        /// <returns> True, если удалено. </returns>
        [Route("delete")]
        [HttpPost]
        public IActionResult DeleteNotes(int id)
        {
            try
            {
                NoteRepos.Delete(id);
                Storage.Save();                
            }
            catch(ArgumentException ex)
            {
                logger.Error(ex.Message);
                return BadRequest(ex.Message);
            }
            return Ok(true);
        }
    }
}
