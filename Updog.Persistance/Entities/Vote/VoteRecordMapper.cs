using System;
using Updog.Application;
using Updog.Domain;

namespace Updog.Persistance {
    /// <summary>
    /// Mapper to convert vote record's to their entity and back.
    /// </summary>
    public sealed class VoteRecordMapper : IReversableMapper<VoteRecord, Vote> {
        #region Publics
        public Vote Map(VoteRecord source) => new Vote() {
            Id = source.Id,
            UserId = source.UserId,
            ResourceId = source.ResourceId,
            ResourceType = source.ResourceType,
            Direction = source.Direction
        };

        public VoteRecord Reverse(Vote destination) => new VoteRecord() {
            Id = destination.Id,
            UserId = destination.UserId,
            ResourceId = destination.ResourceId,
            ResourceType = destination.ResourceType,
            Direction = destination.Direction
        };
        #endregion
    }
}