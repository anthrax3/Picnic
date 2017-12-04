using System.Threading.Tasks;
using ctorx.Core.Mvc.Messaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Picnic.Model;
using Picnic.Service;

namespace Picnic.Areas.Picnic.Controllers
{
    [Area("Picnic")]
    [Route("content")]
    [Authorize("PicnicAuthPolicy")]
    public class ContentController : Controller
    {
        readonly IMessenger Messenger;
        readonly IContentService ContentService;

        /// <summary>
        /// ctor the Mighty
        /// </summary>
        public ContentController(IMessenger messenger, IContentService contentService)
        {
            this.Messenger = messenger;
            this.ContentService = contentService;
        }
        

        [Route("")]
        public async Task<IActionResult> List()
        {
            var allContent = await this.ContentService.GetAllItemsAsync();
            return this.View(allContent);
        }

        [HttpGet]
        [Route("new")]
        public IActionResult New()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("new")]
        public async Task<IActionResult> New(Content content)
        {
            var keyInUse = false;
            if (this.ModelState.IsValid)
            {
                if (!(keyInUse = this.ContentService.KeyInUse(content.Key)))
                {
                    await this.ContentService.SaveAsync(content);
                    this.Messenger.ForwardSuccess();
                    return this.RedirectToAction(nameof(Edit), new { id = content.Id });
                }
            }

            this.Messenger.AppendError(keyInUse ? "The provided content key is already being used" : null, "Error");
            return this.View(content);
        }

        [HttpGet]
        [Route("{id}/edit")]
        public async Task<IActionResult> Edit(string id)
        {
            var content = await this.ContentService.GetByIdAsync(id);

            if (content == null)
                return this.NotFound();

            return this.View(content);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("{id}/edit")]
        public async Task<IActionResult> Edit(Content content)
        {
            var keyInUse = false;
            if (this.ModelState.IsValid)
            {
                if (!(keyInUse = this.ContentService.KeyInUse(content.Key, content.Id)))
                {
                    await this.ContentService.SaveAsync(content);
                    this.Messenger.ForwardSuccess();
                    return this.RedirectToAction(nameof(Edit), new {id = content.Id});
                }
            }

            this.Messenger.AppendError(keyInUse ? "The provided content key is already being used" : null, "Error");
            return this.View(content);
        }
    }
}