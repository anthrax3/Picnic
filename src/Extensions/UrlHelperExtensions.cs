using System.IO;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Picnic.Areas.Picnic.Controllers;
using Picnic.Controllers;

namespace Picnic.Extensions
{
    public static class UrlHelperExtensions
    {
        /// <summary>
        /// Generates a url for static content
        /// </summary>
        public static HtmlString Static(this IUrlHelper url, string path)
        {
            return new HtmlString(url.Action(nameof(StaticController.Render), "Static") + path);
        }
    }
}