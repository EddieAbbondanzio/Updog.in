using System;
using Updog.Application;
using Updog.Domain;

namespace Updog.Persistance {
    /// <summary>
    /// Mapper to convert the subscription record to it's entity.
    /// </summary>
    public sealed class SubscriptionRecordMapper : IReversableMapper<SubscriptionRecord, Subscription> {
        #region Publics
        public Subscription Map(SubscriptionRecord source) => new Subscription() {
            Id = source.Id,
            UserId = source.UserId,
            SpaceId = source.SpaceId
        };

        public SubscriptionRecord Reverse(Subscription destination) => new SubscriptionRecord() {
            Id = destination.Id,
            UserId = destination.UserId,
            SpaceId = destination.SpaceId
        };
    }
    #endregion
}