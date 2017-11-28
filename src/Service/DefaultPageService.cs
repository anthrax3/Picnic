using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Picnic.Model;
using Picnic.Stores;

namespace Picnic.Service
{
    /// <summary>
    /// A serice for performing common operations on Pages
    /// </summary>
    public class DefaultPageService : GenericService<Page>, IPageService
    {
        /// <summary>
        /// ctor the Mighty
        /// </summary>
        public DefaultPageService(IGenericStore<Page> store) : base(store) { }


        /// <summary>
        /// Gets a page by path
        /// </summary>
        /// <param name="path">The path for the page to be retrieved</param>
        /// <returns>The matching path or null</returns>
        public Page GetByPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentNullException(nameof(path));

            return this.GetAllItems().FirstOrDefault<Page>(x => x.Path == path);
        }

        /// <summary>
        /// Gets a page by path
        /// </summary>
        /// <param name="path">The path for the page to be retrieved</param>
        /// <returns>The matching path or null</returns>
        public async Task<Page> GetByPathAsync(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentNullException(nameof(path));

            return await this.Store.SingleAsync(x => x.Path == path);
        }

        /// <summary>
        /// Determines if a path is already in use
        /// </summary>
        /// <param name="path">The path to check usage for</param>
        /// <param name="id">The id of the page being edited</param>
        /// <returns>True if in use, otherwise false</returns>
        public bool PathInUse(string path, string id = null)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            var existing = this.GetByPath(path);
            if (!string.IsNullOrWhiteSpace(id) && id == existing.Id)
            {
                return false;
            }

            return existing != null;
        }

        /// <summary>
        /// Determines if a path is already in use
        /// </summary>
        /// <param name="path">The path to check usage for</param>
        /// <param name="id">The id of the page being edited</param>
        /// <returns>True if in use, otherwise false</returns>
        public async Task<bool> PathInUseAsync(string path, string id = null)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            var existing = await this.GetByPathAsync(path);
            if (!string.IsNullOrWhiteSpace(id) && existing != null && id == existing.Id)
            {
                return false;
            }

            return existing != null;
        }

        /// <summary>
        /// Normalizes a page path
        /// </summary>
        /// <param name="path">The path to be normalized</param>
        /// <returns>A normalized path</returns>
        public string NormalizePath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            var normalized = new StringBuilder();

            var prevChar = new char();
            foreach (var current in path.ToCharArray())
            {
                if (current != prevChar)
                {
                    normalized.Append(current);
                    prevChar = current;
                }                
            }

            return $"/{normalized.ToString().Trim('/').Trim()}";
        }
    }
}