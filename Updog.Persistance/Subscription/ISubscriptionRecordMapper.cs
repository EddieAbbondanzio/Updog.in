using System;
using Updog.Application;
using Updog.Domain;

namespace Updog.Persistance {
    /// <summary>
    /// Mapper to convert a subscription record with it's dependencies to a subscription entity.
    /// </summary>
    public interface ISubscriptionRecordMapper : IReversableMapper<SubscriptionRecord, Subscription> { }
}