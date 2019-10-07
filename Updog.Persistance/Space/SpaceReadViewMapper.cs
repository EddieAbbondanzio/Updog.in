using System;
using Updog.Domain;

namespace Updog.Persistance {
    public interface ISpaceReadViewMapper : IMapper<Tuple<SpaceRecord, UserRecord>, SpaceReadView> { }

    public sealed class SpaceReadViewMapper : ISpaceReadViewMapper {
        #region Publics
        public SpaceReadView Map(Tuple<SpaceRecord, UserRecord> source) {
            throw new NotImplementedException();
        }
        #endregion
    }
}