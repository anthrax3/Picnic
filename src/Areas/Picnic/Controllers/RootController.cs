using System.Collections.Generic;
using System.Threading.Tasks;
using ctorx.Core.Mvc.Messaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Picnic.Model;
using Picnic.Service;

namespace Picnic.Areas.Picnic.Controllers
{
    [Area("Picnic")]
    [Route("")]
    [Authorize("PicnicAuthPolicy")]
    public class RootController : Controller
    {
        readonly IPageService PageService;
        readonly IContentService ContentService;

        /// <summary>
        /// ctor the Mighty
        /// </summary>
        public RootController(IMessenger messenger, IPageService pageService, IContentService contentService)
        {
            this.PageService = pageService;
            this.ContentService = contentService;
        }

        [Route("")]
        public async Task<IActionResult> Index()
        {
            var model = new List<IPicnicEntity>();
            model.AddRange(await this.ContentService.GetRecentAsync(10));
            model.AddRange(await this.PageService.GetRecentAsync(10));

            return this.View(model);
        }                
    }
}