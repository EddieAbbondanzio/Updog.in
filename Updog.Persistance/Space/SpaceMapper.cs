
using System;
using Updog.Application;
using Updog.Domain;
using Updog.Persistance;

namespace Updog.Persistance {
    public interface ISpaceMapper : IReversableMapper<SpaceRecord, Space> { }

    /// <summary>
    /// Mapper to convert a space record (DTO) to business entity.
    /// </summary>
    public sealed class SpaceMapper : ISpaceMapper {
        #region Publics
        /// <summary>
        /// Map a record to it's entity.
        /// </summary>
        /// <param name="source">The record to convert.</param>
        /// <returns>The created entity.</returns>
        public Space Map(SpaceRecord source) => new Space(
            source.Id,
            source.UserId,
            source.Name,
            source.Description,
            source.CreationDate,
            source.SubscriptionCount,
            source.IsDefault
        );

        /// <summary>
        /// Reverse the entity back into it's record.
        /// </summary>
        /// <param name="destination">The entity to deconvert.</param>
        /// <returns>The rebuilt record.</returns>
        public SpaceRecord Reverse(Space destination) => new SpaceRecord() {
            Id = destination.Id,
            Name = destination.Name,
            Description = destination.Description,
            CreationDate = destination.CreationDate,
            SubscriptionCount = destination.SuscriberCount,
            IsDefault = destination.IsDefault,
            UserId = destination.UserId
        };
        #endregion
    }
}
