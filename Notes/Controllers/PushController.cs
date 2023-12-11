using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
using Notes.Models;
using Notes.Repository.InterfacesOfRepositories;
using Notes.Repository.InterfacesOfStorage;

namespace Notes.Controllers
{
    /// <summary>
    /// Контроллер пушей.
    /// </summary>
    [Route("api/[controller]")]
    public class PushController : Controller
    {
        readonly IStorage Storage;
        readonly IPushRepository PushRepos;
        readonly Logger logger;

        public PushController(IStorage storage)
        {
            logger = LogManager.GetLogger("main");
            Storage = storage;
            PushRepos = storage.GetRepository<IPushRepository>();
        }

        /// <summary>
        /// Добавить новый пуш.
        /// </summary>
        /// <param name="push"> Пуш для создания. </param>
        /// <returns> Созданный пуш. </returns>
        /// 
        [Route("create")]
        [HttpPost]
        public IActionResult CreatePush([FromBody] Note note)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var push = new Push(note);
                    PushRepos.Create(push);
                    Storage.Save();
                    return Ok(push);
                }
            }
            catch(ArgumentException ex)
            {
                logger.Error(ex.Message);
                return BadRequest(ex.Message);
            }
            return BadRequest(ModelState);
        }
    }
}
