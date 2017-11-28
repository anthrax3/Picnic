using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ctorx.JsonStore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using Picnic.Model;

namespace Picnic.Stores.Json
{
    /// <summary>
    /// Generic Json Store for persisting enties to Json on disk
    /// </summary>
    /// <typeparam name="TStoreItem"><see cref="IPicnicEntity" />The type of entity being operated on</typeparam>
    public class GenericJsonStore<TStoreItem> : JsonStore<TStoreItem, string>, IGenericStore<TStoreItem> where TStoreItem : class, IPicnicEntity
    {
        public GenericJsonStore(IHostingEnvironment hostingEnvironment, IOptions<JsonStoreOptions> jsonStoreOptionsProvider) 
            : base(hostingEnvironment, jsonStoreOptionsProvider, x => x.Id) { }

        /// <summary>
        /// Gets a single entity using the provided expression
        /// </summary>
        /// <param name="expression">Expression used to find a matching entity</param>
        /// <returns>The matching entity or null</returns>
        public TStoreItem Single(Expression<Func<TStoreItem, bool>> expression)
        {
            return this.GetSet().FirstOrDefault(expression);
        }

        /// <summary>
        /// Gets a single entity using the provided expression
        /// </summary>
        /// <param name="expression">Expression used to find a matching entity</param>
        /// <returns>The matching entity or null</returns>
        public async Task<TStoreItem> SingleAsync(Expression<Func<TStoreItem, bool>> expression)
        {
            return (await this.GetSetAsync()).FirstOrDefault(expression);
        }

        /// <summary>
        /// Gets a list of entites from the store
        /// </summary>
        /// <param name="expression">The expression used to filter the list</param>
        /// <returns>A list of matching entities or an empty list if no matches exist</returns>
        public IList<TStoreItem> List(Expression<Func<TStoreItem, bool>> expression = null)
        {
            var set = this.GetSet();
            if (expression != null)
            {
                set = set.Where(expression);
            }
            return set.ToList();
        }

        /// <summary>
        /// Gets a list of entites from the store
        /// </summary>
        /// <param name="expression">The expression used to filter the list</param>
        /// <returns>A list of matching entities or an empty list if no matches exist</returns>
        public async Task<IList<TStoreItem>> ListAsync(Expression<Func<TStoreItem, bool>> expression = null)
        {
            var set = await this.GetSetAsync();
            if (expression != null)
            {
                set = set.Where(expression);
            }
            return set.ToList();
        }
    }
}