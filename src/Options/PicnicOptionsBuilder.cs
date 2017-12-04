using System;
using ctorx.JsonStore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Picnic.Model;
using Picnic.Stores;
using Picnic.Stores.EF;
using Picnic.Stores.Json;

namespace Picnic.Options
{
    /// <summary>
    /// Used to build Picnic Options fluently
    /// </summary>
    public class PicnicOptionsBuilder
    {
        public readonly IServiceCollection Services;

        /// <summary>
        /// ctor the Mighty
        /// </summary>
        public PicnicOptionsBuilder(IServiceCollection services)
        {
            this.Services = services;
        }

        /// <summary>
        /// Specifies that Picnic will use a Json store for persistence
        /// </summary>
        /// <param name="fileStorePath">The path on disk to the file store</param>
        /// <returns>PicnicOptionsBuilder with specified options</returns>
        public PicnicOptionsBuilder UseJsonStore(string fileStorePath = "App_Data\\Picnic")
        {
            return this.UseJsonStore(x => x.FileStorePath = fileStorePath);
        }

        /// <summary>
        /// Specifies that Picnic will use a Json store for persistence
        /// </summary>
        /// <param name="options">Options specific to using the Json store</param>
        /// <returns>PicnicOptionsBuilder with specified options</returns>
        public PicnicOptionsBuilder UseJsonStore(Action<JsonStoreOptions> options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            
            this.Services.Configure(options);

            this.Services.AddScoped<IGenericStore<Content>, GenericJsonStore<Content>>();
            this.Services.AddScoped<IGenericStore<Page>, GenericJsonStore<Page>>();

            return this;
        }

        /// <summary>
        /// Specifies that Picnic will use Entity Framework for persistence
        /// </summary>
        /// <typeparam name="TDbContext"><see cref="DbContext"/> The Type of DbContent Picnic should use</typeparam>
        /// <returns>PicnicOptionsBuilder with specified options</returns>
        public PicnicOptionsBuilder UseEFStore<TDbContext>() where TDbContext : DbContext
        {
            this.Services.AddScoped<IGenericStore<Content>, GenericEFStore<Content, TDbContext>>();
            this.Services.AddScoped<IGenericStore<Page>, GenericEFStore<Page, TDbContext>>();

            return this;
        }
    }
}