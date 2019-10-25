using System;

namespace Updog.Domain {
    /// <summary>
    /// Interface for domain entities to implement.
    /// </summary>
    public interface IEntity {
        #region Properties
        /// <summary>
        /// Unique integer id of the entity.
        /// </summary>
        int Id { get; set; }
        #endregion
    }

    public abstract class Entity<TEntity> : IEntity where TEntity : class, IEntity {
        #region Properties  
        /// <summary>
        /// The unique Id of the entity.
        /// </summary>
        public int Id {
            get => id;
            set => id = id == 0 ? id = value : throw new InvalidOperationException("Cannot change Id of entity.");
        }
        #endregion

        #region Fields
        private int id;
        #endregion

        #region Publics
        /// <summary>
        /// Check to see if the comment is equal to another object.
        /// </summary>
        /// <param name="obj">The other object to check.</param>
        /// <returns>True if the other object matches the comment.</returns>
        public override bool Equals(object? obj) {
            TEntity? c = obj as TEntity;

            if (c == null) {
                return false;
            }

            return Equals(c);
        }

        /// <summary>
        /// Check to see if two comments are equivalent.
        /// </summary>
        /// <param name="c">The other comment to check.</param>
        /// <returns>True if the comments match.</returns>
        public bool Equals(TEntity c) => c.Id == this.Id;

        /// <summary>
        /// Get a unique hashcode of the object.
        /// </summary>
        /// <returns>The unique hashcode.</returns>
        public override int GetHashCode() => Id;
        #endregion
    }
}