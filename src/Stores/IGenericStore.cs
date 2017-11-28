using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Picnic.Model;

namespace Picnic.Stores
{
    /// <summary>
    /// A generic store to handle entity persistence and retrieval
    /// </summary>
    /// <typeparam name="TStoreItem"><see cref="IPicnicEntity" />The type of entity being operated on</typeparam>
    public interface IGenericStore<TStoreItem> where TStoreItem : class, IPicnicEntity
    {
        /// <summary>
        /// Gets a set of store entities
        /// </summary>
        /// <returns>Set of store entities as an IQueryable</returns>
        IQueryable<TStoreItem> GetSet();

        /// <summary>
        /// Gets a set of store entities
        /// </summary>
        /// <returns>Set of store entities as an IQueryable</returns>
        Task<IQueryable<TStoreItem>> GetSetAsync();

        /// <summary>
        /// Gets a single entity using the provided expression
        /// </summary>
        /// <param name="expression">Expression used to find a matching entity</param>
        /// <returns>The matching entity or null</returns>
        TStoreItem Single(Expression<Func<TStoreItem, bool>> expression);

        /// <summary>
        /// Gets a single entity using the provided expression
        /// </summary>
        /// <param name="expression">Expression used to find a matching entity</param>
        /// <returns>The matching entity or null</returns>
        Task<TStoreItem> SingleAsync(Expression<Func<TStoreItem, bool>> expression);

        /// <summary>
        /// Deletes an entity from the store
        /// </summary>
        /// <param name="item">The entity to be deleted</param>
        void Delete(TStoreItem item);

        /// <summary>
        /// Deletes an entity from the store
        /// </summary>
        /// <param name="item">The entity to be deleted</param>
        Task DeleteAsync(TStoreItem item);

        /// <summary>
        /// Saves an entity to the store
        /// </summary>
        /// <param name="item">The entity to be saved</param>
        void Save(TStoreItem item);

        /// <summary>
        /// Saves an entity to the store
        /// </summary>
        /// <param name="item">The entity to be saved</param>
        Task SaveAsync(TStoreItem item);

        /// <summary>
        /// Gets a list of entites from the store
        /// </summary>
        /// <param name="expression">The expression used to filter the list</param>
        /// <returns>A list of matching entities or an empty list if no matches exist</returns>
        IList<TStoreItem> List(Expression<Func<TStoreItem, bool>> expression = null);

        /// <summary>
        /// Gets a list of entites from the store
        /// </summary>
        /// <param name="expression">The expression used to filter the list</param>
        /// <returns>A list of matching entities or an empty list if no matches exist</returns>
        Task<IList<TStoreItem>> ListAsync(Expression<Func<TStoreItem, bool>> expression = null);
    }
}