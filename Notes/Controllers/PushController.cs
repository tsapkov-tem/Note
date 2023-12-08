using Microsoft.AspNetCore.Mvc;
using NLog;
using Notes.Models;
using Notes.Repository.InterfacesOfRepositories;
using Notes.Repository.InterfacesOfStorage;

namespace Notes.Controllers
{
    /// <summary>
    /// Контроллер пушей.
    /// </summary>
    public class PushController : Controller
    {
        readonly IStorage Storage;
        readonly IPushRepository PushRepos;

        public PushController(IStorage storage)
        {
            Storage = storage;
            PushRepos = storage.GetRepository<IPushRepository>();
        }

        /// <summary>
        /// Добавить новый пуш.
        /// </summary>
        /// <param name="push"> Пуш для создания. </param>
        /// <returns> Созданный пуш. </returns>
        /// 
        [Route("Create")]
        [HttpPost]
        public JsonResult CreatePush([FromBody] Push push)
        {
            return Json(PushRepos.Create(push));
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
