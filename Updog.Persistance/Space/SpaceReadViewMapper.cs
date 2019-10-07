using System;
using Updog.Domain;

namespace Updog.Persistance {
    public interface ISpaceReadViewMapper : IMapper<SpaceRecord, SpaceReadView> { }

    public sealed class SpaceReadViewMapper : ISpaceReadViewMapper {
        #region Publics
        public SpaceReadView Map(SpaceRecord source) => new SpaceReadView() {
            Id = source.Id,
            Name = source.Name,
            Description = source.Description,
            CreationDate = source.CreationDate,
            SubscriberCount = source.SubscriptionCount,
            IsDefault = source.IsDefault
        };
        #endregion
    }
}