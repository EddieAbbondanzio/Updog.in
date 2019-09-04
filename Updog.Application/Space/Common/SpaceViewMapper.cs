using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Mapper to convert a space into it's view.
    /// </summary>
    public sealed class SpaceViewMapper : IMapper<Space, SpaceView> {
        #region Fields
        public IMapper<User, UserView> userMapper;
        #endregion

        #region Constructor(s)
        public SpaceViewMapper(IMapper<User, UserView> userMapper) {
            this.userMapper = userMapper;
        }
        #endregion

        #region Publics
        public SpaceView Map(Space source) {
            return new SpaceView(source.Id, source.Name, source.Description, source.SubscriptionCount, source.CreationDate, userMapper.Map(source.User));
        }
        #endregion
    }
}