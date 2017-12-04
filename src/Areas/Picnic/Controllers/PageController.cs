using System.Threading.Tasks;
using ctorx.Core.Mvc.Messaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Picnic.Model;
using Picnic.Service;

namespace Picnic.Areas.Picnic.Controllers
{
    [Area("Picnic")]
    [Route("pages")]
    [Authorize("PicnicAuthPolicy")]
    public class PageController : Controller
    {
        readonly IMessenger Messenger;
        readonly IPageService PageService;

        /// <summary>
        /// ctor the Mighty
        /// </summary>
        public PageController(IMessenger messenger, IPageService pageService)
        {
            this.Messenger = messenger;
            this.PageService = pageService;
        }

        [Route("")]
        public async Task<IActionResult> List()
        {
            var allPages = await this.PageService.GetAllItemsAsync();
            return this.View(allPages);
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
        public async Task<IActionResult> New(Page page)
        {
            var pathInUse = false;

            page.Path = this.PageService.NormalizePath(page.Path);

            if (this.ModelState.IsValid && !string.IsNullOrWhiteSpace(page.Path))
            {
                if (!(pathInUse = await this.PageService.PathInUseAsync(page.Path, page.Id)))
                {
                    await this.PageService.SaveAsync(page);
                    this.Messenger.ForwardSuccess();
                    return this.RedirectToAction(nameof(Edit), new {id = page.Id});
                }
            }

            this.Messenger.AppendError(pathInUse ? "The provided path is already being used" : null, "Error");
            return this.View(page);
        }

        [HttpGet]
        [Route("{id}/edit")]
        public async Task<IActionResult> Edit(string id)
        {
            var page = await this.PageService.GetByIdAsync(id);

            if (page == null)
                return this.NotFound();

            return this.View(page);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("{id}/edit")]
        public async Task<IActionResult> Edit(Page page)
        {
            var pathInUse = false;

            page.Path = this.PageService.NormalizePath(page.Path);

            if (this.ModelState.IsValid && !string.IsNullOrWhiteSpace(page.Path))
            {
                if (!(pathInUse = await this.PageService.PathInUseAsync(page.Path, page.Id)))
                {
                    await this.PageService.SaveAsync(page);
                    this.Messenger.ForwardSuccess();
                    return this.RedirectToAction(nameof(Edit), new {id = page.Id});
                }
            }

            this.Messenger.AppendError(pathInUse ? "The provided path is already being used" : null, "Error");
            return this.View(page);
        }
    }
}