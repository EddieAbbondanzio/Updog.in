using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Mapper to convert a vote into it's view.
    /// </summary>
    public sealed class VoteViewMapper : IVoteViewMapper {
        #region Publics
        public VoteView Map(Vote source) {
            return new VoteView(source.ResourceType, source.ResourceId, source.Direction);
        }
        #endregion
    }
}