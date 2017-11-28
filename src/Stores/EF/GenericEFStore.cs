using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Picnic.Model;

namespace Picnic.Stores.EF
{
    /// <summary>
    /// Generic Entity Framework Store for persisting enties to a database
    /// </summary>
    /// <typeparam name="TStoreItem"><see cref="IPicnicEntity" />The type of entity being operated on</typeparam>
    public class GenericEFStore<TStoreItem, TDbContext> : IGenericStore<TStoreItem> where TStoreItem : class, IPicnicEntity where TDbContext : DbContext
    {
        readonly TDbContext Context;

        /// <summary>
        /// ctor the Mighty
        /// </summary>
        public GenericEFStore(TDbContext context)
        {
            this.Context = context;
        }

        /// <summary>
        /// Gets a set of store entities
        /// </summary>
        /// <returns>Set of store entities as an IQueryable</returns>
        public IQueryable<TStoreItem> GetSet()
        {
            return this.Context.Set<TStoreItem>();
        }

        /// <summary>
        /// Gets a set of store entities
        /// </summary>
        /// <returns>Set of store entities as an IQueryable</returns>
        public Task<IQueryable<TStoreItem>> GetSetAsync()
        {
            throw new NotImplementedException("GetSetAsync() is not supported by GenericEFStore");
        }

        /// <summary>
        /// Gets a single entity using the provided expression
        /// </summary>
        /// <param name="expression">Expression used to find a matching entity</param>
        /// <returns>The matching entity or null</returns>
        public TStoreItem Single(Expression<Func<TStoreItem, bool>> expression)
        {
            var set = this.GetSet();

            if (expression != null)
            {
                set = set.Where(expression);
            }

            return set.FirstOrDefault();
        }

        /// <summary>
        /// Gets a single entity using the provided expression
        /// </summary>
        /// <param name="expression">Expression used to find a matching entity</param>
        /// <returns>The matching entity or null</returns>
        public async Task<TStoreItem> SingleAsync(Expression<Func<TStoreItem, bool>> expression)
        {
            IQueryable<TStoreItem> set = this.Context.Set<TStoreItem>();

            if (expression != null)
            {
                set = set.Where(expression);
            }

            return await set.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Gets a list of entites from the store
        /// </summary>
        /// <param name="expression">The expression used to filter the list</param>
        /// <returns>A list of matching entities or an empty list if no matches exist</returns>
        public IList<TStoreItem> List(Expression<Func<TStoreItem, bool>> expression)
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
        public async Task<IList<TStoreItem>> ListAsync(Expression<Func<TStoreItem, bool>> expression)
        {
            var set = this.GetSet();

            if (expression != null)
            {
                set = set.Where(expression);
            }

            return await set.ToListAsync();
        }

        /// <summary>
        /// Deletes an entity from the store
        /// </summary>
        /// <param name="item">The entity to be deleted</param>
        public void Delete(TStoreItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            this.Context.Remove(item);
            this.Context.SaveChanges();
        }

        /// <summary>
        /// Deletes an entity from the store
        /// </summary>
        /// <param name="item">The entity to be deleted</param>
        public async Task DeleteAsync(TStoreItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            this.Context.Remove(item);
            await this.Context.SaveChangesAsync();
        }

        /// <summary>
        /// Saves an entity to the store
        /// </summary>
        /// <param name="item">The entity to be saved</param>
        public void Save(TStoreItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if (!this.GetSet().Any(x => x.Id == item.Id))
            {
                this.Context.Add(item);
            }

            this.Context.SaveChanges();
        }

        /// <summary>
        /// Saves an entity to the store
        /// </summary>
        /// <param name="item">The entity to be saved</param>
        public async Task SaveAsync(TStoreItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if (!await this.GetSet().AnyAsync(x => x.Id == item.Id))
            {
                this.Context.Add(item);
            }

            await this.Context.SaveChangesAsync();
        }        
    }
}