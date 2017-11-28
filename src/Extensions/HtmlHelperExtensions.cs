using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Picnic.Areas.Picnic.Controllers;

namespace Picnic.Extensions
{
    public static class HtmlHelperExtensions 
    {
        /// <summary>
        /// Renders inline content by key
        /// </summary>
        public static IHtmlContent Content(this IHtmlHelper helper, string key)
        {
            return helper.Partial("_Content", key);
        }        
    }
}