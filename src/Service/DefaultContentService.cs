using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Picnic.Model;
using Picnic.Stores;

namespace Picnic.Service
{
    /// <summary>
    /// A serice for performing common operations on Content
    /// </summary>
    public class DefaultContentService : GenericService<Content>, IContentService
    {
        /// <summary>
        /// ctor the Mighty
        /// </summary>
        public DefaultContentService(IGenericStore<Content> store, IHttpContextAccessor httpContextAccessor) : base(store, httpContextAccessor) { }

        /// <summary>
        /// Gets a content entry by key
        /// </summary>
        /// <param name="key">The key for the content to be retrieved</param>
        /// <returns>Mathing Content entry or null if not found</returns>
        public Content GetByKey(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            
            return this.Store.GetSet().FirstOrDefault(x => x.Key == key);
        }

        /// <summary>
        /// Gets a content entry by key
        /// </summary>
        /// <param name="key">The key for the content to be retrieved</param>
        /// <returns>Mathing Content entry or null if not found</returns>
        public async Task<Content> GetByKeyAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            return await this.Store.SingleAsync(x => x.Key == key);
        }

        /// <summary>
        /// Determines if a content key is already in use
        /// </summary>
        /// <param name="key">The key to check usage for</param>
        /// <param name="id">The id of content being edited</param>
        /// <returns>True if in use, otherwise false</returns>
        public bool KeyInUse(string key, string id = null)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            var existing = this.GetByKey(key);
            if (!string.IsNullOrWhiteSpace(id) && existing != null && id == existing.Id)
            {
                return false;
            }

            return existing != null;
        }

        /// <summary>
        /// Determines if a content key is already in use
        /// </summary>
        /// <param name="key">The key to check usage for</param>
        /// <param name="id">The id of content being edited</param>
        /// <returns>True if in use, otherwise false</returns>
        public async Task<bool> KeyInUseAsync(string key, string id = null)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            var existing = await this.GetByKeyAsync(key);
            if (!string.IsNullOrWhiteSpace(id) && id == existing.Id)
            {
                return false;
            }

            return existing != null;
        }
    }
}