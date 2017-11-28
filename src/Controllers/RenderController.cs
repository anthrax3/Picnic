using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Picnic.Options;
using Picnic.Model;
using Picnic.Service;
using Picnic.Stores;
using Picnic.Stores.Json;

namespace Picnic.Controllers
{
    /// <summary>
    /// The render controller is used to render dynamic pages
    /// </summary>
    public class RenderController : Controller
    {
        readonly IPageService PageService;
        readonly PicnicOptions Options;

        /// <summary>
        /// ctor the Mighty
        /// </summary>
        public RenderController(IOptions<PicnicOptions> optionsProvider, IPageService pageService)
        {
            this.PageService = pageService;
            this.Options = optionsProvider.Value;
        }

        public async Task<IActionResult> DynamicPage()
        {
            var path = this.Request.Path.Value;
            var page = await this.PageService.GetByPathAsync(path);

            if (page == null || !page.IsActive)
                return this.NotFound();

            return this.View(this.Options.DynamicView.Name, page);
        }
    }
}