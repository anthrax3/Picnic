using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Picnic.Service;

namespace Picnic.Areas.Picnic.Controllers
{    
    [Route("api/content")]
    [Authorize("PicnicAuthPolicy")]
    public class ContentApiController : Controller
    {
        readonly IContentService ContentService;

        /// <summary>
        /// ctor the Mighty
        /// </summary>
        public ContentApiController(IContentService contentService)
        {
            this.ContentService = contentService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var content = await this.ContentService.GetByIdAsync(id);
            return this.Json(content);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var content = await this.ContentService.GetByIdAsync(id);
            await this.ContentService.DeleteAsync(content);
            return this.Json(new { Success = true } );
        }
    }
}