using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace Picnic.Areas.Picnic.Controllers
{
    [Area("Picnic")]
    [Route("static")]
    public class StaticController : Controller
    {
        [Route("{*url}")]
        public IActionResult Render()
        {
            var match = "static/";
            var rawPath = Request.Path.Value;
            var pathWithoutRoute = rawPath.Substring(rawPath.IndexOf(match, StringComparison.InvariantCultureIgnoreCase) + match.Length);

            var normalizedPath = WebUtility.UrlDecode(pathWithoutRoute).Replace("/", ".");
            
            // images / fonts
            var extension = normalizedPath.Split('.').Last().ToLower();

            var mimeMap = new Dictionary<string, string>
            {
                { "css", "text/css" },
                { "js", "text/javascript" },
                { "gif", "text/gif" },
                { "jpg", "text/jpeg" },
                { "png", "text/png" }
            };

            var mimeType = mimeMap.ContainsKey(extension) ? mimeMap[extension] : "application/octet-stream";

            var assembly = typeof(RootController).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream($"Picnic.Areas.Picnic.wwwroot.{normalizedPath}");
            return this.File(stream, mimeType);
        }
    }
}