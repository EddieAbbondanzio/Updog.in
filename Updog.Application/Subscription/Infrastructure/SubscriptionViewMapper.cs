
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Mappper to convert the subscription entity to it's view.
    /// </summary>
    public sealed class SubscriptionViewMapper : ISubscriptionViewMapper {
        #region Properties
        private IUserViewMapper userViewMapper;

        private ISpaceViewMapper spaceViewMapper;
        #endregion

        #region Constructor(s)
        public SubscriptionViewMapper(IUserViewMapper userViewMapper, ISpaceViewMapper spaceViewMapper) {
            this.userViewMapper = userViewMapper;
            this.spaceViewMapper = spaceViewMapper;
        }
        #endregion

        #region Publics
        public SubscriptionView Map(Subscription source) {
            return new SubscriptionView(
                source.Id,
                userViewMapper.Map(source.User),
                spaceViewMapper.Map(source.Space)
            );
        }
        #endregion
    }
}