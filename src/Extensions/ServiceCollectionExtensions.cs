using System;
using ctorx.Core.Mvc;
using ctorx.Core.Mvc.Messaging;
using ctorx.Core.Mvc.Messaging.Extensions;
using ctorx.Core.Mvc.Messaging.Options;
using Microsoft.Extensions.DependencyInjection;
using Picnic.Areas;
using Picnic.Controllers;
using Picnic.Options;
using Picnic.Service;

namespace Picnic.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds Picnic to the application
        /// </summary>
        /// <param name="services">IServiceCollection for the application</param>
        /// <param name="options">Options for how Picnic will operate</param>
        /// <param name="messagingOptions">Options for messaging</param>
        /// <returns>PicnicOptionsBuilder with specified options</returns>
        public static PicnicOptionsBuilder AddPicnic(this IServiceCollection services, Action<PicnicOptions> options = null, Action<MessagingOptions> messagingOptions = null)
        {
            services.AddMessaging(messagingOptions ?? (opts => opts.CookieKey = "picnicmsg"));

            services.AddSingleton<IDefaultMessages, PicnicDefaultMessages>();

            services.AddScoped<IContentService, DefaultContentService>();
            services.AddScoped<IPageService, DefaultPageService>();            

            services.Configure(options ?? PicnicOptions.Default);

            return new PicnicOptionsBuilder(services);
        }

    }
}