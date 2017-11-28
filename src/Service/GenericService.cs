using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Picnic.Model;
using Picnic.Stores;

namespace Picnic.Service
{
    /// <summary>
    /// A generic serice for performing common operations on store entities
    /// </summary>
    /// <typeparam name="TEntity"><see cref="IPicnicEntity" />The type of entity being operated on</typeparam>
    public abstract class GenericService<TEntity> : IGenericService<TEntity> where TEntity : class, IPicnicEntity
    {
        protected readonly IGenericStore<TEntity> Store;

        /// <summary>
        /// ctor the Mighty
        /// </summary>
        protected GenericService(IGenericStore<TEntity> store)
        {
            this.Store = store;
        }

        /// <summary>
        /// Gets a list of all items of <typeparam name="TEntity" /> 
        /// </summary>
        /// <returns>List of all items of type <typeparam name="TEntity" /></returns>
        public IList<TEntity> GetAllItems()
        {
            return this.Store.List();
        }

        /// <summary>
        /// Gets a list of all items of <typeparam name="TEntity" /> 
        /// </summary>
        /// <returns>List of all items of type <typeparam name="TEntity" /></returns>
        public async Task<IList<TEntity>> GetAllItemsAsync()
        {
            return await this.Store.ListAsync();
        }

        /// <summary>
        /// Gets a entity by Id
        /// </summary>
        /// <param name="id">The id of the entity to be retrieved</param>
        /// <returns>The matching entity or null</returns>
        public TEntity GetById(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException(nameof(id));

            return this.Store.Single(x => x.Id == id);
        }

        /// <summary>
        /// Gets a entity by Id
        /// </summary>
        /// <param name="id">The id of the entity to be retrieved</param>
        /// <returns>The matching entity or null</returns>
        public async Task<TEntity> GetByIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException(nameof(id));

            return await this.Store.SingleAsync(x => x.Id == id);
        }

        /// <summary>
        /// Gets a list of N recently modified store entities
        /// </summary>
        /// <param name="count">The number of entities to retrieve</param>
        /// <returns>List of matching entites or an empty list</returns>
        public IList<TEntity> GetRecent(int count)
        {
            if (count <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            return this.Store.GetSet()
                .OrderByDescending(x => x.LastModifyDate)
                .Take(count)
                .ToList();
        }

        /// <summary>
        /// Gets a list of N recently modified store entities
        /// </summary>
        /// <param name="count">The number of entities to retrieve</param>
        /// <returns>List of matching entites or an empty list</returns>
        public async Task<IList<TEntity>> GetRecentAsync(int count)
        {
            if (count <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            // TODO: Create way to pass queryable, order, etc into store

            return (await this.Store.ListAsync())
                .OrderByDescending(x => x.LastModifyDate)
                .Take(count)
                .ToList();
        }

        /// <summary>
        /// Saves a entity
        /// </summary>
        /// <param name="item">The entity to be saved</param>
        public void Save(TEntity item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            this.ItemUpdated(item);
            this.Store.Save(item);
        }

        /// <summary>
        /// Saves a entity
        /// </summary>
        /// <param name="item">The entity to be saved</param>
        public async Task SaveAsync(TEntity item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            this.ItemUpdated(item);
            await this.Store.SaveAsync(item);
        }

        /// <summary>
        /// Deletes a entity
        /// </summary>
        /// <param name="item">The entity to be deleted</param>
        public void Delete(TEntity item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            this.Store.Delete(item);
        }

        /// <summary>
        /// Deletes a entity
        /// </summary>
        /// <param name="item">The entity to be deleted</param>
        public async Task DeleteAsync(TEntity item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            await this.Store.DeleteAsync(item);
        }

        void ItemUpdated(TEntity item)
        {
            if (string.IsNullOrWhiteSpace(item.Id))
            {
                item.Id = Guid.NewGuid().ToString();
            }

            item.LastModifyDate = DateTime.Now;
            // TODO: access username through auth mechanism somehow
        }
        
    }
}