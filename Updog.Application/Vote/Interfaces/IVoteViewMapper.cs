using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Mapper to convert a vote from it's entity to it's view.
    /// </summary>
    public interface IVoteViewMapper : IMapper<Vote, VoteView> { }
}