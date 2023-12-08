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
        /// <returns> Слайс тэгов.(Json) </returns>
        [Route("tags")]
        [HttpGet]
        public JsonResult GetTags(int page, int size)
        {
            var list = TagRepos.Read(page, size);
            return Json(data: list);
        }

        /// <summary>
        /// Изменить тэг.
        /// </summary>
        /// <param name="tag"> Тэг для изменения. </param>
        /// <returns> Измененный тэг.(Json) </returns>
        [Route("update")]
        [HttpPost]
        public JsonResult UpdateTag([FromBody] Tag tag)
        {
            try
            {
                TagRepos.Update(tag);
            }
            catch (ArgumentException ex)
            {
                logger.Error(ex.Message);
                return Json(ex.Message);
            }
            return Json(tag);
        }

        /// <summary>
        /// Создать новый тэг.
        /// </summary>
        /// <param name="tag"> Тэг для создания. </param>
        /// <returns> Созданный тэг.(Json) </returns>
        [Route("Create")]
        [HttpPost]
        public JsonResult CreateTag([FromBody] Tag tag)
        {
            return Json(TagRepos.Create(tag));
        }

        /// <summary>
        /// Удалить тэг.
        /// </summary>
        /// <param name="id"> Айди тэга. </param>
        /// <returns> True, если удалено. </returns>
        [Route("Delete")]
        [HttpPost]
        public JsonResult DeleteTag(int id)
        {
            try
            {
                TagRepos.Delete(id);
            }
            catch (ArgumentException ex)
            {
                logger.Error(ex.Message);
                return Json(ex.Message);
            }
            return Json(true);
        }

        /// <summary>
        /// Сохранение всех изменений тэгов.
        /// </summary>
        [Route("Save")]
        [HttpPost]
        public void SaveTags()
        {
            Storage.Save();
        }
    }
}
