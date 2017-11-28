using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Picnic.Controllers;

namespace Picnic.Extensions
{
    public static class RouteBuilderExtensions
    {
        /// <summary>
        /// Maps the Picnic dynamic page route
        /// </summary>
        /// <param name="name">The name of the route</param>
        /// <param name="template">The template to be used, otherwise defaults to {*url}</param>
        /// <param name="routes">The RouteBuilder for the Mvc Application</param>
        /// <returns>The route builder with the added routes</returns>
        public static IRouteBuilder AddPicnicDynamicPageRoute(this IRouteBuilder routes, string name = "PicnicCatchAll", string template = "{*url}")
        {
            if (routes == null)
            {
                throw new ArgumentNullException(nameof(routes));
            }

            return routes.MapRoute(name, template, new { controller = "Render", action = nameof(RenderController.DynamicPage) });
        }
    }
}