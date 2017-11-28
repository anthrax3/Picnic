using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Picnic.Service;

namespace Picnic.Areas.Picnic.Controllers
{
    [Route("api/page")]
    [Authorize("PicnicAuthPolicy")]
    public class PageApiController : Controller
    {
        readonly IPageService PageService;

        /// <summary>
        /// ctor the Mighty
        /// </summary>
        public PageApiController(IPageService pageService)
        {
            this.PageService = pageService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var content = await this.PageService.GetByIdAsync(id);
            return this.Json(content);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var content = await this.PageService.GetByIdAsync(id);
            await this.PageService.DeleteAsync(content);
            return this.Json(new { Success = true });
        }
    }
}