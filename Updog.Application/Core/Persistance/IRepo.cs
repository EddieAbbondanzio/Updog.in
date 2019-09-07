using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// CRUD interface for managing users.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity stored.</typeparam>
    public interface IRepo<TEntity> where TEntity : class, IEntity {
        #region Publics
        /// <summary>
        /// Find an entity by it's unique Id.
        /// </summary>
        /// <param name="id">The id of the entity.</param>
        /// <returns>The matching entity found.</returns>
        Task<TEntity?> FindById(int id);

        /// <summary>
        /// Add a new entity.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        Task Add(TEntity entity);


        /// <summary>
        /// Update an existing entity.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        Task Update(TEntity entity);

        /// <summary>
        /// Delete an existing entity.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        Task Delete(TEntity entity);
        #endregion
    }
}