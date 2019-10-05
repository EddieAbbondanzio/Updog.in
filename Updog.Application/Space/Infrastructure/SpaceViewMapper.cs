using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Mapper to convert a space into it's view.
    /// </summary>
    public sealed class SpaceViewMapper : ISpaceViewMapper {
        #region Publics
        public SpaceView Map(Space source) => new SpaceView(source.Id, source.Name, source.Description, source.SubscriptionCount, source.CreationDate, source.UserId);
        #endregion
    }
}