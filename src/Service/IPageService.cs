using System.Threading.Tasks;
using Picnic.Model;

namespace Picnic.Service
{
    public interface IPageService : IGenericService<Page>
    {
        /// <summary>
        /// Gets a page by path
        /// </summary>
        /// <param name="path">The path for the page to be retrieved</param>
        /// <returns>The matching path or null</returns>
        Page GetByPath(string path);

        /// <summary>
        /// Gets a page by path
        /// </summary>
        /// <param name="path">The path for the page to be retrieved</param>
        /// <returns>The matching path or null</returns>
        Task<Page> GetByPathAsync(string path);

        /// <summary>
        /// Determines if a path is already in use
        /// </summary>
        /// <param name="path">The path to check usage for</param>
        /// <param name="id">The id of the page being edited</param>
        /// <returns>True if in use, otherwise false</returns>
        bool PathInUse(string path, string id = null);

        /// <summary>
        /// Determines if a path is already in use
        /// </summary>
        /// <param name="path">The path to check usage for</param>
        /// <param name="id">The id of the page being edited</param>
        /// <returns>True if in use, otherwise false</returns>
        Task<bool> PathInUseAsync(string path, string id = null);

        /// <summary>
        /// Normalizes a page path
        /// </summary>
        /// <param name="path">The path to be normalized</param>
        /// <returns>A normalized path</returns>
        string NormalizePath(string path);
    }
}