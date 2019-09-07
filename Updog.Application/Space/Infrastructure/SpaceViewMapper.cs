using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Mapper to convert a space into it's view.
    /// </summary>
    public sealed class SpaceViewMapper : ISpaceViewMapper {
        #region Fields
        public IUserViewMapper _userMapper;
        #endregion

        #region Constructor(s)
        public SpaceViewMapper(IUserViewMapper userMapper) {
            this._userMapper = userMapper;
        }
        #endregion

        #region Publics
        public SpaceView Map(Space source) {
            return new SpaceView(source.Id, source.Name, source.Description, source.SubscriptionCount, source.CreationDate, _userMapper.Map(source.User));
        }
        #endregion
    }
}