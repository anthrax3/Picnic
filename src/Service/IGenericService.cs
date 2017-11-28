using System.Collections.Generic;
using System.Threading.Tasks;
using Picnic.Model;

namespace Picnic.Service
{
    /// <summary>
    /// A generic serice for performing common operations on store entities
    /// </summary>
    /// <typeparam name="TEntity"><see cref="IPicnicEntity" />The type of entity being operated on</typeparam>
    public interface IGenericService<TEntity> where TEntity : class, IPicnicEntity
    {
        /// <summary>
        /// Gets a list of all items of <typeparam name="TEntity" /> 
        /// </summary>
        /// <returns>List of all items of type <typeparam name="TEntity" /></returns>
        IList<TEntity> GetAllItems();

        /// <summary>
        /// Gets a list of all items of <typeparam name="TEntity" /> 
        /// </summary>
        /// <returns>List of all items of type <typeparam name="TEntity" /></returns>
        Task<IList<TEntity>> GetAllItemsAsync();

        /// <summary>
        /// Gets a entity by Id
        /// </summary>
        /// <param name="id">The id of the entity to be retrieved</param>
        /// <returns>The matching entity or null</returns>
        TEntity GetById(string id);

        /// <summary>
        /// Gets a entity by Id
        /// </summary>
        /// <param name="id">The id of the entity to be retrieved</param>
        /// <returns>The matching entity or null</returns>
        Task<TEntity> GetByIdAsync(string id);

        /// <summary>
        /// Gets a list of N recently modified store entities
        /// </summary>
        /// <param name="count">The number of entities to retrieve</param>
        /// <returns>List of matching entites or an empty list if no matches exist</returns>
        IList<TEntity> GetRecent(int count);

        /// <summary>
        /// Gets a list of N recently modified store entities
        /// </summary>
        /// <param name="count">The number of store entities to retrieve</param>
        /// <returns>List of matching store entites or an empty list if no matches exist</returns>
        Task<IList<TEntity>> GetRecentAsync(int count);

        /// <summary>
        /// Saves a entity
        /// </summary>
        /// <param name="item">The entity to be saved</param>
        void Save(TEntity item);

        /// <summary>
        /// Saves a entity
        /// </summary>
        /// <param name="item">The entity to be saved</param>
        Task SaveAsync(TEntity item);

        /// <summary>
        /// Deletes a entity
        /// </summary>
        /// <param name="item">The entity to be deleted</param>
        void Delete(TEntity item);

        /// <summary>
        /// Deletes a entity
        /// </summary>
        /// <param name="item">The entity to be deleted</param>
        Task DeleteAsync(TEntity item);
    }
}