using System.Threading.Tasks;
using Picnic.Model;

namespace Picnic.Service
{
    public interface IContentService : IGenericService<Content>
    {
        /// <summary>
        /// Gets a content entry by key
        /// </summary>
        /// <param name="key">The key for the content to be retrieved</param>
        /// <returns>Mathing Content entry or null if not found</returns>
        Content GetByKey(string key);

        /// <summary>
        /// Gets a content entry by key
        /// </summary>
        /// <param name="key">The key for the content to be retrieved</param>
        /// <returns>Mathing Content entry or null if not found</returns>
        Task<Content> GetByKeyAsync(string key);

        /// <summary>
        /// Determines if a content key is already in use
        /// </summary>
        /// <param name="key">The key to check usage for</param>
        /// <param name="id">The id of content being edited</param>
        /// <returns>True if in use, otherwise false</returns>
        bool KeyInUse(string key, string id = null);

        /// <summary>
        /// Determines if a content key is already in use
        /// </summary>
        /// <param name="key">The key to check usage for</param>
        /// <param name="id">The id of content being edited</param>
        /// <returns>True if in use, otherwise false</returns>
        Task<bool> KeyInUseAsync(string key, string id = null);
    }
}