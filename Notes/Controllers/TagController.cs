using Microsoft.AspNetCore.Mvc;
using NLog;
using Notes.Models;
using Notes.Repository.InterfacesOfRepositories;
using Notes.Repository.InterfacesOfStorage;
using System.Text.Json;

namespace Notes.Controllers
{
    /// <summary>
    /// Контроллер для взаимодействия с тэгами.
    /// </summary>
    [Route("api/[controller]")]
    public class TagController : Controller
    {
        readonly IStorage Storage;
        readonly ITagRepository TagRepos;
        readonly Logger logger;

        public TagController(IStorage storage)
        {
            logger = LogManager.GetLogger("main");
            Storage = storage;
            TagRepos = storage.GetRepository<ITagRepository>();
        }

        /// <summary>
        /// Получить слайс тэгов.
        /// </summary>
        /// <param name="page"> Порядковый номер слайса. </param>
        /// <param name="size"> Размер слайса. </param>
        [Route("tags")]
        [HttpGet]
        public IEnumerable<Tag> GetTags(int page, int size)
        {
            var result = TagRepos.Read(page, size).ToList();
            return result;
        }

        /// <summary>
        /// Изменить тэг.
        /// </summary>
        /// <param name="tag"> Тэг для изменения. </param>
        [Route("update")]
        [HttpPost]
        public IActionResult UpdateTag([FromBody] Tag tag)
        {
            if (ModelState.IsValid)
            {
                TagRepos.Update(tag);
                Storage.Save();
                return Ok(tag);
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Создать новый тэг.
        /// </summary>
        /// <param name="tag"> Тэг для создания. </param>
        [Route("create")]
        [HttpPost]
        public IActionResult CreateTag([FromBody] Tag tag)
        {
            if (ModelState.IsValid)
            {
                TagRepos.Create(tag);
                Storage.Save();
                return Ok(tag);
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Удалить тэг.
        /// </summary>
        /// <param name="id"> Айди тэга. </param>
        [Route("delete")]
        [HttpPost]
        public IActionResult DeleteTag(int id)
        {
            try
            {
                TagRepos.Delete(id);
                Storage.Save();
            }
            catch (ArgumentException ex)
            {
                logger.Error(ex.Message);
                return BadRequest(ex.Message);
            }
            return Ok(true);
        }
    }
}
