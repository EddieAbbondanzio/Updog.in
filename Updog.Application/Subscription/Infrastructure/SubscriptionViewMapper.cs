
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Mappper to convert the subscription entity to it's view.
    /// </summary>
    public sealed class SubscriptionViewMapper : ISubscriptionViewMapper {
        #region Properties
        private IUserViewMapper _userViewMapper;

        private ISpaceViewMapper _spaceViewMapper;
        #endregion

        #region Constructor(s)
        public SubscriptionViewMapper(IUserViewMapper userViewMapper, ISpaceViewMapper spaceViewMapper) {
            _userViewMapper = userViewMapper;
            _spaceViewMapper = spaceViewMapper;
        }
        #endregion

        #region Publics
        public SubscriptionView Map(Subscription source) {
            return new SubscriptionView(
                source.Id,
                _userViewMapper.Map(source.User),
                _spaceViewMapper.Map(source.Space)
            );
        }
        #endregion
    }
}