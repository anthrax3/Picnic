using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Picnic.Areas.Picnic.Controllers;

namespace Picnic.Options
{
    public class PicnicPrefixAppModelConvention : IApplicationModelConvention
    {
        readonly AttributeRouteModel PrefixRouteModel;

        /// <summary>
        /// Creates a new PicnicAppModelConvention
        /// </summary>
        /// <param name="prefix">The prefix to be applied to picnic manage routes</param>
        public PicnicPrefixAppModelConvention(string prefix = "picnic")
        {
            this.PrefixRouteModel = new AttributeRouteModel(new RouteAttribute(prefix));
        }

        public void Apply(ApplicationModel application)
        {
            var restrictToNamespace = typeof(RootController).Namespace;

            // Loop through any controller matching our selector
            foreach (var controller in application.Controllers)
            {
                var controllerNamespace = controller.ControllerType.Namespace;

                if (!controllerNamespace.Equals(restrictToNamespace, StringComparison.InvariantCultureIgnoreCase))
                    continue;

                foreach (var selectorModel in controller.Selectors.Where(x => x.AttributeRouteModel != null).ToList())
                {
                    var originalAttributeRoute = selectorModel.AttributeRouteModel;
                    selectorModel.AttributeRouteModel = AttributeRouteModel.CombineAttributeRouteModel(this.PrefixRouteModel, originalAttributeRoute);
                }
            }
        }
    }
}